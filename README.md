# prueba-tecnica

Para ejectuar el projecto se debe tener instalado minimo la version .net 8. Ejecutar los comandos comandos siguientes en su respectiva terminal:
```shell
dotnet run src/dependencies/ExRateApiJson/ExRateApiJson.csproj
dotnet run src/dependencies/ExRateApiXml/ExRateApiXml.csproj
dotnet run src/dependencies/ExRateApiFancyJson/ExRateApiFancyJson.csproj
```

Una vez que esten en ejecucion las apis, podemos ejecutar la aplicacion principal que funciona como un aggregator y consume del resto de apis:
```shell
dotnet run src/ExchangeRate/ExchangeRate.csproj
```

La aplicacion se estara ejecutando en el puerto `28541` del localjost y para acceder al swagger solo tendrian que seguir esta url: http://localhost:28541/swagger
