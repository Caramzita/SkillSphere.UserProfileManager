FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8084
ENV ASPNETCORE_URLS=http://+:8084

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN dotnet nuget add source http://host.docker.internal:5000/v3/index.json -n baget
ENV NUGET_PACKAGES=/root/.nuget/packages

COPY ["src/SkillSphere.UserProfileManager.API/SkillSphere.UserProfileManager.API.csproj", "src/SkillSphere.UserProfileManager.API/"]
COPY ["src/SkillSphere.UserProfileManager.Contracts/SkillSphere.UserProfileManager.Contracts.csproj", "src/SkillSphere.UserProfileManager.Contracts/"]
COPY ["src/SkillSphere.UserProfileManager.Core/SkillSphere.UserProfileManager.Core.csproj", "src/SkillSphere.UserProfileManager.Core/"]
COPY ["src/SkillSphere.UserProfileManager.DataAccess/SkillSphere.UserProfileManager.DataAccess.csproj", "src/SkillSphere.UserProfileManager.DataAccess/"]
COPY ["src/SkillSphere.UserProfileManager.UseCases/SkillSphere.UserProfileManager.UseCases.csproj", "src/SkillSphere.UserProfileManager.UseCases/"]
RUN dotnet restore "./src/SkillSphere.UserProfileManager.API/SkillSphere.UserProfileManager.API.csproj"

COPY . .
WORKDIR "/src/src/SkillSphere.UserProfileManager.API"
RUN dotnet build "./SkillSphere.UserProfileManager.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SkillSphere.UserProfileManager.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SkillSphere.UserProfileManager.API.dll"]