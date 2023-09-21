using MediatR;
using Microsoft.AspNetCore.Mvc;
using NAFAPI.APPLICATION.Commands.Account.Login;
using NAFCommon.Base.Common.MethodResult;
using System.Net;
using System.Threading.Tasks;

namespace NAFAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private const string Login = nameof(Login);

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gen token login - (Author: son)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<LoginCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        [Route(Login)]
        public async Task<IActionResult> LoginAsync(LoginCommand command)
        {
            var result = await _mediator.Send(command).ConfigureAwait(false);
            return Ok(result);
        }
    }
}