# LojaVirtual — CP2 Persistência com EF Core, Mapeamento e Camada de Infraestrutura

## 📋 Integrantes do Grupo

| Nome | RM |
|-----|------|
| Carlos Alberto Guedes Neto | RM: 566022 |
| Eduardo Novaes Mollo       | RM: 561515 |
| Filippo Dal Medico Tolone  | RM: 562329 |

---

## 🎯 Domínio Escolhido

**Loja Virtual** — Sistema de gerenciamento de uma loja virtual, abrangendo lojas, estoques, produtos, categorias e clientes.

---

## 🗄️ Banco de dados utilizado

**MySQL 8.0+** — Banco de dados relacional escolhido para persistência, utilizando o provider `Pomelo.EntityFrameworkCore.MySql 9.0.0`.

---

## 📊 Entidades Modeladas

| Entidade      | PK | Atributos |
|---------------|-----|-----------|
| **Loja**      | `Id` (Guid)  | `NomeLoja` (string), `CnpjLoja` (string), `CreatedAt`, `Active` |
| **Estoque**   | `Id` (Guid)  | `QntdEstoq` (int), `ValidadeEstoq` (DateTime?), `LojaId` (FK), `CreatedAt`, `Active` |
| **Produto**   | `Id` (Guid)  | `NomeProd` (string), `PrecoProd` (decimal), `ClienteId` (FK?), `CreatedAt`, `Active` |
| **Categoria** | `Id` (Guid)  | `NomeCategoria` (string), `DescCategoria` (string?), `CreatedAt`, `Active` |
| **Cliente**   | `Id` (Guid)  | `NomeCliente` (string), `TelefoneCliente` (string?), `CpfCliente` (string), `CreatedAt`, `Active` |
| **EstoqueProduto** | `(EstoqueId, ProdutoId)` | Chave composta N:N |
| **CategoriaProduto** | `(CategoriaId, ProdutoId)` | Chave composta N:N |

---

## 🔗 Relacionamentos

| Relacionamento | Cardinalidade | Opcionalidade | Descrição |
|-------------|-------------|-------------|-----------|
| Loja ➜ Estoque | 1:N | Obrigatório | Uma loja possui um ou mais estoques. |
| Estoque ⟷ Produto (via EstoqueProduto) | N:N | Obrigatório | Muitos-para-muitos explícito. |
| Categoria ⟷ Produto (via CategoriaProduto) | N:N | Opcional | Um produto pode não ter categorias. |
| Produto ➜ Cliente | N:1 | Opcional | Um produto pode ou não ter cliente comprador. |

---

## 🏗️ Clean Architecture — Estrutura do Projeto

```
CP1-.Net-Carlos-Alberto-Eduardo-Mollo-e-Filippo-Tolone/
├── src/
│   ├── LojaVirtual.Domain/                 (Camada de Domínio)
│   │   ├── Commons/
│   │   │   └── BaseEntity.cs               (Classe base: Id, CreatedAt, Active)
│   │   └── Entities/                       (Entidades de negócio)
│   │       ├── Loja.cs
│   │       ├── Estoque.cs
│   │       ├── Produto.cs
│   │       ├── Cliente.cs
│   │       ├── Categoria.cs
│   │       ├── EstoqueProduto.cs
│   │       └── CategoriaProduto.cs
│   │
│   ├── LojaVirtual.Application/            (Camada de Aplicação)
│   │   └── Repositories/                   (Interfaces de Repositório)
│   │       ├── IRepository.cs              (Interface genérica)
│   │       ├── ILojaRepository.cs
│   │       ├── IEstoqueRepository.cs
│   │       ├── IProdutoRepository.cs
│   │       ├── IClienteRepository.cs
│   │       └── ICategoriaRepository.cs
│   │
│   ├── LojaVirtual.Infrastructure/         (Camada de Infraestrutura)
│   │   ├── Persistance/
│   │   │   ├── LojaVirtualContext.cs       (DbContext)
│   │   │   ├── DesignTimeDbContextFactory.cs (Factory para migrations)
│   │   │   ├── Configurations/             (Fluent API Mappings)
│   │   │   │   ├── LojaConfiguration.cs
│   │   │   │   ├── EstoqueConfiguration.cs
│   │   │   │   ├── ProdutoConfiguration.cs
│   │   │   │   ├── ClienteConfiguration.cs
│   │   │   │   ├── CategoriaConfiguration.cs
│   │   │   │   ├── EstoqueProdutoConfiguration.cs
│   │   │   │   └── CategoriaProdutoConfiguration.cs
│   │   │   ├── Repositories/               (Implementações)
│   │   │   │   ├── Repository.cs           (Base genérica)
│   │   │   │   ├── LojaRepository.cs
│   │   │   │   ├── EstoqueRepository.cs
│   │   │   │   ├── ProdutoRepository.cs
│   │   │   │   ├── ClienteRepository.cs
│   │   │   │   └── CategoriaRepository.cs
│   │   │   └── Migrations/                 (Histórico de versões DB)
│   │   │       ├── 20260416221542_InitialCreate.cs
│   │   │       ├── 20260416221542_InitialCreate.Designer.cs
│   │   │       └── LojaVirtualContextModelSnapshot.cs
│   │   └── LojaVirtual.Infrastructure.csproj
│   │
│   └── LojaVirtual.Api/                    (Camada de API / Startup)
│       ├── Program.cs                      (DI Container Configuration)
│       ├── appsettings.Development.json    (Connection String)
│       └── LojaVirtual.Api.csproj
│
├── docs/                                   (Documentação)
│   ├── mer.jpeg                            (Diagrama MER — CP1)
│   ├── ESQUEMA_FISICO.md                   (Documentação do esquema físico)
│   └── SETUP_BD.md                         (Instruções de setup do BD)
│
└── README.md
```

