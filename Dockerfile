FROM eventstore/eventstore AS eventstore
ENTRYPOINT [ ]
CMD sed --in-place "s/2113/${PORT-2113}/" /etc/eventstore/eventstore.conf && /entrypoint.sh


FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime-base
WORKDIR /app/


FROM microsoft/dotnet:2.2-sdk AS solution-build
ARG configuration=Debug
WORKDIR /project/
COPY . .
RUN dotnet build Peyk.sln --configuration ${configuration}


FROM solution-build AS publish-clientserver
WORKDIR /project/
RUN dotnet publish src/Peyk.ClientServer.Web/Peyk.ClientServer.Web.csproj --configuration Release --output /app/


FROM runtime-base AS final-client-server
WORKDIR /app/
COPY --from=publish-clientserver /app /app
CMD ASPNETCORE_URLS=http://+:${PORT:-80} dotnet Peyk.ClientServer.Web.dll
