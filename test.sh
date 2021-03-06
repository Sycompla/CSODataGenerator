#rootDisk#
generatorDir="/home/d7p4n4/dotNETCore/CSODataGenerator/CSODataGenerator/bin/Debug/netcoreapp3.0/"
mainDir="/home/d7p4n4/CSharp/test/"
namespace="FBClasses"
planObjectPath=$mainDir'/'$namespace

cd $mainDir
dotnet new classlib -n $namespace -f netcoreapp3.1
cd $generatorDir
dotnet CSODataGenerator.dll "PlanObject"
cd $mainDir'/'$namespace
dotnet build

cd $mainDir
dotnet new classlib -n $namespace'Cap' -f netcoreapp3.1
cd $namespace'Cap'
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference $planObjectPath
cd $generatorDir
dotnet CSODataGenerator.dll 'Cap'
dotnet CSODataGenerator.dll "Context"
cd $mainDir'/'$namespace'Cap/'
dotnet build

cd $mainDir
dotnet new classlib -n $namespace'ObjectService' -f netcoreapp3.1
cd $namespace'ObjectService'
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference $mainDir'/'$namespace'Cap'
dotnet add reference $planObjectPath
cd $generatorDir
dotnet CSODataGenerator.dll "ObjectService"
cd $mainDir'/'$namespace'ObjectService/'
dotnet build

cd $mainDir
dotnet new mvc -n $namespace'ODataService' -f netcoreapp3.1
cd $namespace'ODataService'
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.OData
dotnet add package Microsoft.AspNet.StaticFiles
dotnet add package Microsoft.Data.OData
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference $mainDir'/'$namespace'Cap'
dotnet add reference $planObjectPath
dotnet add reference $mainDir'/'$namespace'ObjectService'
mkdir Document
cd $generatorDir
dotnet CSODataGenerator.dll "ODataController"
dotnet CSODataGenerator.dll "Csproj"
dotnet CSODataGenerator.dll "Kestrel"
dotnet CSODataGenerator.dll "Startup"
dotnet CSODataGenerator.dll "OpenApiDocument"

cd $mainDir'/'$namespace'ODataService/'
cd Document
npm install -g redoc-cli
redoc-cli bundle -o index.html OpenApiDocument.json

cd $mainDir
dotnet new mvc -n $namespace'UpsertService' -f netcoreapp3.1
cd $namespace'UpsertService'
dotnet add package Newtonsoft.Json
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference $planObjectPath
cd $generatorDir
dotnet CSODataGenerator.dll "UpsertController"
dotnet CSODataGenerator.dll "UpsertService"
dotnet CSODataGenerator.dll "UpsertResponse"
dotnet CSODataGenerator.dll "UpsertServiceStartup"
dotnet CSODataGenerator.dll "Ac4yRestServiceClient"
cd $mainDir'/'$namespace'UpsertService'
dotnet build

cd $mainDir'/'$namespace'ODataService/'
dotnet build