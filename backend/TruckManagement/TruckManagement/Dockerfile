#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine3.14 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine3.14 AS build
WORKDIR /src
COPY ["TruckManagement/TruckManagement.csproj", "TruckManagement/"]
COPY ["TruckManagement.Business.Interfaces/TruckManagement.Business.Interfaces.csproj", "TruckManagement.Business.Interfaces/"]
COPY ["TruckManagement.ViewModels/TruckManagement.ViewModels.csproj", "TruckManagement.ViewModels/"]
COPY ["TruckManagement.Models/TruckManagement.Models.csproj", "TruckManagement.Models/"]
COPY ["TruckManagement.Business/TruckManagement.Business.csproj", "TruckManagement.Business/"]
COPY ["TruckManagement.Repository.Interfaces/TruckManagement.Repository.Interfaces.csproj", "TruckManagement.Repository.Interfaces/"]
COPY ["TruckManagement.Infra.Core/TruckManagement.Infra.Core.csproj", "TruckManagement.Infra.Core/"]
COPY ["TruckManagement.Repository/TruckManagement.Repository.csproj", "TruckManagement.Repository/"]
RUN dotnet restore "TruckManagement/TruckManagement.csproj"
COPY . .
WORKDIR "/src/TruckManagement"
RUN dotnet build "TruckManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TruckManagement.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TruckManagement.dll"]