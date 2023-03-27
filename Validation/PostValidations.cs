using AonikenRestAPI.Connections;
using AonikenRestAPI.Enums;
using AonikenRestAPI.Helpers;
using Microsoft.Data.SqlClient;

namespace AonikenRestAPI.Validation
{
    public static class PostValidations
    {
        public static async Task validatePostForStatusUpdate(int postId, int status)
        {
            validateStatusExistsNotAny(status);
            await validatePostExists(postId);
            await validatePendingStatus(postId);
        }


        public static async Task validateStatusForGetPosts(int status)
        {
            if (!Enum.IsDefined(typeof(PostStatusEnum), status))
            {
                throw new Exception("The status sent is not a valid status code.");
            }
        }

        internal static async Task validatePostForDelete(int id)
        {
            await validatePostExists(id);
        }

        internal static void validateStatusExistsNotAny(int status)
        {
            if (!Enum.IsDefined(typeof(PostStatusEnum), status) || (PostStatusEnum)status == PostStatusEnum.any)
            {
                throw new Exception("The status is not defined. Use a number between 1 and 3");
            }
        }

        private static async Task validatePendingStatus(int postId)
        {
            string query = QueryHelper.queryGetPostByID(postId);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var item = await cmd.ExecuteReaderAsync();

                if (item.Read() && (int)item["STATUS"] != (int)PostStatusEnum.pending)
                {
                    throw new Exception("The post's status can't be changed because it is not pending");
                }
            }
        }

        private static async Task validatePostExists(int postId)
        {
            string query = QueryHelper.queryGetPostByID(postId);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var item = await cmd.ExecuteReaderAsync();
                
                if (!item.Read())
                {
                    throw new Exception("There is no post that matches the PostId");    
                }
            }
        }
    }
}
