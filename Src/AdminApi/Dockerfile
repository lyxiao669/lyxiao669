#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AdminApi/AdminApi.csproj", "AdminApi/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "AdminApi/AdminApi.csproj"
COPY . .
WORKDIR "/src/AdminApi"
RUN dotnet build "AdminApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["AdminApi/wwwroot/","wwwroot/"]
ENTRYPOINT ["dotnet", "AdminApi.dll"]