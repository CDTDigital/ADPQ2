FROM microsoft/aspnetcore:1.0.1
#FROM microsoft/dotnet
ENV ASPNETCORE_ENVIRONMENT Production
WORKDIR /app
EXPOSE 5050
COPY out ./
ENTRYPOINT ["dotnet", "Com.Natoma.Adpq.Prototype.Api.dll"]

