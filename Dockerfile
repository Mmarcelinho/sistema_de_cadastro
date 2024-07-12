FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY src/ .

WORKDIR Backend/SistemaDeCadastro.Api

RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/sdk:8.0 
WORKDIR /app


COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "SistemaDeCadastro.Api.dll"]