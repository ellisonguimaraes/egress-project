#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/microservices/AuthApp/API/AuthApp.API/AuthApp.API.csproj", "src/microservices/AuthApp/API/AuthApp.API/"]
COPY ["src/microservices/AuthApp/Infra/AuthApp.Infra.Data/AuthApp.Infra.Data.csproj", "src/microservices/AuthApp/Infra/AuthApp.Infra.Data/"]
COPY ["src/microservices/AuthApp/Infra/AuthApp.Infra.CrossCutting.Resources/AuthApp.Infra.CrossCutting.Resources.csproj", "src/microservices/AuthApp/Infra/AuthApp.Infra.CrossCutting.Resources/"]
COPY ["src/microservices/AuthApp/Domain/AuthApp.Domain/AuthApp.Domain.csproj", "src/microservices/AuthApp/Domain/AuthApp.Domain/"]
COPY ["src/microservices/AuthApp/Infra/AuthApp.Infra.CrossCutting.IoC/AuthApp.Infra.CrossCutting.IoC.csproj", "src/microservices/AuthApp/Infra/AuthApp.Infra.CrossCutting.IoC/"]
COPY ["src/microservices/AuthApp/Application/AuthApp.Application/AuthApp.Application.csproj", "src/microservices/AuthApp/Application/AuthApp.Application/"]
RUN dotnet restore "src/microservices/AuthApp/API/AuthApp.API/AuthApp.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/AuthApp/API/AuthApp.API"
RUN dotnet build "AuthApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthApp.API.dll"]
