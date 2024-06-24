#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TesteOakTecnologia.API/TesteOakTecnologia.API.csproj", "TesteOakTecnologia.API/"]
COPY ["TesteOakTecnologia.Application/TesteOakTecnologia.Application.csproj", "TesteOakTecnologia.Application/"]
COPY ["TesteOakTecnologia.Domain/TesteOakTecnologia.Domain.csproj", "TesteOakTecnologia.Domain/"]
COPY ["TesteOakTecnologia.Infrastructure/TesteOakTecnologia.Infrastructure.csproj", "TesteOakTecnologia.Infrastructure/"]
RUN dotnet restore "./TesteOakTecnologia.API/TesteOakTecnologia.API.csproj"
COPY . .
WORKDIR "/src/TesteOakTecnologia.API"
RUN dotnet build "./TesteOakTecnologia.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TesteOakTecnologia.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteOakTecnologia.API.dll"]