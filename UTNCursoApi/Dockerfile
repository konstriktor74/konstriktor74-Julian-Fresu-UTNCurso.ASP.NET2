#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UTNCursoApi/UTNCursoApi.csproj", "UTNCursoApi/"]
COPY ["UTNCurso.BLL.Bootstrappers/UTNCurso.BLL.Bootstrappers.csproj", "UTNCurso.BLL.Bootstrappers/"]
COPY ["UTNCurso.Core/UTNCurso.Core.csproj", "UTNCurso.Core/"]
COPY ["UTNCurso.Infrastructure/UTNCurso.Infrastructure.csproj", "UTNCurso.Infrastructure/"]
COPY ["UTNCurso.BLL.Services/UTNCurso.BLL.Services.csproj", "UTNCurso.BLL.Services/"]
COPY ["UTNCurso.BLL.DTOs/UTNCurso.BLL.DTOs.csproj", "UTNCurso.BLL.DTOs/"]
COPY ["UTNCurso.BLL.POCOs/UTNCurso.BLL.POCOs.csproj", "UTNCurso.BLL.POCOs/"]
COPY ["UTNCurso.Common.Entities/UTNCurso.Common.Entities.csproj", "UTNCurso.Common.Entities/"]
COPY ["UTNCurso.DAL.EFCore/UTNCurso.DAL.EFCore.csproj", "UTNCurso.DAL.EFCore/"]
RUN dotnet restore "UTNCursoApi/UTNCursoApi.csproj"
COPY . .
WORKDIR "/src/UTNCursoApi"
RUN dotnet build "UTNCursoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UTNCursoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UTNCursoApi.dll"]