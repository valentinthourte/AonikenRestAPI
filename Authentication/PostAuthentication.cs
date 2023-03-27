using AonikenRestAPI.Connections;
using AonikenRestAPI.Helpers;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Web;
namespace AonikenRestAPI.Authentication
{
    public class PostAuthentication
    {
        internal static async Task authenticateUser(IHeaderDictionary headers) // Podría recibir solo el string de Authorization, pero el cambio implica varias modificaciónes
        {                                                                      // sobre la estructura del código existente.
            headers.TryGetValue("Authorization", out StringValues authHeader);

            if (authHeader.Any() && authHeader.ToString().StartsWith("Basic "))
            {
                getUserCredentialsFromEncodedHeader(authHeader.ToString().Substring("Basic ".Length).Trim(), out string username, out string pass);
                await validateUserCredentials(username, pass);
            }
            else
            {
                throw new AuthenticationFailedException("The authentication header is either empty or not basic");
            }
        }

        private static void getUserCredentialsFromEncodedHeader(string encodedAuth, out string username, out string pass)
        {
            Encoding encoding = Encoding.GetEncoding("iso-8859-1");

            string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedAuth));

            int index = encodedAuth.IndexOf(":");
            username = usernamePassword.Substring(0, index);
            pass = usernamePassword.Substring(index);
        }

        private static async Task validateUserCredentials(string username, string pass)
        {
            var query = QueryHelper.queryGetUserByUsername(username);
            using (SqlCommand cmd = await DBConnection.getConnectedSqlCommand(query))
            {
                var result = await cmd.ExecuteReaderAsync();

                if (result.Read()) 
                {
                    if (result["PASS"].ToString() != pass)
                    {
                        throw new AuthenticationFailedException("The password does not correspond to the user");
                    }
                }
                else
                {
                    throw new AuthenticationFailedException("There is no registered user with that username");
                }
            }
        }
    }
}
