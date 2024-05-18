using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.DTO;
using Task.IRepository;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUsers _users;

        public UserController(IUsers users)
        {
            _users = users;
        }

        [HttpPost("Apply")]
        public async Task<DataResponse> Apply(UserDTO user)
        {
            var query = await _users.Apply(user);
            return query;
        }
    }
}
