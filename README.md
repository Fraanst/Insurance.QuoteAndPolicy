# 🛡️ Plataforma de Seguros - MS Proposta e MS Contratação

<p>
  <img src="https://img.shields.io/badge/Architecture-Hexagonal-blueviolet" alt="Hexagonal Architecture Badge">
  <img src="https://img.shields.io/badge/Framework-.NET%2010-512BD4" alt=".NET 10 Badge">
  <img src="https://img.shields.io/badge/Containerization-Docker%20Compose-2496ED" alt="Docker Compose Badge">
  <img src="https://img.shields.io/badge/Cloud%20Simulation-LocalStack-F0AD4E" alt="LocalStack Badge">
</p>

Esta solução implementa um sistema de microserviços para gerenciamento de propostas de seguro e seu processo de contratação, seguindo os princípios de **Domain-Driven Design (DDD)**, **Arquitetura Hexagonal (Ports & Adapters)** e **Clean Code**.

---

## 1. 🏗️ Arquitetura da Solução

O ambiente é orquestrado via Docker Compose, simulando um ecossistema de microserviços em um ambiente AWS local, com bancos de dados isolados e mensageria assíncrona.

### 1.1 Princípios Arquiteturais

| Princípio | Aplicação |
| :---: | :--- |
| **Arquitetura Hexagonal** | Cada microserviço é dividido em camadas (Domain, Application, Infrastructure). O Core (Domain/Application) é isolado, comunicando-se com o mundo externo (APIs, DBs, AWS) apenas através de **Ports** (interfaces no Domain) e **Adapters** (implementações na Infrastructure). |
| **DDD/Clean Code/SOLID** | Uso de Aggregates, Entities e Value Objects na camada Domain, garantindo que as regras de negócio sejam independentes da tecnologia. O princípio **D de SOLID (Inversão de Dependência)** é aplicado na comunicação entre serviços (Porta `IQuoteServicePort`). |

### 1.2 Estrutura de Microserviços 

<table width="100%">
  <thead>
    <tr>
      <th>Serviço</th>
      <th>Nome da API</th>
      <th>Responsabilidades</th>
      <th>Comunicação Externa</th>
      <th>Porta Local</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><b>Proposta (QuoteService)</b></td>
      <td><code>Insurance.Quote.Api</code></td>
      <td>Criação, listagem e alteração de status de propostas (Em Análise, Aprovada, Rejeitada).</td>
      <td>PostgreSQL (DB Próprio), <b>LocalStack (SNS Adapter)</b></td>
      <td><code>7150</code></td>
    </tr>
    <tr>
      <td><b>Contratação (PolicyService)</b></td>
      <td><code>Insurance.Policy.Api</code></td>
      <td>Contratação de propostas aprovadas e persistência da contratação.</td>
      <td>PostgreSQL (DB Próprio), <b>QuoteService (HTTP Adapter)</b></td>
      <td><code>7151</code></td>
    </tr>
  </tbody>
</table>

---

## 2. 💻 Tecnologias e Infraestrutura

* **Linguagem/Framework:** C# / .NET 10.
* **Contêineres:** Docker / Docker Compose.
* **Bancos de Dados:** PostgreSQL 15 (<code>quote-db</code> e <code>policy-db</code>).
* **Mensageria:** **LocalStack** (Simulador AWS) para o serviço **SNS (Simple Notification Service)**.

---

## 3. ⚙️ Requisitos Prévios

Para executar o ambiente, certifique-se de ter instalado:

1.  **Git**
2.  **.NET SDK 10** (para o ambiente de desenvolvimento local).
3.  **Docker Desktop** (com Docker Compose v3.8+ ativo).
4.  Um terminal **Linux-like** (Git Bash, PowerShell ou Terminal).

---

## 4. 🚀 Instruções de Execução (Docker Compose)

O ambiente completo (APIs e Infraestrutura) é iniciado com um único comando.

### Passo 4.1: Setup Inicial

1.  **Clone o repositório** e navegue até o diretório raiz da solução:
    ```bash
    git clone https://github.com/Fraanst/Insurance.QuoteAndPolicy)](https://github.com/Fraanst/Insurance.QuoteAndPolicy.git
    cd Insurance.QuoteAndPolicy 
    ```

2.  **Conceda permissão de execução** ao script de inicialização do LocalStack:
    * Este comando é obrigatório para que o LocalStack crie o tópico SNS (`quote-approved-topic`) antes de a API ser iniciada.
    ```bash
    chmod +x localstack/init-sns.sh
    ```

### Passo 4.2: Build e Inicialização

Execute o Docker Compose para fazer o *build* das APIs e iniciar todos os contêineres:

```bash
docker-compose up --build
```

### ⚠️ Sequência de Inicialização Crítica (Orquestração Garantida)
Ao rodar o docker-compose up, os serviços são iniciados na seguinte ordem automática, garantindo a integridade dos dados:

quote-db e policy-db (Bancos de Dados) iniciam.

quote-migrator e policy-migrator rodam: Instalam o dotnet-ef dentro do contêiner e aplicam todas as Migrations pendentes.

quote-seeder rodam (após o quote-migrator): Injeta o Customer e o Product iniciais no quote-db.

quote-api e policy-api (APIs) iniciam e ficam prontas para aceitar requisições.

