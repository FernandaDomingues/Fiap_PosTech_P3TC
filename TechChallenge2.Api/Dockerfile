#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TechChallenge2.Api/TechChallenge2.Api.csproj", "TechChallenge2.Api/"]
COPY ["TechChallenge.Tests/TechChallenge.Tests.csproj", "TechChallenge.Tests/"]
COPY ["TechChallenge2.Application/TechChallenge2.Application.csproj", "TechChallenge2.Application/"]
COPY ["TechChallenge2.Domain/TechChallenge2.Domain.csproj", "TechChallenge2.Domain/"]
COPY ["TechChallenge2.Data/TechChallenge2.Data.csproj", "TechChallenge2.Data/"]
COPY ["TechChallenge2.Identity/TechChallenge2.Identity.csproj", "TechChallenge2.Identity/"]
RUN dotnet restore "TechChallenge2.Api/TechChallenge2.Api.csproj"
COPY . .
WORKDIR "/src/TechChallenge2.Api"
RUN dotnet build "TechChallenge2.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TechChallenge2.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechChallenge2.Api.dll"]