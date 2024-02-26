FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

FROM  mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ./src /src/BonyadMaskan.WebApi/

WORKDIR /src/BonyadMaskan.WebApi

RUN dotnet restore --force "BonyadMaskan.WebApi/BonyadMaskan.WebApi.csproj" -v n

RUN dotnet build "BonyadMaskan.WebApi/BonyadMaskan.WebApi.csproj" -c Release -o "/app/build"

RUN dotnet publish "BonyadMaskan.WebApi/BonyadMaskan.WebApi.csproj" -c Release -o "/app/publish"

FROM base AS publish

WORKDIR /app/publish

COPY --from=build /app/publish .

RUN rm -rf /etc/localtime
RUN ln -s /usr/share/zoneinfo/Asia/Tehran /etc/localtime

ENTRYPOINT ["dotnet","BonyadMaskan.WebApi.dll","--urls=http://0.0.0.0:82"]
