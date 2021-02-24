using Gazin.Portal.Data.Dtos.Inputs;
using data = Gazin.Portal.Core.Tests.Mocks.Datas.RefreshTokenData;

namespace Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs
{
    class RefreshTokenInputDtoMock
    {
        public static RefreshTokenInputDto Create()
            => new RefreshTokenInputDto
            {
                AccessToken = data.ACCESS_TOKEN,
                RefreshToken = data.REFRESH_TOKEN
            };

        public static RefreshTokenInputDto CreateEmpty()
            => new RefreshTokenInputDto();
    }
}