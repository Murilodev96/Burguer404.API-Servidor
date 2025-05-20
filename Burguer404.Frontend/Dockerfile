FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Burguer404.Frontend/Burguer404.Frontend.csproj", "Burguer404.Frontend/"]
RUN dotnet restore "Burguer404.Frontend/Burguer404.Frontend.csproj"
COPY . .
WORKDIR "/src/Burguer404.Frontend"
RUN dotnet build "Burguer404.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Burguer404.Frontend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Burguer404.Frontend.dll"]
