# iiDENTIfii.Forum
---
by Carl Wessels
iiDENTIfii.Forum is a .NET application designed to serve as the backend for a forum application. You can find the Postman collection [here](https://www.postman.com/carlwessels/workspace/iidentifii-forum/overview).
---
## Usage
* To run the software from the IDE, make sure you are running the Iidentifii.Forum.Api project. The current database server is configured to `(localdb)\\MSSQLLocalDB`, and the database is Forum. Feel free to change this configuration if your database is hosted elsewhere.

## Users
* Create a User: Use the `user\create` endpoint. The first user created will automatically receive the role of Owner. All subsequent users will only have the User role.
* Promote to Moderator: Use the `use\createmoderator` endpoint.
Login: Use the `use\login` endpoint to receive a bearer token. This token must be added to your headers for all endpoints requiring authentication.

All users in the seeded database have a password of "password"
Some accounts :
* Email	Role
* john.doe@example.com	Owner
* jane.smith@example.com	User
* alice.johnson@example.com	User
* bob.brown@example.com	Moderator
* charlie.black@example.com	User
## Subforums
* Create a Subforum: Use the `subforum\create` endpoint.
* Retrieve Subforums: Use the `subforum\get` endpoint to retrieve all subforums.
## Posts
* Create a Post: Use the `post\create` endpoint.
* View a Post: Use the `post\get\{postId}` endpoint.
* List All Posts: Use the `post\get` endpoint. There are optional filters and sorting options available.
## Tags
* Create a Tag Lookup: Use the `taglookup\create` endpoint.
* View Tag Lookups: Use the `taglookup\get` endpoint.
* Tag a Post: Use the `tag\create` endpoint.
## Likes
* A user cannot like their own post or like a post more than once.
* Like or Unlike a Post: Use the `like\likeorunlike` endpoint.
## Comments
* Create a Comment: A signed-in user can comment on a post using the `commen\create` endpoint.
