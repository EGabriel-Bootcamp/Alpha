using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns>Encapsulated object representing the user </returns>
        [HttpPost("create_user")]
        public ActionResult CreateUser(UserForDisplayDto userDto)
        {
            if (userDto != null)
            {
                _userService.Insert(userDto);
                return Ok("User created successfully");
            }
            return BadRequest("Something Went Wrong");
        }

        /// <summary>
        /// Returns all users in the database
        /// </summary>
        /// <returns>An array of users</returns>
        [HttpGet("get_all_users")]
        public ActionResult GetAllUsers()
        {
            var obj = _userService.GetAll();
            if (obj == null)
            {
                return NotFound("No user found");
            }
            return Ok(obj);
        }

        /// <summary>
        /// Endpoint that gets a user based on supplied Id
        /// </summary>
        /// <param name="Id">user Id</param>
        /// <returns>User object</returns>
        [HttpGet("get_user")]
        public ActionResult GetUser(int Id)
        {
            try
            {
                var obj = _userService.Get(Id);
                return Ok(obj);
            }
            catch(Exception)
            {
                return NotFound("User does not exist");
            }
        }
        
        /// <summary>
        /// Endpoint returns users matching filter query
        /// </summary>
        /// <param name="name">User first or last name</param>
        /// <returns>An array of users matching the filter query</returns>
        [HttpGet("filter_user")]
        public ActionResult FilterUser(string name)
        {
            if (name == null){
                return BadRequest();
            }
            var obj =  _userService.Filter(name);
            Console.WriteLine(obj);
            return Ok(obj);
        }
        
        /// <summary>
        /// Endpoint to update a user details
        /// </summary>
        /// <param name="Id">user Id</param>
        /// <param name="userDto">User updateDetails</param>
        /// <returns>void</returns>
        [HttpPatch("edit_user")]
        public ActionResult EditUser(int Id, UserForDisplayDto userDto)
        {

            if (userDto != null)
            {
                _userService.Update(Id, userDto);
                return Ok("user Updated Successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Enpoint deletes user
        /// </summary>
        /// <param name="Id">User Id</param>
        /// <returns>void</returns>
        [HttpPost("delete_user")]
        public ActionResult DeleteUser(int Id)
        {

            if (Id != null)
            {
                _userService.Delete(Id);
                return Ok("User deleted");
            }
            else
            {
                return BadRequest();
            }
        }

    }
}