# https://hub.docker.com/_/microsoft-dotnet

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.sln .
COPY MeControla.Core/*.csproj ./MeControla.Core/
COPY Gazin.Portal.Data/*.csproj ./Gazin.Portal.Data/
COPY Gazin.Portal.DataStorage/*.csproj ./Gazin.Portal.DataStorage/
COPY Gazin.Portal.Integrations.Jira/*.csproj ./Gazin.Portal.Integrations.Jira/
COPY Gazin.Portal.Core/*.csproj ./Gazin.Portal.Core/
COPY Gazin.Portal.Integrations.Jira.Tests/*.csproj ./Gazin.Portal.Integrations.Jira.Tests/
COPY Gazin.Portal.Core.Tests/*.csproj ./Gazin.Portal.Core.Tests/
COPY Gazin.Portal.GraphQL/*.csproj ./Gazin.Portal.GraphQL/
COPY Gazin.Portal.RestApi/*.csproj ./Gazin.Portal.RestApi/
RUN dotnet restore
COPY . .
WORKDIR "/src/Gazin.Portal.RestApi"
RUN dotnet build "Gazin.Portal.RestApi.csproj" -c Debug -o /app/build

# copy everything else and build app
FROM build AS publish
RUN dotnet publish "Gazin.Portal.RestApi.csproj" -c Debug -o /app/publish

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gazin.Portal.RestApi.dll"]