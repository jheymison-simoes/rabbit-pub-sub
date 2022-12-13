FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/RabbitPub.Api/RabbitPub.Api.csproj", "RabbitPub.Api/"]
COPY ["src/RabbitPub.Business/RabbitPub.Business.csproj", "RabbitPub.Business/"]
COPY ["src/RabbitPub.Data/RabbitPub.Data.csproj", "RabbitPub.Data/"]
COPY ["src/RabbitPub.Domain/RabbitPub.Domain.csproj", "RabbitPub.Domain/"]
RUN dotnet restore "RabbitPub.Api/RabbitPub.Api.csproj"
COPY . .
WORKDIR "src/RabbitPub.Api"
RUN dotnet build "RabbitPub.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitPub.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitPub.Api.dll"]