---

## 🛠️ Mapeamento com Fluent API

Todas as entidades foram mapeadas explicitamente usando `IEntityTypeConfiguration<T>` na pasta `Persistance/Configurations/`:

✅ **Tipos de dados**: VARCHAR, DECIMAL, INT, DATE com tamanhos apropriados  
✅ **Chaves primárias e estrangeiras**: Nomeadas explicitamente  
✅ **Índices**: Criados para FK e campos únicos (CNPJ, CPF)  
✅ **Relacionamentos N:N**: Mapeados com chaves compostas explícitas  
✅ **Opcionalidade**: Respeitada Para campos opcionais  
✅ **Valores padrão**: `CreatedAt` (CURRENT_TIMESTAMP(6)), `Active` (true)  
✅ **Cascade delete**: Configurado apropriadamente para N:N  

---

## 🚀 Dependency Injection (DI)

Todas as dependências estão registradas em `Program.cs`:

```csharp
// DbContext
builder.Services.AddDbContext<LojaVirtualContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Repositórios
builder.Services.AddScoped<ILojaRepository, LojaRepository>();
builder.Services.AddScoped<IEstoqueRepository, EstoqueRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
```

---

## 📦 Pacotes NuGet Utilisados

| Pacote | Versão | Finalidade |
|--------|--------|------------|
| `Microsoft.EntityFrameworkCore` | 9.0.14 | ORM principal |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.14 | Ferramentas de migrations |
| `Pomelo.EntityFrameworkCore.MySql` | 9.0.0 | Provider MySQL |

---

## 🚀 Como Usar

### 📋 Pré-requisitos

- **.NET SDK 9.0+**
- **MySQL 8.0+** (instalado e rodando)
- **dotnet-ef** (instalado globalmente)

```bash
dotnet tool install --global dotnet-ef
```

### 🔧 Setup Inicial

1. **Clone o repositório**
   ```bash
   git clone <link-do-repositorio>
   cd CP1-.Net-Carlos-Alberto-Eduardo-Mollo-e-Filippo-Tolone
   ```

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Configure a conexão do banco** (appsettings.Development.json):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=127.0.0.1;Port=3306;Database=LojaVirtual;User=root;Password=;SslMode=None;"
     }
   }
   ```

4. **Aplique as migrations**
   ```bash
   cd src/LojaVirtual.Api
   dotnet ef database update --project ../LojaVirtual.Infrastructure
   ```

5. **Execute a aplicação**
   ```bash
   dotnet run
   ```

A API estará disponível em `https://localhost:5000`.

---

## 📚 Repositórios e Interfaces

### IRepository<T> (Genérico)

```csharp
Task<T?> GetByIdAsync(Guid id);
Task<IReadOnlyCollection<T>> GetAllAsync();
Task<IReadOnlyCollection<T>> GetAllActiveAsync();
Task AddAsync(T entity);
Task UpdateAsync(T entity);
Task DeleteAsync(Guid id);
Task SaveChangesAsync();
```

### ILojaRepository

```csharp
Task<Loja?> GetByPKCnpjAsync(string cnpj);
Task<IReadOnlyCollection<Loja>> GetComEstoquesAsync();
```

