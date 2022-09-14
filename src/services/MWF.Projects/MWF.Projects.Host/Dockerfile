#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /MWF.Projects
COPY ["./MWF.Projects.Host/MWF.Projects.Host.csproj", "./MWF.Projects.Host/"]
COPY ["./MWF.Projects.Application/MWF.Projects.Application.csproj", "./MWF.Projects.Application/"]
COPY ["./MWF.Projects.Domain/MWF.Projects.Domain.csproj", "./MWF.Projects.Domain/"]
COPY ["./MWF.Projects.Infraestructure/MWF.Projects.Infraestructure.csproj", "./MWF.Projects.Infraestructure/"]
RUN dotnet restore "./MWF.Projects.Host/MWF.Projects.Host.csproj"
COPY . .
#Deve ser modificado: 
WORKDIR "/MWF.Projects/MWF.Projects.Host" 
RUN dotnet build "MWF.Projects.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MWF.Projects.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MWF.Projects.Host.dll"]