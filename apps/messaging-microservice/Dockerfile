FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["apps/messaging-microservice/src/MessagingService/MessagingService.csproj", "apps/messaging-microservice/src/MessagingService/"]
COPY ["packages/MessageClient/MessageClient.csproj", "packages/MessageClient/"]
COPY ["packages/Shared.Contracts/Shared.Contracts.csproj", "packages/Shared.Contracts/"]
RUN dotnet restore "apps/messaging-microservice/src/MessagingService/MessagingService.csproj"
COPY . .
WORKDIR "/src/apps/messaging-microservice/src/MessagingService"
RUN dotnet build "MessagingService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MessagingService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessagingService.dll"]
