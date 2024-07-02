using Microsoft.AspNetCore.Mvc;

namespace BurkWebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetUser")]
        public List<Models.User> GetUsers()
        {
            return LoadUsers();
        }

        private static List<Models.User> LoadUsers()
        {
            List<Models.User> userList = [];

            Models.User obj = new()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
            userList.Add(obj);

            obj = new Models.User
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Doe"
            };
            userList.Add(obj);

            return userList;
        }
    }
}
