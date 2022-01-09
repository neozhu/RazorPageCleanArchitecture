#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/SmartAdmin.WebUI/SmartAdmin.WebUI.csproj", "src/SmartAdmin.WebUI/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/SmartAdmin.WebUI/SmartAdmin.WebUI.csproj"
COPY . .
WORKDIR "/src/src/SmartAdmin.WebUI"
RUN dotnet build "SmartAdmin.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartAdmin.WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENTRYPOINT ["dotnet", "SmartAdmin.WebUI.dll"]