#### A Aplicação adiciona um Produto e um Cliente para que seja possível adicionar uma Proposta

### Acesso e Teste da API
Após a inicialização completa (os logs param de mostrar atividade de *-migrator e *-seeder), as APIs estão acessíveis via localhost:

<table width="100%">
  <thead>
    <tr>
      <th>Serviço</th>
      <th>Porta</th>
      <th>EndPoint Swagger</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><b>Proposta (Quote API)</b></td>
      <td><code>7150</code></td>
      <td>http://localhost:7150/swagger/index.html.</td>
    </tr>
    <tr>
      <td><b>Contratação (Policy API)</b></td>
      <td><code>7151</code></td>
      <td>http://localhost:7151/swagger/index.htm</td>
    </tr>
  </tbody>
</table>


### 📝 Guia de Testes da Plataforma Insurance 

Este guia explica o **fluxo de testes de ponta a ponta** cobrindo a criação de uma **Proposta** (`Quote`) até a **Criação de um Contrato** (`Policy`).

O fluxo completo é dividido em três etapas essenciais, acessadas via Swagger das respectivas APIs.

---

### 🚀 Pré-Requisitos

Certifique-se de que todo o ambiente de microsserviços está rodando e acessível.

* **Docker:** Todos os contêineres (`quote-api`, `policy-api`, `quote-db`, `policy-db`, `localstack`) devem estar **`Up`** (ativos).
---

## 1. Etapa: Criação da Proposta (API de Quote)

O primeiro passo é gerar uma nova proposta de seguro.

### 1.1. 💾 Enviar Requisição de Criação

Você usará o *endpoint* `POST /quote` para iniciar a cotação.

| API | URL (Swagger) | Método | Rota |
| :--- | :--- | :--- | :--- |
| **Quote API** | `http://localhost:7150/swagger` | `POST` | `/api/v1/Quote` |

> ℹ️ **Observação de Teste (Simplificação):**
> Você **não precisa se preocupar** em criar o `Customer` ou o `Product` separadamente. O *Service Layer* da `Quote API` é responsável por receber os dados do cliente e produto na requisição e **criar/persistir** essas entidades automaticamente antes de gerar o `QuoteId`.

> Exemplo de Payload
```bash
{
  "insuranceType": "auto",
  "status": 0,
  "estimatedValue": 200
}
```

### 1.2. 🎯 Verificação do Resultado

| Item | Detalhe |
| :--- | :--- |
| **Status Code** | Espere um `200 OK`. |
| **Corpo da Resposta** | Receberá o objeto `QuoteResponse` contendo o `QuoteId` gerado (um GUID). |
| **Ação** | **Copie o `QuoteId`**. Ele será necessário nas etapas 2 e 3. |

---

## 2. Etapa: Aprovação da Proposta (API de Quote)

Para que uma proposta possa se tornar um contrato (Apólice), ela deve estar em um *status* de **Aprovada**.

### 2.1. ⚙️ Alterar o Status

Você usará a rota de alteração de *status* da `Quote API`.

| API | URL (Swagger) | Método | Rota |
| :--- | :--- | :--- | :--- |
| **Quote API** | `http://localhost:7150/swagger` | `PATCH` | `/api/v1/Quote/{quoteId}/status` |

| Parâmetro | Tipo | Ação |
| :--- | :--- | :--- |
| **`quoteId`** (URL) | `GUID` | Cole o `QuoteId` copiado na Etapa 1. |
| **`newStatus`** (Body) | `int` | Envie o valor do *enum* que representa o status **Aprovado** (Ex: `1` para "Aprovado"`). |

### 2.2. 🎯 Verificação do Resultado

* **Status Code:** Espere um `200 OK`.
* A proposta está agora marcada como apta para contratação no banco de dados da `Quote API`.

---

## 3. Etapa: Criação da Apólice / Contrato (API de Policy)

O passo final é enviar a proposta aprovada para a `Policy API`, que é responsável por emitir a apólice.

### 3.1. 📨 Enviar Proposta Aprovada

Você enviará o `QuoteId` para a `Policy API`, que deverá buscar a proposta aprovada na `Quote API` (comunicação Síncrona) e criar o contrato.

| API | URL (Swagger) | Método | Rota |
| :--- | :--- | :--- | :--- |
| **Policy API** | `http://localhost:7151/swagger` | `POST` | `/api/v1/Policy` |

| Parâmetro | Tipo | Ação |
| :--- | :--- | :--- |
| **`QuoteId`** (Body) | `GUID` | **Cole o `QuoteId`** que você copiou na Etapa 1. |

### 3.2. 🎯 Verificação do Resultado

* **Status Code:** Espere um **`201 Created`**.
* **Corpo da Resposta:** Você receberá um objeto `PolicyResponse`.
* Isso confirma que o fluxo completo de comunicação e persistência foi concluído com sucesso.

---

### 🐛 Próximo Passo

Se você encontrar erros durante este fluxo, verifique os logs dos contêineres para diagnosticar falhas de comunicação ou persistência:

```bash
docker logs quote-api
docker logs policy-api
```

---

#### Diagrama 

<img width="787" height="465" alt="image" src="https://github.com/user-attachments/assets/f123bf7b-3624-4dce-b8ba-6d983783a1b3" />


---
