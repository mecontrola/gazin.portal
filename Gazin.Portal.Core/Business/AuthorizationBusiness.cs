using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Gazin.Portal.Data.Entities;
using Gazin.Portal.DataStorage.Repositories;
using Gazin.Portal.Integrations.Jira;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Exceptions;
using MeControla.Core.Configurations.Managers;
using MeControla.Core.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public class AuthorizationBusiness : IAuthorizationBusiness
    {
        private const string TOKEN_TYPE = "Bearer";

        private readonly IJWTManager jwtManager;
        private readonly IAuthSessionGet authSession;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public AuthorizationBusiness(IJWTManager jwtManager,
                                     IAuthSessionGet authSession,
                                     IRefreshTokenRepository refreshTokenRepository)
        {
            this.jwtManager = jwtManager;
            this.authSession = authSession;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthorizationDto> Login(CredentialInputDto credentials, CancellationToken cancellationToken)
        {
            var authenticationDto = await IsAuthenticatedSuccess(credentials, cancellationToken);

            var jwtData = jwtManager.Generate(credentials.Email, credentials.Token, authenticationDto.AccountId);
            var refreshToken = await AddRefreshToken(jwtData);

            return new AuthorizationDto
            {
                AccessToken = jwtData.Token,
                Expiration = jwtData.Expired,
                TokenType = TOKEN_TYPE,
                RefreshToken = refreshToken
            };
        }

        private async Task<AuthenticationDto> IsAuthenticatedSuccess(CredentialInputDto credentials, CancellationToken cancellationToken)
        {
            try
            {
                return await authSession.IsAuthenticated(credentials.Email, credentials.Token, cancellationToken);
            }
            catch (JiraException ex)
            {
                throw new UnauthorizedException(ex);
            }
        }

        private async Task<Guid> AddRefreshToken(IJWTData jwtData)
        {
            var refreshToken = GenerateRefreshToken(jwtData);

            await refreshTokenRepository.CreateAsync(refreshToken);

            return refreshToken.Uuid;
        }

        private static RefreshToken GenerateRefreshToken(IJWTData jwtData)
            => new RefreshToken
            {
                Uuid = jwtData.Jti,
                Token = jwtData.Token,
                Created = jwtData.Created,
                Expired = jwtData.Expired,
                Used = false,
                Invalidated = false
            };
    }
}