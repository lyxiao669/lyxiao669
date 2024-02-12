#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Juzhen.AiYanJing.MiniApi/Juzhen.AiYanJing.MiniApi.csproj", "Juzhen.AiYanJing.MiniApi/"]
COPY ["Juzhen.Infrastructure/Juzhen.Infrastructure.csproj", "Juzhen.Infrastructure/"]
COPY ["Juzhen.Domain/Juzhen.Domain.csproj", "Juzhen.Domain/"]
RUN dotnet restore "Juzhen.AiYanJing.MiniApi/Juzhen.AiYanJing.MiniApi.csproj"
COPY . .
WORKDIR "/src/Juzhen.AiYanJing.MiniApi"
RUN dotnet build "Juzhen.AiYanJing.MiniApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Juzhen.AiYanJing.MiniApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Juzhen.AiYanJing.MiniApi/wwwroot/","wwwroot/"]
ENTRYPOINT ["dotnet", "Juzhen.AiYanJing.MiniApi.dll"]