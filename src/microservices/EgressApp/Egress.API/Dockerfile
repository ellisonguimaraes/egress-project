#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/microservices/EgressApp/Egress.API/Egress.API.csproj", "src/microservices/EgressApp/Egress.API/"]
COPY ["src/microservices/EgressApp/Egress.Infra/Egress.Infra.Data/Egress.Infra.Data.csproj", "src/microservices/EgressApp/Egress.Infra/Egress.Infra.Data/"]
COPY ["src/microservices/EgressApp/Egress.Domain/Egress.Domain.csproj", "src/microservices/EgressApp/Egress.Domain/"]
COPY ["src/microservices/EgressApp/Egress.Infra/Egress.Infra.CrossCutting.Resource/Egress.Infra.CrossCutting.Resource.csproj", "src/microservices/EgressApp/Egress.Infra/Egress.Infra.CrossCutting.Resource/"]
COPY ["src/microservices/EgressApp/Egress.Infra/Egress.Infra.CrossCutting.IoC/Egress.Infra.CrossCutting.IoC.csproj", "src/microservices/EgressApp/Egress.Infra/Egress.Infra.CrossCutting.IoC/"]
COPY ["src/microservices/EgressApp/Egress.Application/Egress.Application.csproj", "src/microservices/EgressApp/Egress.Application/"]
RUN dotnet restore "src/microservices/EgressApp/Egress.API/Egress.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/EgressApp/Egress.API"
RUN dotnet build "Egress.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Egress.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Egress.API.dll"]
