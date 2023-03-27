using AonikenRestAPI.Connections;
using AonikenRestAPI.Enums;
using AonikenRestAPI.Helpers;
using AonikenRestAPI.Models;
using Microsoft.Data.SqlClient;

namespace AonikenRestAPI.Validation
{
    public static class UserValidations
    {
        internal static void validateNewUserData(User user)
        {
            validateUsername(user.USERNAME);
            validatePassword(user.PASS);
            validateUserType(user.USER_TYPE);
        }

        private static void validateUserType(int USER_TYPE)
        {
            if (!Enum.IsDefined(typeof(UserTypesEnum), USER_TYPE))
            {
                throw new ArgumentException("User type is not valid. Please use an integer value between 1 and 2");
            }
        }

        private static void validatePassword(string PASS)
        {
            if (string.IsNullOrEmpty(PASS))
            {
                throw new ArgumentException("Passowrd is null or empty");
            }
        }

        private static void validateUsername(string USERNAME)
        {
            if (string.IsNullOrEmpty(USERNAME))
            {
                throw new ArgumentException("Username is null or empty");
            }
        }

        internal static async Task validateUserForUpdate(User? user)
        {
            validatePassword(user.PASS);
            validateUserType(user.USER_TYPE);
            await validateUserExists(user.USERNAME);
        }
        internal static async Task validateUserForUpdateByID(User? user)
        {
            validateUsername(user.USERNAME);
            await validateUserForUpdate(user);
        }
        private static async Task validateUserExists(string uSERNAME)
        {
            string query = QueryHelper.queryGetUserByUsername(uSERNAME);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteReaderAsync();

                if (!result.Read())
                {
                    throw new ArgumentException("There is no user that matches the username");
                }
            }
        }

        internal static async Task validateUsernameForDelete(string username)
        {
            await validateUserExists(username);
        }
    }
}
