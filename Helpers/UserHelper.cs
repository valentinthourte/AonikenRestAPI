using AonikenRestAPI.Connections;
using AonikenRestAPI.DTO;
using AonikenRestAPI.Models;
using Azure;
using Microsoft.Data.SqlClient;
using System.Net;

namespace AonikenRestAPI.Helpers
{
    public static class UserHelper
    {
        internal static async Task<ResponseDTO<User>> addUser(User user)
        {
            ResponseDTO<User> response;
            string query = QueryHelper.queryInsertUser(user.USERNAME, user.PASS, user.USER_TYPE);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteNonQueryAsync();
                string responseMsg;
                HttpStatusCode responseCode;
                bool isSuccess = result > 0;
                responseMsg = (isSuccess) ? "Successfully added user" : "Could not add user";
                responseCode = (isSuccess) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;

                response = new ResponseDTO<User>(user, responseMsg, responseCode);
            }
            return response;
        }

        internal static async Task<ResponseDTO<string>> deleteUserByUsername(string username)
        {
            ResponseDTO<string> response;
            string query = QueryHelper.queryDeleteUserByUsername(username);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteNonQueryAsync();
                string responseMsg;
                HttpStatusCode responseCode;

                bool isSuccess = result > 0;
                responseMsg = (isSuccess) ? "Successfully deleted user" : "Could not delete user";
                responseCode = (isSuccess) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                response = new($"Deleted user with username: {username}", responseMsg, responseCode);    
            }
            return response;
        }

        internal static async Task<User> getUserByID(int id)
        {
            User returnUser = new User();
            string query = QueryHelper.queryGetUserById(id);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteReaderAsync();
                if (result.Read())
                {
                    returnUser.ID_USER = (int)result["ID_USER"];
                    returnUser.USERNAME = result["USERNAME"].ToString();
                    returnUser.PASS = result["PASS"].ToString();
                    returnUser.USER_TYPE = (int)result["USER_TYPE"];
                }
            }
            return returnUser;
        }

        internal static async Task<IEnumerable<User>> getUsers()
        {
            var users = new List<User>();
            string query = QueryHelper.queryGetUsers();
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteReaderAsync();

                while (result.Read()) {
                    User user = new User();
                    user.ID_USER = (int)result["ID_USER"];
                    user.USERNAME = result["USERNAME"].ToString();
                    user.PASS = result["PASS"].ToString();
                    user.USER_TYPE = (int)result["USER_TYPE"];
                    users.Add(user);
                }
            }
            return users;
        }

        private static async Task<ResponseDTO<User>> updateUser(User user, string query)
        {
            ResponseDTO<User> response;

            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteNonQueryAsync();
                string responseMsg;
                HttpStatusCode responseCode;

                bool isSuccess = result > 0;
                responseMsg = (isSuccess) ? "Successfully updated user" : "Could not update user";
                responseCode = (isSuccess) ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
                response = new(user, responseMsg, responseCode);
            }
            return response;
        }
        internal static async Task<ResponseDTO<User>> updateUserByUsername(User user)
        {
            return await updateUser(user, QueryHelper.queryUpdateUserByUsername(user.USERNAME, user.PASS, user.USER_TYPE));
        }

        internal static async Task<ResponseDTO<User>> updateUserByID(User? user)
        {
            return await updateUser(user, QueryHelper.queryUpdateUserByID(user.ID_USER, user.USERNAME, user.PASS, user.USER_TYPE));
        }
    }
}
