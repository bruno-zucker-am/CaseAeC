FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CaseAeC.csproj ./
RUN dotnet restore

COPY Controllers/ ./Controllers/
COPY Data/ ./Data/ 
COPY Models/ ./Models/
COPY Views/ ./Views/
COPY Properties/ ./Properties/
COPY wwwroot/ ./wwwroot/
COPY appsettings.json ./
COPY appsettings.Development.json ./
COPY Program.cs ./

RUN dotnet publish CaseAeC.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS="http://+:3007;http://+:8087"
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CaseAeC.dll"]