#rootDisk#
dotnet tool install --global dotnet-ef

set generatorDir=%1
set mainDir=%2
set planObjectPath=%mainDir%%3
set namespace=%4

cd %maimnDir%
dotnet nmew classlib -n %3 -f netcoreapp3.1
cd %genereatorDir%
dotnet CSOnDataGenerator.dll "PlanObject" %5 %6
cd %mainDirt%\%planObjectNamespace%
dotnet build

cd %mainDir%
dotnet new classlib -n %namespace%Cap -f netcoreapp3.1
cd %namespace%Cap
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.FileExtensions
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference %planObjectPath%
cd %generatorDir%
dotnet CSODataGenerator.dll "Cap" %5 %6
dotnet CSODataGenerator.dll "Context" %5 %6
cd %mainDir%\%namespace%Cap\
dotnet build

cd %mainDir%
dotnet new classlib -n %namespace%ObjectService -f netcoreapp3.1
cd %namespace%ObjectService
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.FileExtensions
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference %mainDir%\%namespace%Cap
dotnet add reference %planObjectPath%
cd %generatorDir%
dotnet CSODataGenerator.dll "ObjectService" %5 %6
cd %mainDir%\%namespace%ObjectService\
dotnet build

cd %mainDir%
dotnet new mvc -n %namespace%ODataService -f netcoreapp3.1
cd %namespace%ODataService
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.OData
dotnet add package Microsoft.AspNet.StaticFiles
dotnet add package Microsoft.Data.OData
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.FileExtensions
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference %mainDir%\%namespace%Cap
dotnet add reference %planObjectPath%
dotnet add reference %mainDir%\%namespace%ObjectService

cd %generatorDir%
dotnet CSODataGenerator.dll "ODataController" %5 %6
dotnet CSODataGenerator.dll "Csproj" %5 %6
dotnet CSODataGenerator.dll "Kestrel" %5 %6
dotnet CSODataGenerator.dll "Startup" %5 %6
dotnet CSODataGenerator.dll "OpenApiDocument" %5 %6
dotnet CSODataGenerator.dll "Appsettings" %5 %6
dotnet CSODataGenerator.dll "Nuget" %5 %6

cd %mainDir%\%namespace%ODataService\
cd Document
call npm install -g redoc-cli
call redoc-cli bundle -o index.html OpenApiDocument.json

cd %mainDir%\%namespace%ODataService\
dotnet build

nuget pack Package.nuspec
nuget setApiKey oy2pnolipb2hd545bpdq4no54ggtexaujcvpxk4wyeqdka
nuget push %namespace%ODataService.%7.nupkg -Source https://api.nuget.org/v3/index.json

cd bin\Debug\netcoreapp3.1
start cmd /K %namespace%ODataService.exe
start https://localhost:5001/document/index.html





