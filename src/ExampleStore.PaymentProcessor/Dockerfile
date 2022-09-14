#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ExampleStore.PaymentProcessor/ExampleStore.PaymentProcessor.csproj", "src/ExampleStore.PaymentProcessor/"]
RUN dotnet restore "src/ExampleStore.PaymentProcessor/ExampleStore.PaymentProcessor.csproj"
COPY . .
WORKDIR "/src/src/ExampleStore.PaymentProcessor"
RUN dotnet build "ExampleStore.PaymentProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleStore.PaymentProcessor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleStore.PaymentProcessor.dll"]