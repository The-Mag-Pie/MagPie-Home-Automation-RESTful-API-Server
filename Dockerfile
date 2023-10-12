FROM mcr.microsoft.com/dotnet/sdk:latest AS build
WORKDIR /source
COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:latest
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT [ "dotnet", "api_server.dll" ]
