using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextHome.API.Common.Extensions;
using NextHome.Application.UseCases.Users.Interfaces;

namespace NextHome.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ICreateUserUseCase _createUser;
        private readonly IGetUserByIdUseCase _getUserById;
        private readonly IGetAllUsersUseCase _getAllUsers;
        private readonly IUpdateUserUseCase _updateUser;
        private readonly IDeleteUserUseCase _deleteUser;


        [HttpGet("me")]
        [Authorize]
        public IActionResult GetAuthenticatedUser()
        {
            var userId = User.GetUserId();
            var email = User.GetUserEmail();
            var username = User.GetUsername();
            var roles = User.GetRoles();

            return Ok(new
            {
                Id = userId,
                Email = email,
                Username = username,
                Roles = roles
            });
        }
    }
}
