FROM microsoft/aspnetcore:1.0.1
#FROM microsoft/dotnet
ENV ASPNETCORE_ENVIRONMENT Production
WORKDIR /app
COPY out ./
EXPOSE 5000
ENTRYPOINT ["dotnet", "Com.Natoma.Adpq.Prototype.Web.dll"]
