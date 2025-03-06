# Lagerstyringssystem – Systemspecifikation

## **1. Teknologier**
- **Sprog & Frameworks:**
  - C# (.NET 9)
  - ASP.NET Core Web API (REST API)
  - EF Core (Entity Framework Core) til databaseadgang

- **Persistenslag:**
  - **Production**: SQLite
  - **Test**: JSON-fil

- **Test & Kvalitetssikring:**
  - xUnit til enhedstests
  - Moq til mocking
  - Integrationstests af API-endpoints

- **NuGet-pakker:**
  - **Eksterne pakker:**  
    - `Microsoft.EntityFrameworkCore.Sqlite` (EF Core SQLite-understøttelse)  
    - `Newtonsoft.Json` (JSON-håndtering)  
    - `xunit` (Testframework)  
    - `Moq` (Mocking af afhængigheder)  
  - **Egenudviklet NuGet-pakke:**  
    - En del af applikationen, f.eks. **validering** eller en anden komponent, udvikles som en **egen NuGet-pakke**, der i første omgang **lægges lokalt og bruges i projektet**.

## **2. Systemfunktionalitet**
- CRUD-operationer for varer (oprette, læse, opdatere, slette)
- Observer-mekanisme til at overvåge ændringer i lagerstatus
- REST API til interaktion med systemet

## **3. Arkitektur**
- **Lagopdeling:**
  - **Præsentationslag**: Console UI eller Web UI (ASP.NET Core)
  - **API-lag**: ASP.NET Core Web API
  - **Forretningslogik-lag**: Lagerstyringslogik & observermønster
  - **Dataadgangslag**: EF Core + SQLite/JSON

## **4. Design Patterns**
- **Singleton**: Databaseadgang håndteres via en Singleton.
- **Observer**: Lagerstatus-ændringer overvåges af observere.
- **Factory Pattern**: Bruges til at oprette lagerrelaterede objekter, så implementeringsdetaljer skjules.
- **Dependency Injection (DI)**: Bruges til at administrere afhængigheder mellem komponenter, f.eks. forretningslogik og dataadgang.
