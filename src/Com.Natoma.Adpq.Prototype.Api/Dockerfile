FROM microsoft/aspnetcore:1.0.1
#FROM microsoft/dotnet
ENV ASPNETCORE_ENVIRONMENT Production
WORKDIR /app
EXPOSE 61552
COPY out ./
ENTRYPOINT ["dotnet", "Com.Natoma.Adpq.Prototype.Api.dll"]

