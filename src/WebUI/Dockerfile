#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:16-alpine AS client 
WORKDIR /app 
COPY src/WebUI/ClientApp . 
RUN npm install 
RUN npm run-script build

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Application.Interfaces/Application.Interfaces.csproj", "src/Application.Interfaces/"]
COPY ["src/Common/Common.csproj", "src/Common/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Persistence/Persistence.csproj", "src/Persistence/"]
COPY ["src/Service/Service.csproj", "src/Service/"]
COPY ["src/WebUI/WebUI.csproj", "src/WebUI/"]

RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .

WORKDIR "/src/src/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=client /app/dist /app/dist
ENTRYPOINT ["dotnet", "Mortoff.WebUI.dll"]