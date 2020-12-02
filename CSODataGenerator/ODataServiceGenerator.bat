set generatorDir=D:\Sycompla\
set mainDir=d:\Server\Visual_studio\CMDGeneration\CSCMDTry3
set planObjectPath=%mainDir%\CSODataEntityWithTwoCombo

cd %mainDir%
dotnet new classlib -n %1Cap -f %2
cd %1Cap
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference %planObjectPath%
cd %generatorDir%
CSODataGenerator.exe "Cap"
CSODataGenerator.exe "Context"
cd %mainDir%\%1Cap\
dotnet build
dotnet ef migrations add %1Migration
dotnet ef database update

cd %mainDir%
dotnet new classlib -n %1ObjectService -f %2
cd %1ObjectService
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference %mainDir%\%1Cap
dotnet add reference %planObjectPath%
cd %generatorDir%
CSODataGenerator.exe "ObjectService"
cd %mainDir%\%1ObjectService\
dotnet build

cd %mainDir%
dotnet new mvc -n %1ODataService -f %2
cd %1ODataService
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.OData
dotnet add package Microsoft.AspNet.StaticFiles
dotnet add package Microsoft.Data.OData
dotnet add package ReferencesToNuGet -v 1.2020.1015.3
dotnet add reference %mainDir%\%1Cap
dotnet add reference %planObjectPath%
dotnet add reference %mainDir%\%1ObjectService
cd %generatorDir%
CSODataGenerator.exe "ODataController"
CSODataGenerator.exe "Csproj"
CSODataGenerator.exe "Kestrel"
CSODataGenerator.exe "Startup"
CSODataGenerator.exe "OpenApiDocument"

cd %mainDir%\%1ODataService\
cd Document
call npm install -g redoc-cli
call redoc-cli bundle -o index.html OpenApiDocument.json
cd %mainDir%\%1ODataService\
dotnet build



