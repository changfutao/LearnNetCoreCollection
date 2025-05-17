using JWTAuthorizationScheme.Auth;
using JWTAuthorizationScheme.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthorizationScheme.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize(Permissions.UserCreate)]
        public IResultOutput UserCreate() => ResultOutput.Ok(Permissions.UserCreate);
        [HttpGet]
        [Authorize(Permissions.UserUpdate)]
        public IResultOutput UserUpdate() => ResultOutput.Ok(Permissions.UserUpdate);
        [HttpGet]
        [Authorize(Permissions.UserDelete)]
        public IResultOutput UserDelete() => ResultOutput.Ok(Permissions.UserDelete);
    }
}
