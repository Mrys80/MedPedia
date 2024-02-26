#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Teb.WebApi/Teb.WebApi.csproj", "Teb.WebApi/"]
RUN dotnet restore "Teb.WebApi/Teb.WebApi.csproj"
COPY . .
WORKDIR "/src/Teb.WebApi"
RUN dotnet build "Teb.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Teb.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Teb.WebApi.dll","--urls=http://0.0.0.0:82"]