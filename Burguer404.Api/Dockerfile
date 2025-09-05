FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Burguer404.Api/Burguer404.Api.csproj", "Burguer404.Api/"]
COPY ["Burguer404.Configurations/Burguer404.Configurations.csproj", "Burguer404.Configurations/"]
COPY ["Burguer404.Application/Burguer404.Application.csproj", "Burguer404.Application/"]
COPY ["Burguer404.Domain/Burguer404.Domain.csproj", "Burguer404.Domain/"]
COPY ["Burguer404.Infrastructure.Pagamentos/Burguer404.Infrastructure.Pagamentos.csproj", "Burguer404.Infrastructure.Pagamentos/"]
COPY ["Burguer404.Infrastructure/Burguer404.Infrastructure.Data.csproj", "Burguer404.Infrastructure/"]
COPY ["Burguer404.Infrastructure.Auth/Burguer404.Infrastructure.Auth.csproj", "Burguer404.Infrastructure.Auth/"]
RUN dotnet restore "./Burguer404.Api/Burguer404.Api.csproj"
COPY . .
WORKDIR "/src/Burguer404.Api"
RUN dotnet build "./Burguer404.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Burguer404.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Burguer404.Api.dll"]