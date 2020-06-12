#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Task_4/Task_4.csproj", "Task_4/"]
RUN dotnet restore "Task_4/Task_4.csproj"
COPY . .
WORKDIR "/src/Task_4"
RUN dotnet build "Task_4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Task_4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Task_4.dll"]