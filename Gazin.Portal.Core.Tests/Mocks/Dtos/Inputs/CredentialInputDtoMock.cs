using Gazin.Portal.Data.Dtos.Inputs;
using data = Gazin.Portal.Core.Tests.Mocks.Datas.CredentialData;

namespace Gazin.Portal.Core.Tests.Mocks.Dtos.Inputs
{
    public class CredentialInputDtoMock
    {
        public static CredentialInputDto Create()
            => new CredentialInputDto
            {
                Email = data.EMAIL,
                Token = data.TOKEN
            };

        public static CredentialInputDto CreateInvalidEmail()
            => new CredentialInputDto
            {
                Email = data.EMAIL_INVALID,
                Token = data.TOKEN
            };

        public static CredentialInputDto CreateEmpty()
            => new CredentialInputDto();
    }
}