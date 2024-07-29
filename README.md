# Introdução 
VeiculosWeb.Back

# Como executar o projeto?
docker run -d --name postgresql --restart always -e POSTGRES_PASSWORD=sua_senha -v /var/lib/postgresql/data:/var/lib/postgresql/data -p 5432:5432 postgres:latest
docker pull gustavo1rx7/VeiculosWeb.Back
docker run -e DatabaseConnection="Host=localhost;Port=5432;Username=postgres;Password=sua_senha;Database=db-veiculosweb-prod;Pooling=true;" --restart always -d --name veiculosweb-back -p 3000:8080 gustavo1rx7/VeiculosWeb.Back:latest

# Como criar uma migration?
dotnet ef migrations add Initial -p VeiculosWeb.Persistence -s VeiculosWeb.API -c VeiculosWebContext --verbose
