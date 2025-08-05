# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PlatformService.csproj", "./"]
RUN dotnet restore "./PlatformService.csproj"
COPY . .
RUN dotnet publish "PlatformService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
# EXPOSE 8080
# EXPOSE 8081
ENTRYPOINT ["dotnet", "PlatformService.dll"]


# Build the Docker image
# > docker build -t platformservice .

# Run the container
# > docker run -d -p 5200:8080 --name platformservice platformservice

# Visit http://localhost:5200/api/platform to see the API in action.
# You can also use tools like Postman or curl to interact with the API endpoints.

# Push to Docker Hub
# > docker tag platformservice <your-dockerhub-username>/platformservice:latest
# > docker tag platformservice esteadonis/platformservice:latest  

# > docker push <your-dockerhub-username>/platformservice:latest
# > docker push esteadonis/platformservice:latest

# After pushing go to https://hub.docker.com/repositories/esteadonis