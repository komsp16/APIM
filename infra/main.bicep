param location string = resourceGroup().location
param appServicePlanName string = 'MovieCatalogPlan'
param webAppName string
param apiManagementName string = 'moviecatalogapim'

resource appServicePlan 'Microsoft.Web/serverfarms@2022-09-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'         // Free tier
    tier: 'Free'
  }
  kind: 'linux'
  properties: {
    reserved: true
  }
}

resource webApp 'Microsoft.Web/sites@2022-09-01' = {
  name: webAppName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    siteConfig: {
      linuxFxVersion: 'DOTNET|9.0'
    }
    httpsOnly: true
  }
}

resource apim 'Microsoft.ApiManagement/service@2022-08-01' = {
  name: apiManagementName
  location: location
  sku: {
    name: 'Consumption'   // Free serverless option
    capacity: 0
  }
  properties: {
    publisherEmail: 'kaumil@HomeLabsCom.onmicrosoft.com'
    publisherName: 'Kaumil Parekh'
  }
}
