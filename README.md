# QuickPizza.ApiTests

NUnit + RestSharp API tests for the QuickPizza demo API.

## Requirements

- .NET SDK

## Configuration

Tests use these environment variables:

- `QUICKPIZZA_BASE_URL`
  - Default: `https://quickpizza.grafana.com`
- `QUICKPIZZA_TOKEN`
  - Default: `vlae9OngnvbiJia6`
  - Header format: `Authorization: Token <value>`

## Run

```bash
QUICKPIZZA_TOKEN="vlae9OngnvbiJia6" dotnet test
```

Optionally override base URL:

```bash
QUICKPIZZA_BASE_URL="https://quickpizza.grafana.com" QUICKPIZZA_TOKEN="vlae9OngnvbiJia6" dotnet test
```
