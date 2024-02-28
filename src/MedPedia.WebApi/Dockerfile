#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MedPedia.WebApi/MedPedia.WebApi.csproj", "MedPedia.WebApi/"]
RUN dotnet restore "MedPedia.WebApi/MedPedia.WebApi.csproj"
COPY . .
WORKDIR "/src/MedPedia.WebApi"
RUN dotnet build "MedPedia.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedPedia.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedPedia.WebApi.dll","--urls=http://0.0.0.0:92"]