#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CM.WeeklyTeamReport/src/CM.WeeklyTeamReport.WebApp/CM.WeeklyTeamReport.WebApp.csproj", "CM.WeeklyTeamReport/src/CM.WeeklyTeamReport.WebApp/"]
RUN dotnet restore "CM.WeeklyTeamReport/src/CM.WeeklyTeamReport.WebApp/CM.WeeklyTeamReport.WebApp.csproj"
COPY . .
WORKDIR "/src/CM.WeeklyTeamReport/src/CM.WeeklyTeamReport.WebApp"
RUN dotnet build "CM.WeeklyTeamReport.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CM.WeeklyTeamReport.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CM.WeeklyTeamReport.WebApp.dll"]