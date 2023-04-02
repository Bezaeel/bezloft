#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 3000

ENV ASPNETCORE_URLS=http://+:3000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["bezloft.api/bezloft.api.csproj", "bezloft.api/"]
COPY ["bezloft.application/bezloft.application.csproj", "bezloft.application/"]
COPY ["bezloft.core/bezloft.core.csproj", "bezloft.core/"]
COPY ["bezloft.infrastructure/bezloft.infrastructure.csproj", "bezloft.infrastructure/"]
RUN dotnet restore "bezloft.api/bezloft.api.csproj"
COPY . .
WORKDIR "/src/bezloft.api"
RUN dotnet build "bezloft.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "bezloft.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "bezloft.api.dll"]