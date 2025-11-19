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
