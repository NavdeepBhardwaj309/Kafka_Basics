# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files
COPY MyApp.Api/MyApp.Api.csproj MyApp.Api/
COPY MyApp.Application/MyApp.Application.csproj MyApp.Application/
COPY MyApp.Core/MyApp.Core.csproj MyApp.Core/
COPY MyApp.Infrastructure/MyApp.Infrastructure.csproj MyApp.Infrastructure/

# Restore dependencies
RUN dotnet restore MyApp.Api/MyApp.Api.csproj

# Copy remaining source
COPY . .

# Build
RUN dotnet build MyApp.Api/MyApp.Api.csproj -c Release --no-restore

# Publish
RUN dotnet publish MyApp.Api/MyApp.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "MyApp.Api.dll"]