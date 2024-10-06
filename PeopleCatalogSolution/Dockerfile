
# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official ASP.NET Core SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

# Build the project
RUN dotnet restore "./PeopleCatalog.API/PeopleCatalog.API.csproj"
RUN dotnet build "./PeopleCatalog.API/PeopleCatalog.API.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "./PeopleCatalog.API/PeopleCatalog.API.csproj" -c Release -o /app/publish

# Copy to base and run
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeopleCatalog.API.dll"]