### IEstoqueRepository

```csharp
Task<IReadOnlyCollection<Estoque>> GetEstoquesPorLojaAsync(Guid lojaId);
Task<IReadOnlyCollection<Estoque>> GetEstoquesValidosAsync();
Task<IReadOnlyCollection<Estoque>> GetComProdutosAsync();
```

### IProdutoRepository

```csharp
Task<Produto?> GetProdutoComEstoquesAsync(Guid produtoId);
Task<Produto?> GetProdutoComCategoriasAsync(Guid produtoId);
Task<Produto?> GetProdutoComEstoquesECategoriasAsync(Guid produtoId);
Task<IReadOnlyCollection<Produto>> GetProdutosPorClienteAsync(Guid clienteId);
Task<IReadOnlyCollection<Produto>> GetProdutosPorCategoriaAsync(Guid categoriaId);
Task<IReadOnlyCollection<Produto>> GetProdutosPorEstoqueAsync(Guid estoqueId);
```

### IClienteRepository

```csharp
Task<Cliente?> GetByCpfAsync(string cpf);
Task<Cliente?> GetClienteComProdutosAsync(Guid clienteId);
Task<IReadOnlyCollection<Cliente>> GetClientesComProdutosAsync();
```

### ICategoriaRepository

```csharp
Task<Categoria?> GetCategoriaComProdutosAsync(Guid categoriaId);
Task<IReadOnlyCollection<Categoria>> GetCategoriasComProdutosAsync();
```

---

## 📄 Migrations

**Migration inicial:** `20260416221542_InitialCreate`

Para **criar uma nova migration** após modificar as entidades:

```bash
cd src/LojaVirtual.Api
dotnet ef migrations add <NomeDaMigration> --project ../LojaVirtual.Infrastructure
```

Para **remover a última migration**:

```bash
dotnet ef migrations remove --project ../LojaVirtual.Infrastructure
```

---

## 🔐 Segurança

✅ **Connection strings não estão commitadas** — use `appsettings.Development.json` local  
✅ **Senhas não estão no código** — configure via `User Secrets` ou variáveis de ambiente  
✅ **Nenhuma credencial real no repositório**

---

## 🌟 Checklist de Entrega

- ✅ Projeto compila sem erros
- ✅ DbContext criado na Infrastructure
- ✅ Mapeamentos de entidades concluídos com Fluent API
- ✅ Relações N:N mapeadas explicitamente
- ✅ Migration inicial criada e testada
- ✅ Repositórios com interface/implementação em camadas corretas
- ✅ DI configurado no Program.cs
- ✅ README.md atualizado com instruções completas
- ✅ Pasta `/docs` com documentação
- ✅ Repositório sem segredos

---

## 📝 Próximas Etapas (CP3+)

- Implementar casos de uso na camada Application
- Criar DTOs para validação e transferência de dados
- Implementar autenticação e autorização
- Adicionar testes unitários
- Documentar endpoints da API

---

## 📎 Relação com o CP1

| CP1 | CP2 |
|-----|-----|
| MER + entidades C# | ✅ Esquema físico + EF Core + migrations |
| Sem banco de dados | ✅ Banco configurado e reproduzível |
| Sem persistência | ✅ Repositórios/UoW + DI |
| | ✅ Fluent API com mapeamentos explícitos |
| | ✅ N:N e opcionais documentados |

---

## 🏆 Conclusão

Este projeto implementa **persistência completa** seguindo princípios de **Clean Architecture**, com mapeamentos **Fluent API** explícitos, **migrations versionadas** e **acesso a dados por contrato** (interfaces de repositório).

---

**"Faça o teu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda"  
— Mario Sergio Cortella**
│   │       ├── Loja.cs
│   │       ├── Estoque.cs
│   │       ├── Produto.cs
│   │       ├── Categoria.cs
│   │       ├── Cliente.cs
│   │       ├── EstoqueProduto.cs       → Associativa (N:N)
│   │       └── CategoriaProduto.cs     → Associativa (N:N)
│   ├── LojaVirtual.Application/       → Camada de aplicação (casos de uso)
│   ├── LojaVirtual.Infrastructure/    → Camada de infraestrutura (acesso a dados)
│   └── LojaVirtual.Api/              → Camada de apresentação (WebAPI)
│       └── Program.cs
├── docs/
│   └── mer.jpeg                       → Diagrama MER
└── README.md
```

## Tecnologias

- **.NET 10** (WebAPI)
- **C#**
- Arquitetura: **Clean Architecture**
