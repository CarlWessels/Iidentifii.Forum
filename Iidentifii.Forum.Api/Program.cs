using FastEndpoints;
using FastEndpoints.Security;
using Iidentifii.Forum.Library.Auth;
using Iidentifii.Forum.Library.Comments;
using Iidentifii.Forum.Library.Database;
using Iidentifii.Forum.Library.Likes;
using Iidentifii.Forum.Library.Posts;
using Iidentifii.Forum.Library.Subforums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Iidentifii.Forum.Api.MiddleWare;
using Iidentifii.Forum.Library.Tags;
using FastEndpoints.Swagger;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .SwaggerDocument()
    .AddAuthorization()
    .AddFastEndpoints()

    .AddScoped<ITokenService, TokenService>()
    .AddScoped<IUserManager, UserManager>()
    .AddScoped<IPostService, PostService>()
    .AddScoped<ICommentService, CommentService>()
    .AddScoped<ILikeService, LikeService>()
    .AddScoped<ITagService, TagService>()

    .AddSingleton<IDbConnectionFactory, DbConnectionFactory>()
    .AddSingleton<ISubforumService, SubformuService>()

    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["JwtSettings:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
        };
    });

var app = builder.Build();
app.UseFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerGen();
    app.UseDeveloperExceptionPage();
}
else
    ErrorHandlingMiddlewareExtensions.UseErrorHandlingMiddleware(app);


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
