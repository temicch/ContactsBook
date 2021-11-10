FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContactsBook.WebApi/ContactsBook.WebApi.csproj", "ContactsBook.WebApi/"]
COPY ["ContactsBook.Application/ContactsBook.Application.csproj", "ContactsBook.Application/"]
COPY ["ContactsBook.Application.Interfaces/ContactsBook.Application.Interfaces.csproj", "ContactsBook.Application.Interfaces/"]
COPY ["ContactsBook.Domain/ContactsBook.Domain.csproj", "ContactsBook.Domain/"]
COPY ["ContactsBook.Utils/ContactsBook.Utils.csproj", "ContactsBook.Utils/"]
COPY ["ContactsBook.Infrastructure.Interfaces/ContactsBook.Infrastructure.Interfaces.csproj", "ContactsBook.Infrastructure.Interfaces/"]
COPY ["ContactsBook.DataAccess/ContactsBook.DataAccess.MsSql.csproj", "ContactsBook.DataAccess/"]
RUN dotnet restore "ContactsBook.WebApi/ContactsBook.WebApi.csproj"
COPY . .
WORKDIR "/src/ContactsBook.WebApi"
RUN dotnet build "ContactsBook.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactsBook.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactsBook.WebApi.dll"]