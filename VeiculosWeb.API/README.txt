dotnet restore --Rodar antes de qualquer comando scaffold
Microsoft.NETCore.App
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.Design


Para criar migration:
dotnet ef migrations add 1 -p VeiculosWeb.Persistence -s VeiculosWeb.API -c VeiculosWebContext --verbose