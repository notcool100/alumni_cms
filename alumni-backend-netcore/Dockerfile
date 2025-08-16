# Use the official .NET 8.0 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["AlumniBackendApi/AlumniBackendApi.csproj", "AlumniBackendApi/"]
RUN dotnet restore "AlumniBackendApi/AlumniBackendApi.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/AlumniBackendApi"

# Build the application
RUN dotnet build "AlumniBackendApi.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "AlumniBackendApi.csproj" -c Release -o /app/publish

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "AlumniBackendApi.dll"]
