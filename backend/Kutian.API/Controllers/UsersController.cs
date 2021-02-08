using System.Net;
using System.Threading.Tasks;
using Kutian.Domain.ViewModels;
using Kutian.Infrastructure.Features.Users.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kutian.API.Controllers
{
    public class UsersController : BaseController
    {
        [HttpPost("login", Name = "LoginRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] Login.Query loginQuery) =>
            await Mediator.Send(loginQuery);
    }
}