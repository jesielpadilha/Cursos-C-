#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NSE.Pedido.API/NSE.Pedido.API.csproj", "NSE.Pedido.API/"]
COPY ["NSE.MessageBus/NSE.MessageBus.csproj", "NSE.MessageBus/"]
COPY ["NSE.Core/NSE.Core.csproj", "NSE.Core/"]
COPY ["NSE.Pedido.Domain/NSE.Pedido.Domain.csproj", "NSE.Pedido.Domain/"]
COPY ["NSE.Pedido.Infra/NSE.Pedido.Infra.csproj", "NSE.Pedido.Infra/"]
COPY ["NSE.WebAPI.Core/NSE.WebAPI.Core.csproj", "NSE.WebAPI.Core/"]
RUN dotnet restore "NSE.Pedido.API/NSE.Pedido.API.csproj"
COPY . .
WORKDIR "/src/NSE.Pedido.API"
RUN dotnet build "NSE.Pedido.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Pedido.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Pedido.API.dll"]