FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Propulse.sln .
COPY Propulse.Core/Propulse.Core.csproj Propulse.Core/
COPY Propulse.Infrastructure/Propulse.Infrastructure.csproj Propulse.Infrastructure/
COPY Propulse.Api/Propulse.Api.csproj Propulse.Api/
RUN dotnet restore

COPY . .
RUN dotnet publish Propulse.Api/Propulse.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

RUN mkdir -p /app/data

COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:${PORT:-8080}

ENTRYPOINT ["dotnet", "Propulse.Api.dll"]
