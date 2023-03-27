using AonikenRestAPI.Enums;

namespace AonikenRestAPI.Helpers
{
    public static class QueryHelper
    {
        public static string queryGetPostsBystatus(int status)
        {
            string filter;
            filter = (status == (int)PostStatusEnum.any) ? "" : $"WHERE STATUS = '{status}'";
            return "SELECT * FROM POSTS " + filter;
        }

        internal static string queryDeletePostById(int id)
        {
            return $"DELETE POSTS WHERE ID_POST = {id}";
        }

        internal static string queryDeleteUserByUsername(string username)
        {
            return $"DELETE USERS WHERE USERNAME = '{username}'";
        }

        internal static string queryGetPostByID(int postId)
        {
            return $"SELECT * FROM POSTS WHERE ID_POST = {postId}";
        }

        internal static string queryGetUserById(int id)
        {
            return $"{queryGetUsers()} WHERE ID_USER = {id}";
        }

        internal static string queryGetUserByUsername(string uSERNAME)
        {
            return $"SELECT * FROM USERS WHERE USERNAME = '{uSERNAME}'";
        }

        internal static string queryGetUsers()
        {
            return "SELECT * FROM USERS";
        }

        internal static string queryInsertPost(string author, int status)
        {
            return $"INSERT INTO POSTS (AUTHOR, STATUS) VALUES ('{author}', {status})";
        }

        internal static string queryInsertUser(string username, string pass, int user_type)
        {
            return $"INSERT INTO USERS (USERNAME, PASS, USER_TYPE) VALUES ('{username}','{pass}',{user_type})";
        }

        internal static string queryUpdatePoststatusById(int id, int status)
        {
            return $"UPDATE POSTS SET STATUS = '{status}' WHERE ID_POST = {id}";
        }

        internal static string queryUpdateUserByID(int iD_USER, string uSERNAME, string pASS, int uSER_TYPE)
        {
            return $"UPDATE USERS SET USERNAME = '{uSERNAME}', PASS = '{pASS}', USER_TYPE = '{uSER_TYPE}' WHERE ID_USER = {iD_USER}";
        }

        internal static string queryUpdateUserByUsername(string username, string pass, int user_type)
        {
            return $"UPDATE USERS SET PASS = '{pass}', USER_TYPE = {user_type} WHERE USERNAME = '{username}'";
        }
    }
}
