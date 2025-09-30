# MovieCatalog API with Azure API Management (APIM)

This repository contains a sample **.NET 9 Web API** project
(`MovieCatalog.Api`) along with **Infrastructure as Code (Bicep)** and
**GitHub Actions workflows** to deploy it to **Azure App Service** and
front it with **Azure API Management (APIM)**.

------------------------------------------------------------------------

## 📂 Project Structure

    APIM/
    ├── MovieCatalog.Api/          # .NET 9 Web API (Movies CRUD)
    ├── infra/                     # Bicep templates for App Service + APIM
    │   └── main.bicep
    ├── .github/                   # GitHub Actions workflows
    │   └── workflows/
    │       ├── infra.yml          # Infra provisioning (App Service, APIM)
    │       └── api.yml            # CI/CD for API
    ├── MovieCatalog.sln           # Solution file
    └── README.md

------------------------------------------------------------------------

## 🚀 Getting Started

### Prerequisites

-   [.NET 9 SDK](https://dotnet.microsoft.com/download)
-   [Azure CLI](https://learn.microsoft.com/cli/azure/install-azure-cli)
-   Azure Subscription
-   GitHub repo with Actions enabled

### Run locally

``` bash
cd MovieCatalog.Api
dotnet run
```

Swagger UI: <https://localhost:5001/swagger>

------------------------------------------------------------------------

## ☁️ Deployment

### Infrastructure (manual trigger)

Infra is provisioned with **Bicep** and deployed via GitHub Actions
(`infra.yml`).

1.  Create a Service Principal and add its JSON output as GitHub secret
    `AZURE_CREDENTIALS`.
2.  Run the `Provision Infrastructure` workflow from GitHub Actions tab.

This provisions: - Resource Group: `MoviePlatformRG` - App Service Plan
(F1) - Web App (Linux, .NET 9) - API Management (Consumption tier)

### CI/CD for API

The `api.yml` workflow builds, tests, and deploys the API automatically
when changes are pushed to `main`.

Secrets required: - `AZURE_CREDENTIALS` → Service Principal JSON -
`AZURE_WEBAPP_PUBLISH_PROFILE` → From
`az webapp deployment list-publishing-profiles`

------------------------------------------------------------------------

## 🔑 API Endpoints

Base URL (after deploy to App Service):

    https://moviecatalogapi<yourname>.azurewebsites.net

Example endpoints: 
- `GET /api/movies` → List all movies 
- `GET /api/movies/{id}` → Get movie by ID 
- `POST /api/movies` → Create new movie 
- `PUT /api/movies/{id}` → Update movie 
- `DELETE /api/movies/{id}` → Delete movie

Swagger UI: `/swagger`

------------------------------------------------------------------------

## 🛡️ APIM Features

After importing the API into APIM: 
- Apply **policies**: 
    - Rate limiting (`<rate-limit calls="5" renewal-period="60"/>`) 
    - Header validation (`<check-header name="x-client-id" .../>`) 
- Response caching 
- Secure with **Azure AD / OAuth2** or **subscription keys** 
- Monitor with **Application Insights**

------------------------------------------------------------------------

## 📊 Architecture Diagram

``` mermaid
flowchart LR
    Client[Client Apps] --> APIM[Azure API Management]
    APIM -->|Policies, Security, Caching| AppService[Azure App Service: MovieCatalog API]
    AppService --> DB[(In-Memory Repo or Future DB)]
    APIM --> DevPortal[Developer Portal]
```


------------------------------------------------------------------------

## 📜 License

MIT
