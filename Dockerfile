FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.sln .
COPY AdvancedHealthChecks/*.csproj ./AdvancedHealthChecks/
RUN dotnet restore

COPY AdvancedHealthChecks/. ./AdvancedHealthChecks/
WORKDIR /source/AdvancedHealthChecks
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "AdvancedHealthChecks.dll"]