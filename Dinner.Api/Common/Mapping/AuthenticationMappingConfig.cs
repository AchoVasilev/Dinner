namespace Dinner.Api.Common.Mapping;

using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.Login;
using Application.Common.Authentication;
using Contracts.Authentication;
using Mapster;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}