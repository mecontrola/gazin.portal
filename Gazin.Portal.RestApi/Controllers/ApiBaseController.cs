using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.RestApi.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        protected async Task<IActionResult> ExecuteActionAsync<T, U>(Func<U, CancellationToken, Task<T>> function, U inputDto, CancellationToken cancellationToken)
            => Ok(await function(inputDto, cancellationToken));

        protected async Task<IActionResult> ExecuteInsertAsync<T, U>(string getActionName, Func<U, CancellationToken, Task<T>> function, U inputDto, CancellationToken cancellationToken)
            where T : IIdDto
        {
            var item = await function(inputDto, cancellationToken);

            return CreatedAtRoute(getActionName, new { item.Id }, item);
        }

        protected async Task<IActionResult> ExecutePutAsync<T, U>(Func<U, CancellationToken, Task<T>> function, U inputDto, CancellationToken cancellationToken)
            where T : IIdDto
            => Ok(await function(inputDto, cancellationToken));

        protected async Task<IActionResult> ExecuteDeleteAsync<T>(Func<T, CancellationToken, Task<bool>> function, T inputDto, CancellationToken cancellationToken)
        {
            await function(inputDto, cancellationToken);

            return NoContent();
        }

        protected T FillInput<T>(T input, Action<T> action)
            where T : BaseInputDto
        {
            input.Username = GetEmail();
            input.Password = GetNameId();

            action(input);

            return input;
        }

        protected T FillInput<T>(Action<T> action)
            where T : BaseInputDto, new()
            => FillInput(new T(), action);

        private string GetEmail()
            => GetClaimInfo(JwtRegisteredClaimNames.Email);

        private string GetNameId()
            => GetClaimInfo(JwtRegisteredClaimNames.NameId);

        protected string GetSid()
            => GetClaimInfo(JwtRegisteredClaimNames.Sid);

        private string GetClaimInfo(string key)
            => User.Claims.FirstOrDefault(itm => itm.Type.Contains(key))?.Value;
    }
}