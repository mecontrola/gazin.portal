using Gazin.Portal.Core.Business;
using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Gazin.Portal.RestApi.Configurations.RoutesConfiguration;

namespace Gazin.Portal.RestApi.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IAuthorizationBusiness authorizationBusiness;
        private readonly IRefreshTokenBusiness refreshTokenBusiness;

        public AuthenticationController(ILogger<AuthenticationController> logger,
                                        IAuthorizationBusiness authorizationBusiness,
                                        IRefreshTokenBusiness refreshTokenBusiness)
        {
            this.logger = logger;
            this.authorizationBusiness = authorizationBusiness;
            this.refreshTokenBusiness = refreshTokenBusiness;
        }

        /// <summary>
        /// Performs user authentication to gain access to available functionality.
        /// </summary>
        /// <returns>Authorization data</returns>
        /// <response code="200">Returns the Authorization data</response>
        /// <response code="404">If the user was not found</response>
        /// <response code="500">Internal server error</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost(Authentication.LOGIN)]
        [ProducesResponseType(typeof(AuthorizationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginAction([FromBody] CredentialInputDto request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Autenticando o usuário {request.Email}");

            return await ExecuteActionAsync(authorizationBusiness.Login, request, cancellationToken);
        }

        /// <summary>
        /// Performs user authentication to gain access to available functionality.
        /// </summary>
        /// <returns>Refreshed token data</returns>
        /// <response code="200">Returns the refreshed token data</response>
        /// <response code="404">If the token was not found</response>
        /// <response code="500">Internal server error</response>
        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost(Authentication.REFRESH)]
        [ProducesResponseType(typeof(RefreshTokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenInputDto request, CancellationToken cancellationToken)
            => await ExecuteActionAsync(refreshTokenBusiness.GenerateToken, request, cancellationToken);

        protected async Task<IActionResult> ExecuteActionAsync<T, U>(Func<U, CancellationToken, Task<T>> function, U inputDto, CancellationToken cancellationToken)
            => Ok(await function(inputDto, cancellationToken));
    }
}