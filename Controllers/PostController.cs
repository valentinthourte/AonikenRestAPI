using AonikenRestAPI.Authentication;
using AonikenRestAPI.DTO;
using AonikenRestAPI.Helpers;
using AonikenRestAPI.Models;
using AonikenRestAPI.Validation;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace AonikenRestAPI.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : Controller
    {

        [HttpGet]
        [Route("getPostsBystatus/{status}")]
        public async Task<ActionResult<IEnumerable<Post>>> Get(int status)
        {
            try
            {
                await PostAuthentication.authenticateUser(HttpContext.Request.Headers); // Me está faltando verificar si el usuario tiene un tipo apto para operar los posts
                await PostValidations.validateStatusForGetPosts(status);                
                var response = await PostHelper.getPostsByStatus(status);
                return Ok(response);
            }
            catch (AuthenticationFailedException authEx)
            {
                return BadRequest($"Could not authenticate user: {authEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addPost")]
        public async Task<IActionResult> Post([FromBody] JObject obj) 
        {
            try
            {
                await PostAuthentication.authenticateUser(HttpContext.Request.Headers);
                Post? post = obj.ToObject<Post>();
                PostValidations.validateStatusExistsNotAny(post.Status);
                var response = await PostHelper.addPost(post);
                return Ok(response);
            }
            catch (AuthenticationFailedException authEx)
            {
                return BadRequest($"Could not authenticate user: {authEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not add post: {ex.Message}");
            }

        }

        [HttpPut]
        [Route("updatePost")]
        public async Task<IActionResult> Put([FromBody] JObject obj)
        {

            Post post = obj.ToObject<Post>();
            try
            {
                await PostAuthentication.authenticateUser(HttpContext.Request.Headers);
                await PostValidations.validatePostForStatusUpdate(post.PostID, post.Status);
                var response = await PostHelper.updatePost(post);
                return Ok(response);
            }
            catch (AuthenticationFailedException authEx)
            {
                return BadRequest($"Could not authenticate user: {authEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<Post>(post, ex.Message, System.Net.HttpStatusCode.BadRequest));
            }
        }

        [HttpDelete]
        [Route("deletePost/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await PostAuthentication.authenticateUser(HttpContext.Request.Headers);
                await PostValidations.validatePostForDelete(id);
                await PostHelper.deletePost(id);
                return Ok("Post deleted successfully");
            }
            catch (AuthenticationFailedException authEx)
            {
                return BadRequest($"Could not authenticate user: {authEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not delete post: {ex.Message}");
            }
        }
    }
}
