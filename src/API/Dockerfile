FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /build

COPY src/ src/
RUN dotnet restore src/API

WORKDIR /build/src/API
RUN dotnet publish -c Release -r linux-x64 --no-self-contained

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /build/src/API/bin/Release/net6.0/linux-x64/publish/ .
ENTRYPOINT [ "dotnet", "API.dll" ]