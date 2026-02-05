# Use the official .NET 8.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy all project files and restore dependencies
COPY ["WebAPI/Alumni.WebAPI.csproj", "WebAPI/"]
COPY ["Infrastructure/Alumni.Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Alumni.Application.csproj", "Application/"]
COPY ["Domain/Alumni.Domain.csproj", "Domain/"]
RUN dotnet restore "WebAPI/Alumni.WebAPI.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/WebAPI"

# Build the application
RUN dotnet build "Alumni.WebAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Alumni.WebAPI.csproj" -c Release -o /app/publish

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Create a non-root user
# Note: For SQLite, the user needs write access to the directory containing the .db file
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "Alumni.WebAPI.dll"]
