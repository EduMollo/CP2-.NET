# 📊 Esquema Físico do Banco de Dados

## Informações Gerais

- **SGBD**: MySQL 8.0+
- **Provider EF Core**: Pomelo.EntityFrameworkCore.MySql 9.0.0
- **Framework**: .NET 9.0
- **Data de Criação**: 16/04/2026

---

## Tabelas

### TB_LOJA

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| ID | CHAR(36) | PK, NOT NULL | PK_LOJA_ID |
| NOME_LOJA | VARCHAR(255) | NOT NULL | - |
| CNPJ_LOJA | VARCHAR(18) | NOT NULL, UNIQUE | IX_LOJA_CNPJ |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_LOJA_ID (ID)  
**Índices**: 
- IX_LOJA_CNPJ (UNIQUE em CNPJ_LOJA)

---

### TB_ESTOQUE

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| ID | CHAR(36) | PK, NOT NULL | PK_ESTOQUE_ID |
| QNTD_ESTOQ | INT | NOT NULL | - |
| VALIDADE_ESTOQ | DATE | NULL (opcional) | - |
| LOJA_ID | CHAR(36) | NOT NULL, FK | IX_ESTOQUE_LOJA |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_ESTOQUE_ID (ID)  
**Chave Estrangeira**: FK_ESTOQUE_LOJA (LOJA_ID → TB_LOJA.ID) ON DELETE CASCADE  
**Índices**:
- IX_ESTOQUE_LOJA (LOJA_ID)

---

### TB_CLIENTE

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| ID | CHAR(36) | PK, NOT NULL | PK_CLIENTE_ID |
| NOME_CLIENTE | VARCHAR(255) | NOT NULL | - |
| TELEFONE_CLIENTE | VARCHAR(20) | NULL (opcional) | - |
| CPF_CLIENTE | VARCHAR(15) | NOT NULL, UNIQUE | IX_CLIENTE_CPF |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_CLIENTE_ID (ID)  
**Índices**:
- IX_CLIENTE_CPF (UNIQUE em CPF_CLIENTE)

---

### TB_PRODUTO

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| ID | CHAR(36) | PK, NOT NULL | PK_PRODUTO_ID |
| NOME_PROD | VARCHAR(255) | NOT NULL | - |
| PRECO_PROD | DECIMAL(10,2) | NOT NULL | - |
| CLIENTE_ID | CHAR(36) | NULL (opcional), FK | IX_PRODUTO_CLIENTE |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_PRODUTO_ID (ID)  
**Chave Estrangeira**: FK_PRODUTO_CLIENTE (CLIENTE_ID → TB_CLIENTE.ID) ON DELETE SET NULL  
**Índices**:
- IX_PRODUTO_CLIENTE (CLIENTE_ID)

---

### TB_CATEGORIA

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| ID | CHAR(36) | PK, NOT NULL | PK_CATEGORIA_ID |
| NOME_CATEGORIA | VARCHAR(255) | NOT NULL | - |
| DESC_CATEGORIA | VARCHAR(1000) | NULL (opcional) | - |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_CATEGORIA_ID (ID)

---

### TB_ESTOQUE_PRODUTO *(N:N)*

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| ESTOQUE_ID | CHAR(36) | PK, FK, NOT NULL | IX_ESTOQUE_PRODUTO_ESTOQUE |
| PRODUTO_ID | CHAR(36) | PK, FK, NOT NULL | IX_ESTOQUE_PRODUTO_PRODUTO |
| ID | CHAR(36) | NOT NULL | - |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_ESTOQUE_PRODUTO (ESTOQUE_ID, PRODUTO_ID)  
**Chaves Estrangeiras**:
- FK_ESTOQUE_PRODUTO_ESTOQUE (ESTOQUE_ID → TB_ESTOQUE.ID) ON DELETE CASCADE
- FK_ESTOQUE_PRODUTO_PRODUTO (PRODUTO_ID → TB_PRODUTO.ID) ON DELETE CASCADE

**Índices**:
- IX_ESTOQUE_PRODUTO_ESTOQUE (ESTOQUE_ID)
- IX_ESTOQUE_PRODUTO_PRODUTO (PRODUTO_ID)

---

### TB_CATEGORIA_PRODUTO *(N:N)*

| Coluna | Tipo | Restrições | Índices |
|--------|------|-----------|---------|
| CATEGORIA_ID | CHAR(36) | PK, FK, NOT NULL | IX_CATEGORIA_PRODUTO_CATEGORIA |
| PRODUTO_ID | CHAR(36) | PK, FK, NOT NULL | IX_CATEGORIA_PRODUTO_PRODUTO |
| ID | CHAR(36) | NOT NULL | - |
| CRIADO_EM | DATETIME(6) | NOT NULL, DEFAULT CURRENT_TIMESTAMP(6) | - |
| ATIVO | TINYINT(1) | NOT NULL, DEFAULT 1 | - |

**Chave Primária**: PK_CATEGORIA_PRODUTO (CATEGORIA_ID, PRODUTO_ID)  
**Chaves Estrangeiras**:
- FK_CATEGORIA_PRODUTO_CATEGORIA (CATEGORIA_ID → TB_CATEGORIA.ID) ON DELETE CASCADE
- FK_CATEGORIA_PRODUTO_PRODUTO (PRODUTO_ID → TB_PRODUTO.ID) ON DELETE CASCADE

**Índices**:
- IX_CATEGORIA_PRODUTO_CATEGORIA (CATEGORIA_ID)
- IX_CATEGORIA_PRODUTO_PRODUTO (PRODUTO_ID)

---

## 🔄 Relacionamentos

```
      TB_LOJA (1)
         │
         ├─────────────────────┐
         │ 1:N                 │
         ▼                     │
    TB_ESTOQUE (N)             │
         │                     │
         │ N:N (via TB_ESTOQUE_PRODUTO)
         │                     │
         ├─────────────────────┤
         │                     │
         ▼                     │
    TB_PRODUTO (N) ◄───────────┤
         │                     │
         ├─ N:1 (opcional)     │
         │                     │
         ▼                     │
    TB_CLIENTE (1) ◄───────────┤
         │                     │
         │ N:N (via TB_CATEGORIA_PRODUTO)
         │                     │
         ▼                     │
     TB_CATEGORIA (N) ◄────────┘
```

---

## 📝 Mudanças do CP1 para CP2

### Adições

✅ Mapeamento **Fluent API** completo com tipos de dados explícitos  
✅ Indexes em Foreign Keys e campos UNIQUE  
✅ Valores padrão (DEFAULT) para CRIADO_EM e ATIVO  
✅ Nomes explícitos para constraints (PK, FK, indexes)  
✅ Coluna ATIVO (soft-delete) em todas as tabelas  
✅ Migrations versionadas e reproduzíveis  

### Mudanças Estruturais

- **Antes**: Modelo apenas em memória
- **Depois**: Esquema físico completo no MySQL
- **Antes**: N:N implícito
- **Depois**: N:N explícito com tabelas de junção
- **ClienteId**: Agora nullable (Guid?) para refletir opcionalidade

---

## 🎯 Validação do Esquema

✅ Todas as tabelas possuem PK e FK explícitos  
✅ Relacionamentos N:N com chaves compostas  
✅ Campos opcionais como NULL  
✅ Índices para performance  
✅ Cascade delete apropriado  
✅ Soft-delete via coluna ATIVO  
✅ Auditoria básica via CRIADO_EM  

---

**Gerado automaticamente via Entity Framework Core Migrations**
