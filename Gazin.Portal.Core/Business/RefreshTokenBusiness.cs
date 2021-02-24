using Gazin.Portal.Data.Dtos;
using Gazin.Portal.Data.Dtos.Inputs;
using Gazin.Portal.Data.Entities;
using Gazin.Portal.DataStorage.Repositories;
using MeControla.Core.Configurations;
using MeControla.Core.Configurations.Exceptions;
using MeControla.Core.Configurations.Managers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Business
{
    public class RefreshTokenBusiness : IRefreshTokenBusiness
    {
        private const string TOKEN_TYPE = "Bearer";

        private readonly IJWTConfiguration jwtConfiguration;
        private readonly IJWTManager jwtManager;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public RefreshTokenBusiness(IJWTConfiguration jwtConfiguration,
                                    IJWTManager jwtManager,
                                    IRefreshTokenRepository refreshTokenRepository)
        {
            this.jwtConfiguration = jwtConfiguration;
            this.jwtManager = jwtManager;
            this.refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshTokenDto> GenerateToken(RefreshTokenInputDto input, CancellationToken cancellationToken)
        {
            var principal = GetClaimPrincipal(input);
            var expiryDateUnix = GetExp(principal);
            var expiryDateTimeUtc = GetExpiryDateTimeUtc(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
                throw new JWTTokenException("This Token is expired.");

            var jti = GetJti(principal);

            var storedRefreshToken = await refreshTokenRepository.FindByTokenAsync(input.RefreshToken);
            if (storedRefreshToken == null)
                throw new JWTTokenException("This refresh token does not exists.");

            if (DateTime.UtcNow > storedRefreshToken.Expired)
                throw new JWTTokenException("This refresh token has expired.");

            if (storedRefreshToken.Invalidated)
                throw new JWTTokenException("This refresh token has been invalidated.");

            if (storedRefreshToken.Used)
                throw new JWTTokenException("This refresh token has been used.");

            if (!storedRefreshToken.Uuid.ToString().Equals(jti))
                throw new JWTTokenException("This refresh token does not match this JWT.");

            storedRefreshToken.Used = false;

            await refreshTokenRepository.UpdateAsync(storedRefreshToken);

            var jwtData = jwtManager.Generate(GetEmail(principal), GetIss(principal), GetSid(principal));
            var newRefreshToken = await AddRefreshToken(jwtData);

            return new RefreshTokenDto
            {
                AccessToken = jwtData.Token,
                RefreshToken = newRefreshToken,
                TokenType = TOKEN_TYPE,
                Expiration = jwtData.Expired
            };
        }

        private ClaimsPrincipal GetClaimPrincipal(RefreshTokenInputDto refreshToken)
            => jwtManager.GetClaimsPrincipal(refreshToken.AccessToken)
            ?? throw new JWTTokenException("Invalid Token'.");

        private DateTime GetExpiryDateTimeUtc(long expiryDateUnix)
            => GetDefaultDateTime().AddSeconds(expiryDateUnix)
                                   .Subtract(jwtConfiguration.TimeToExpire);

        private static DateTime GetDefaultDateTime()
            => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static string GetEmail(ClaimsPrincipal principal)
            => GetValueFromClaim(principal, JwtRegisteredClaimNames.Email);

        private static string GetSid(ClaimsPrincipal principal)
            => GetValueFromClaim(principal, JwtRegisteredClaimNames.Sid);

        private static string GetIss(ClaimsPrincipal principal)
            => GetValueFromClaim(principal, JwtRegisteredClaimNames.Iss);

        private static string GetJti(ClaimsPrincipal principal)
            => GetValueFromClaim(principal, JwtRegisteredClaimNames.Jti);

        private static long GetExp(ClaimsPrincipal principal)
            => long.Parse(GetValueFromClaim(principal, JwtRegisteredClaimNames.Exp));

        private static string GetValueFromClaim(ClaimsPrincipal principal, string key)
            => principal.Claims.Single(x => x.Type.Contains(key)).Value;

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