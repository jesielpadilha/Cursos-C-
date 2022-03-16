#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NSE.Pagamento.API/NSE.Pagamentos.API.csproj", "NSE.Pagamento.API/"]
COPY ["NSE.Pagamentos.NerdsPag/NSE.Pagamentos.NerdsPag.csproj", "NSE.Pagamentos.NerdsPag/"]
COPY ["NSE.MessageBus/NSE.MessageBus.csproj", "NSE.MessageBus/"]
COPY ["NSE.Core/NSE.Core.csproj", "NSE.Core/"]
COPY ["NSE.WebAPI.Core/NSE.WebAPI.Core.csproj", "NSE.WebAPI.Core/"]
RUN dotnet restore "NSE.Pagamento.API/NSE.Pagamentos.API.csproj"
COPY . .
WORKDIR "/src/NSE.Pagamento.API"
RUN dotnet build "NSE.Pagamentos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSE.Pagamentos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Pagamentos.API.dll"]