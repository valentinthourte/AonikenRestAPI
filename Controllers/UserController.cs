using AonikenRestAPI.DTO;
using AonikenRestAPI.Helpers;
using AonikenRestAPI.Models;
using AonikenRestAPI.Validation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace AonikenRestAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : Controller
    {

        [HttpGet]
        [Route("getUsers")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var response = await UserHelper.getUsers();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                return Ok(await UserHelper.getUserByID(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("addUser")]
        public async Task<ActionResult<ResponseDTO<User>>> Post([FromBody] JObject obj)
        {

            User? user = obj.ToObject<User>();
            try
            {
                UserValidations.validateNewUserData(user);
                var result = await UserHelper.addUser(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<User>(user, $"Could not add user: {ex.Message}", HttpStatusCode.BadRequest));
            }
        }

        [HttpPut]
        [Route("updateUserByUsername")]
        public async Task<ActionResult<ResponseDTO<User>>> Put([FromBody] JObject obj)
        {
            User? user = obj.ToObject<User>();
            try
            {
                await UserValidations.validateUserForUpdate(user);
                var result = await UserHelper.updateUserByUsername(user);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<User>(user, $"Could not update user: {ex.Message}", HttpStatusCode.BadRequest));
            }
        }

        [HttpPut]
        [Route("updateUserById")]
        public async Task<ActionResult<ResponseDTO<User>>> PutByID([FromBody] JObject obj)
        {
            User? user = obj.ToObject<User>();
            try
            {
                await UserValidations.validateUserForUpdateByID(user);

                var result = await UserHelper.updateUserByID(user);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDTO<User>(user, $"Could not update user: {ex.Message}", HttpStatusCode.BadRequest));
            }
        }

        [HttpDelete]
        [Route("deleteUserByUsername/{username}")]
        public async Task<ActionResult<ResponseDTO<User>>> Delete(string username)
        {
            try
            {
                await UserValidations.validateUsernameForDelete(username);
                var response = await UserHelper.deleteUserByUsername(username);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return NotFound($"Could not delete user: {ex.Message}");
            }
            
        }
    }
}
