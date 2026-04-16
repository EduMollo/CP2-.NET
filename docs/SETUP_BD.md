# 🚀 Guia de Setup do Banco de Dados

## 📋 Pré-requisitos

- **MySQL 8.0+** (instalado e rodando)
- **.NET SDK 9.0+**
- **Git**
- **dotnet-ef** (ferramenta global)

---

## 1️⃣ Instalar Ferramentas

### Instalar dotnet-ef (global)

```bash
dotnet tool install --global dotnet-ef
```

Verifique a instalação:
```bash
dotnet ef --version
```

---

## 2️⃣ Configurar MySQL

### Windows

1. **Baixar MySQL**: https://dev.mysql.com/downloads/mysql/
2. **Executar** o instalador
3. **Configurar**:
   - Porta: `3306` (padrão)
   - Root password: deixe em branco ou configure conforme necessário
   - Iniciar como serviço

4. **Verificar**: Abra PowerShell como Admin:
   ```bash
   mysql -u root -p
   password: (deixe em branco ou digite a senha)
   ```

### Linux (Debian/Ubuntu)

```bash
sudo apt update
sudo apt install mysql-server
sudo mysql_secure_installation
```

### macOS (Homebrew)

```bash
brew install mysql
brew services start mysql
mysql -u root
```

---

## 3️⃣ Criar Banco de Dados (opcional)

Se preferir, crie o banco manualmente:

```sql
CREATE DATABASE LojaVirtual CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
```

Ou deixe o EF Core criar automaticamente (recomendado).

---

## 4️⃣ Configurar Connection String

Edite `src/LojaVirtual.Api/appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=LojaVirtual;User=root;Password=;SslMode=None;"
  }
}
```

### Variações de Connection String:

**Sem senha Root:**
```
Server=127.0.0.1;Port=3306;Database=LojaVirtual;User=root;Password=;SslMode=None;
```

**Com senha Root:**
```
Server=127.0.0.1;Port=3306;Database=LojaVirtual;User=root;Password=sua_senha_aqui;SslMode=None;
```

**Com usuário customizado:**
```
Server=127.0.0.1;Port=3306;Database=LojaVirtual;User=appuser;Password=AppPassword123;SslMode=None;
```

---

## 5️⃣ Aplicar as Migrations

### Clone o repositório

```bash
git clone <link-do-repositorio>
cd CP1-.Net-Carlos-Alberto-Eduardo-Mollo-e-Filippo-Tolone
```

### Restaure as dependências

```bash
dotnet restore
```

### Aplique as migrations

```bash
cd src/LojaVirtual.Api
dotnet ef database update --project ../LojaVirtual.Infrastructure
```

Você verá uma saída como:

```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (15ms) [Parameters=[], CommandTimeout=30]
      CREATE DATABASE [LojaVirtual]
...
Done.
```

### Verificar criação

```bash
mysql -u root
USE LojaVirtual;
SHOW TABLES;
```

Você deverá ver:

```
+------------------------------+
| Tables_in_LojaVirtual        |
+------------------------------+
| __EFMigrationsHistory         |
| TB_CATEGORIA                 |
| TB_CATEGORIA_PRODUTO         |
| TB_CLIENTE                   |
| TB_ESTOQUE                   |
| TB_ESTOQUE_PRODUTO           |
| TB_LOJA                      |
| TB_PRODUTO                   |
+------------------------------+
```

---

## 6️⃣ Executar a Aplicação

```bash
cd ../LojaVirtual.Api
dotnet run
```

A aplicação iniciará em: `https://localhost:5000`

---

## 🔄 Operações Comuns

### Criar nova migration após modificar entidades

```bash
cd src/LojaVirtual.Api
dotnet ef migrations add MeuNomeDeMigration --project ../LojaVirtual.Infrastructure
```

### Remover última migration

```bash
dotnet ef migrations remove --project ../LojaVirtual.Infrastructure
```

### Atualizar para uma migration específica

```bash
dotnet ef database update MeuNomeDeMigration --project ../LojaVirtual.Infrastructure
```

### Reverter para migração anterior

```bash
dotnet ef database update PreviousMigrationName --project ../LojaVirtual.Infrastructure
```

### Gerar script SQL sem aplicar

```bash
dotnet ef migrations script --output script.sql --project ../LojaVirtual.Infrastructure
```

---

## ❌ Troubleshooting

### Erro: "Unable to connect to any of the specified MySQL hosts"

**Solução**:
1. Verifique se MySQL está rodando
2. Verifique a connection string em `appsettings.Development.json`
3. Teste a conexão:
   ```bash
   mysql -u root -h 127.0.0.1
   ```

### Erro: "Database already exists"

**Solução**: 
1. Se a migration falhou no meio, verifique `__EFMigrationsHistory`:
   ```sql
   SELECT * FROM __EFMigrationsHistory;
   ```
2. Se necessário, delete o banco e recrie:
   ```bash
   dotnet ef database drop --project ../LojaVirtual.Infrastructure
   dotnet ef database update --project ../LojaVirtual.Infrastructure
   ```

### Erro: "dotnet-ef not found"

**Solução**:
```bash
dotnet tool install --global dotnet-ef --version 9.0.14
```

### Erro: Columndoesn't match Entity

**Solução**: Crie uma nova migration:
```bash
dotnet ef migrations add FixColumnName --project ../LojaVirtual.Infrastructure
dotnet ef database update --project ../LojaVirtual.Infrastructure
```

---

## 📊 Verificar o Banco Criado

### Listar tabelas

```sql
mysql> USE LojaVirtual;
mysql> SHOW TABLES;
mysql> DESC TB_LOJA;
mysql> DESC TB_PRODUTO;
```

### Contar registers

```sql
SELECT 'TB_LOJA' as table_name, COUNT(*) as rows FROM TB_LOJA
UNION
SELECT 'TB_ESTOQUE', COUNT(*) FROM TB_ESTOQUE
UNION
SELECT 'TB_PRODUTO', COUNT(*) FROM TB_PRODUTO
UNION
SELECT 'TB_CLIENTE', COUNT(*) FROM TB_CLIENTE
UNION
SELECT 'TB_CATEGORIA', COUNT(*) FROM TB_CATEGORIA;
```

---

## 🔐 Segurança

⚠️ **IMPORTANTE**: 

- Nunca commit `appsettings.Development.json` com senhas reais
- Use `User Secrets` para desenvolvimento local:
  ```bash
  dotnet user-secrets init --project src/LojaVirtual.Api
  dotnet user-secrets set ConnectionStrings:DefaultConnection "Server=127.0.0.1;..." --project src/LojaVirtual.Api
  ```

---

## ✅ Checklist de Setup

- ✅ MySQL instalado e rodando
- ✅ dotnet-ef instalado
- ✅ Connection string configurada
- ✅ Migrations aplicadas com sucesso
- ✅ Tabelas criadas no banco
- ✅ Aplicação roda sem erros
- ✅ API respondendo em localhost:5000

---

**Pronto para desenvolvimento! 🚀**
