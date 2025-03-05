# Base image for ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage with .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["Farenheit/Farenheit.csproj", "Farenheit/"]
WORKDIR "/src/Farenheit"
RUN dotnet restore

# Copy everything and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Use "dotnet Farenheit.dll" as entry point
CMD ["dotnet", "Farenheit.dll"]
