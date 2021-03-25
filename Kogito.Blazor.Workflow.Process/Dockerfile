#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Kogito.Blazor.Workflow.Process/Kogito.Blazor.Workflow.Process.csproj", "Kogito.Blazor.Workflow.Process/"]
RUN dotnet restore "Kogito.Blazor.Workflow.Process/Kogito.Blazor.Workflow.Process.csproj"
COPY . .
WORKDIR "/src/Kogito.Blazor.Workflow.Process"
RUN dotnet build "Kogito.Blazor.Workflow.Process.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kogito.Blazor.Workflow.Process.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kogito.Blazor.Workflow.Process.dll"]