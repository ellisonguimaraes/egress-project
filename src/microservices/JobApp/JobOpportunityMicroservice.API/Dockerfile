#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/microservices/JobApp/JobOpportunityMicroservice.API/JobOpportunityMicroservice.API.csproj", "src/microservices/JobApp/JobOpportunityMicroservice.API/"]
COPY ["src/microservices/JobApp/JobOpportunityMicroservice.Infra.Data/JobOpportunityMicroservice.Infra.Data.csproj", "src/microservices/JobApp/JobOpportunityMicroservice.Infra.Data/"]
COPY ["src/microservices/JobApp/JobOpportunityMicroservice.Domain/JobOpportunityMicroservice.Domain.csproj", "src/microservices/JobApp/JobOpportunityMicroservice.Domain/"]
COPY ["src/microservices/JobApp/JobOpportunityMicroservice.Infra.CrossCutting.Resource/JobOpportunityMicroservice.Infra.CrossCutting.Resource.csproj", "src/microservices/JobApp/JobOpportunityMicroservice.Infra.CrossCutting.Resource/"]
COPY ["src/microservices/JobApp/JobOpportunityMicroservice.Infra.CrossCutting.IoC/JobOpportunityMicroservice.Infra.CrossCutting.IoC.csproj", "src/microservices/JobApp/JobOpportunityMicroservice.Infra.CrossCutting.IoC/"]
COPY ["src/microservices/JobApp/JobOpportunityMicroservice.Application/JobOpportunityMicroservice.Application.csproj", "src/microservices/JobApp/JobOpportunityMicroservice.Application/"]
RUN dotnet restore "src/microservices/JobApp/JobOpportunityMicroservice.API/JobOpportunityMicroservice.API.csproj"
COPY . .
WORKDIR "/src/src/microservices/JobApp/JobOpportunityMicroservice.API"
RUN dotnet build "JobOpportunityMicroservice.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobOpportunityMicroservice.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobOpportunityMicroservice.API.dll"]
