#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /MWF.Blog
COPY ["./MWF.Blog.Host/MWF.Blog.Host.csproj", "./MWF.Blog.Host/"]
COPY ["./MWF.Blog.Application/MWF.Blog.Application.csproj", "./MWF.Blog.Application/"]
COPY ["./MWF.Blog.Domain/MWF.Blog.Domain.csproj", "./MWF.Blog.Domain/"]
COPY ["./MWF.Blog.Infraestructure/MWF.Blog.Infraestructure.csproj", "./MWF.Blog.Infraestructure/"]
RUN dotnet restore "./MWF.Blog.Host/MWF.Blog.Host.csproj"
COPY . .
#Deve ser modificado: 
WORKDIR "/MWF.Blog/MWF.Blog.Host" 
RUN dotnet build "MWF.Blog.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MWF.Blog.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MWF.Blog.Host.dll"]