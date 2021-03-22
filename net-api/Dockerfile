FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IpInfoService.csproj", "net-api/IpInfoService/"]
RUN dotnet restore "net-api/IpInfoService/IpInfoService.csproj"
COPY . .
WORKDIR "/src/net-api/IpInfoService"
RUN dotnet build "IpInfoService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IpInfoService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IpInfoService.dll"]