using AonikenRestAPI.DTO;
using AonikenRestAPI.Helpers;
using AonikenRestAPI.Models;
using AonikenRestAPI.Connections;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Net;
using AonikenRestAPI.Validation;

namespace AonikenRestAPI.Helpers
{
    public static class PostHelper
    {
        public static async Task<IEnumerable<Post>> getPostsByStatus(int status)
        {

            List<Post> postList = new List<Post>();

            string query = QueryHelper.queryGetPostsBystatus(status);

            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                cmd.CommandType = System.Data.CommandType.Text;

                using (var item = cmd.ExecuteReaderAsync())
                {
                    while (item.Result.Read())
                    {
                        var post = new Post();
                        post.PostID = (int)item.Result["ID_POST"];
                        post.Status = (int)item.Result["STATUS"];
                        post.Author = (string)item.Result["AUTHOR"];

                        postList.Add(post);
                    }
                }
            }
            
            return postList;
        }

        internal static async Task<ActionResult<ResponseDTO<Post>>> addPost(Post post)
        {
            ResponseDTO<Post> result;
            try
            {
                string query = QueryHelper.queryInsertPost(post.Author, post.Status);

                using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
                {
                    cmd.CommandType = System.Data.CommandType.Text;

                    var item = await cmd.ExecuteNonQueryAsync();
                        
                    if (item > 0)
                    {
                        result = new ResponseDTO<Post>(post, "Post was inserted correctly", HttpStatusCode.OK);
                    } 
                    else
                    {
                        result = ResponseDTO<Post>.CreateErrorResponse(post);
                    }
                }
                return result;
            }
            catch (SqlException ex) 
            {
                throw new Exception("Failed to access Database: " + ex.Message);
            }
        }

        internal static async Task deletePost(int id)
        {
            string query = QueryHelper.queryDeletePostById(id);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        internal static async Task<ActionResult<ResponseDTO<Post>>> updatePost(Post? post)
        {
            try
            {
                await updatePostStatusByID(post.PostID, post.Status);
                return new ResponseDTO<Post>(post, "Post updated successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update post: {ex.Message}");
            }
        }

        private static async Task updatePostStatusByID(int postId, int newStatus)
        {
            string query = QueryHelper.queryUpdatePoststatusById(postId, newStatus);

            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                await cmd.ExecuteNonQueryAsync();
            }
            
        }

        

        //private static async Task<IEnumerable<>> openConnectionAndExecute(string query, Action)
    }
}
    