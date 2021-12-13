FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CM.WeeklyTeamReport.Database.csproj", "CM.WeeklyTeamReport.Database.csproj"]
RUN dotnet restore "CM.WeeklyTeamReport.Database.csproj"
COPY . .
RUN dotnet build "CM.WeeklyTeamReport.Database.csproj"
ENTRYPOINT [ "dotnet" ]
CMD [ "publish", "/src/CM.WeeklyTeamReport.Database.csproj", \
"/p:TargetServerName=$TargetServerName", "/p:TargetDatabaseName=$TargetDatabaseName", \
"/p:TargetUser=$TargetUser", "/p:TargetPassword=$TargetPassword" ]