FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

FROM  mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ./src /src/MedPedia.WebApi/

WORKDIR /src/MedPedia.WebApi

RUN dotnet restore --force "MedPedia.WebApi/MedPedia.WebApi.csproj" -v n

RUN dotnet build "MedPedia.WebApi/MedPedia.WebApi.csproj" -c Release -o "/app/build"

RUN dotnet publish "MedPedia.WebApi/MedPedia.WebApi.csproj" -c Release -o "/app/publish"

FROM base AS publish

WORKDIR /app/publish

COPY --from=build /app/publish .

RUN rm -rf /etc/localtime
RUN ln -s /usr/share/zoneinfo/Asia/Tehran /etc/localtime

ENTRYPOINT ["dotnet","MedPedia.WebApi.dll","--urls=http://0.0.0.0:92"]
