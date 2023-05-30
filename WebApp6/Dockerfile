# Set the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS build-env

RUN apk update && apk add libgdiplus
RUN apk add ttf-freefont && apk add libssl1.1
RUN apk update; apk add fontconfig ttf-liberation
RUN fc-cache -f -v

# Set the working directory in the container
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
# Expose the port that the application listens on
EXPOSE 80

# Set the base image for the final container
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# Copy cs, csproj and restore as distinct layers
WORKDIR /src
COPY ["WebApp6/*.cs", "WebApp6/"]
COPY ["WebApp6/*.csproj", "WebApp6/"]
RUN dotnet restore "WebApp6/WebApp6.csproj"
COPY . .

WORKDIR "/src/WebApp6"
RUN dotnet build "WebApp6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApp6.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM build-env AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Define the command to run the application
ENTRYPOINT ["dotnet", "WebApp6.dll"]