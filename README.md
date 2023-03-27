README
Application to interact with a WEB API, which can store users and posts in real time.

To get the application up and running, you must:
1) Clone the github repository to your local machine
2) In SQL Server Management Studio, you must execute the CreateDatabase\CreateDatabase.sql script in order to create and initialize the database
3) Navigate to appsettings.json and modify the cnxStr key for a valid connection string to the created database (named Aoniken)
4) Build and execute the solution
5) Once the application is running, use the generated API URL for sending requests through postman

SENDING REQUESTS FOR POSTS:
GET: There is a GET endpoint defined which recieves a status (1: pending, 2: approved, 3: rejected, 4: any) and returns all posts corresponding to that status.
URL is [YOUR_URL]/posts/getPostsByStatus/{status}
POST: Adds post to database. Must have STATUS and AUTHOR defined in the body. URL is addUser
PUT: Updates post in database. Must have STATUS and AUTHOR defined in body. URL is updatePost
DELETE: Deletes post from database. Must send post ID in URL. URL is deletePost/{id}

USERS:
GET: Returns all users. URL is getUsers. Also has an endpoint for users by ID. URL is getUserById/{id}
POST: Adds user. Must have the user's username, password and user_type in the request body. URL is addUser
PUT: Updates user. Must have user's username, password and user_type in the request body. URL is updateUserByUsername. You can also use updateUserByID, adding the ID_USER to the request body.
DELETE: Deletes user by username. Must have the username in URL. URL is deleteUserByUsername/{username}



