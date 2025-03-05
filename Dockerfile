FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# If your project is inside a folder, adjust the path accordingly
COPY ["Farenheit/Farenheit.csproj", "Farenheit/"]
WORKDIR "/src/Farenheit"
RUN dotnet restore

# Copy the entire project
COPY . .
WORKDIR "/src/Farenheit"

# Publish the API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Correct entry point for an ASP.NET Core Web API
CMD ["dotnet", "Farenheit.dll"]
