using Ac4yClassModule.Class;
using CSRunWithXmlRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    public class RunWithXml
    {
        private RunWthXmlRequest RunWthXmlRequest { get; set; }

        private string Argument { get; set; }
        private Ac4yModule Ac4yModule { get; set; }

        public RunWithXml(string args, Ac4yModule ac4yModule, RunWthXmlRequest request)
        {
            Argument = args;
            Ac4yModule = ac4yModule;
            RunWthXmlRequest = request;
        }

        public RunWithXml() { }

        public void Run()
        {
            if(Argument.Equals("bat"))
            {
                new BatFileGenerator()
                {

                }.Generate();
            }

            if (Argument.Equals("UpsertController"))
            {
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertControllerGeneratorAc4yClass()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/Controllers/"
                        ,
                        Namespace = RunWthXmlRequest.Namespace
                        ,
                        OdataUrl = RunWthXmlRequest.ODataURL
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("UpsertService"))
            {
                Directory.CreateDirectory(RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/Services/");
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertServiceServiceGeneratorAc4yClass()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/Services/"
                        ,
                        Namespace = RunWthXmlRequest.Namespace
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("UpsertResponse"))
            {
                Directory.CreateDirectory(RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/Responses/");
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertServiceResponseGeneratorAc4yClass()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/Responses/"
                        ,
                        Namespace = RunWthXmlRequest.Namespace
                    }
                        .Generate(planObject);
                }
            }

            if(Argument.Equals("UpsertServiceStartup"))
            {
                new UpsertServiceStartupGeneratorAc4yClass()
                {
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/"
                        ,
                    Namespace = RunWthXmlRequest.Namespace
                }
                        .Generate();
            }

            if(Argument.Equals("Ac4yRestServiceClient"))
            {
                new Ac4yRestServiceClientGeneratorAc4yClass()
                {
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "UpsertService/"
                        ,
                    Namespace = RunWthXmlRequest.Namespace
                }
                        .Generate();
            }

            if (Argument.Equals("PlanObject"))
            {
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new PlanObjectGenerator()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.PlanObjectFolderName
                    }
                        .Generate(planObject);
                }
            }

                    if (Argument.Equals("Cap"))
            {
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {

                    new CapGeneratorAc4yClass()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "Cap/"
                        ,
                        Namespace = RunWthXmlRequest.Namespace
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("Context"))
            {
                new ContextGeneratorAc4yClass()
                {
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "Cap/"
                    ,
                    Namespace = RunWthXmlRequest.Namespace
                    ,
                    ConnectionString = RunWthXmlRequest.ConnectionString
                    ,
                    Parameter = Ac4yModule
                }
                    .Generate(Ac4yModule.ClassList[0]);
            }


            if (Argument.Equals("ObjectService"))
            {
                foreach (Ac4yClass ac4yClass in Ac4yModule.ClassList)
                {
                    new ObjectServiceGeneratorAc4yClass()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ObjectService/"
                    ,
                        Namespace = RunWthXmlRequest.Namespace
                    }
                    .Generate(ac4yClass);
                }
            }

            if (Argument.Equals("ODataController"))
            {
                foreach (Ac4yClass ac4yClass in Ac4yModule.ClassList)
                {
                    new RESTServiceODataControllerGeneratorAc4yClass()
                    {
                        OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/Controllers/"
                    ,
                        Namespace = RunWthXmlRequest.Namespace
                    }
                    .Generate(ac4yClass);
                }

            }

            if (Argument.Equals("Kestrel"))
            {
                new RESTServiceProgramClassWithKestrelGenerator()
                {
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/"
                    ,
                    IPAddress = RunWthXmlRequest.IPAddress
                    ,
                    NameSpace = RunWthXmlRequest.Namespace
                    ,
                    PortNumber = RunWthXmlRequest.PortNumber

                }
                    .Generate();

            }
            if (Argument.Equals("Startup"))
            {
                new RESTServiceStartupClassWithODataGeneratorAc4yClass()
                {
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/"
                    ,
                    NameSpace = RunWthXmlRequest.Namespace
                    ,
                    Parameter = Ac4yModule

                }
                    .Generate();

            }

            if (Argument.Equals("OpenApiDocument"))
            {
                Directory.CreateDirectory(RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/Document/");

                new OpenApiGeneratorAc4yClass()
                {
                    ODataUrl = RunWthXmlRequest.ODataURL
                    ,
                    Version = "1.20201111.1"
                    ,
                    Parameter = Ac4yModule
                    ,
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/Document/"
                }
                    .Generate();
            }
            /*
            if (Argument.Equals("LinuxServiceFile"))
            {
                new LinuxServiceFileGenerator()
                {
                    DLLName = RunWthXmlRequest.Namespace
                    ,
                    Description = LinuxServiceFileDescription
                    ,
                    LinuxPath = LinuxPath
                    ,
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/"
                }
                    .Generate();
            }*/

            if (Argument.Equals("Csproj"))
            {
                new CsprojGenerator()
                {
                    OutputPath = RunWthXmlRequest.RootDirectory + RunWthXmlRequest.Namespace + "ODataService/"
                    ,
                    Name = RunWthXmlRequest.Namespace
                }
                    .Generate();
            }

            
        
        } // run
    }
}
