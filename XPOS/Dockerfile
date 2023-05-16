#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["XPOS_FE/XPOS_FE.csproj", "XPOS_FE/"]
COPY ["XPOS_API/XPOS_API.csproj", "XPOS_API/"]
COPY ["XPOS_ViewModels/XPOS_ViewModels.csproj", "XPOS_ViewModels/"]
RUN dotnet restore "XPOS_FE/XPOS_FE.csproj"
COPY . .
WORKDIR "/src/XPOS_FE"
RUN dotnet build "XPOS_FE.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XPOS_FE.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XPOS_FE.dll"]