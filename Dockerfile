FROM mcr.microsoft.com/dotnet/sdk:8.0 AS  build
COPY . /src
WORKDIR /src/PublicApi
RUN dotnet publish -c Release -o /app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
COPY --from=0 /app /app
WORKDIR /app
ENV ASPNETCORE_URLS http://*:80
ENTRYPOINT ["dotnet", "PublicApi.dll"]