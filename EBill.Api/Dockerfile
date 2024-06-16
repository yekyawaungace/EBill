#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Am.Api/Am.Api.csproj", "Am.Api/"]
COPY ["Am.Infrastructure/Am.Infrastructure.csproj", "Am.Infrastructure/"]
COPY ["Am.Service/Am.Service.csproj", "Am.Service/"]
COPY ["Am.Repository.Ef/Am.Repository.Ef.csproj", "Am.Repository.Ef/"]
RUN dotnet restore "Am.Api/Am.Api.csproj"
COPY . .
WORKDIR "/src/Am.Api"
RUN dotnet build "Am.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Am.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Am.Api.dll"]