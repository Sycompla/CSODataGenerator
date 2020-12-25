using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class RunWithXml
    {
        public string LibraryPath { get; set; }
        public string ParameterPath { get; set; }
        public string ParameterFileName { get; set; }
        public string RootDirectory { get; set; }
        public string Namespace { get; set; }
        public string PLanObjectNamespace { get; set; }
        public string ConnectionString { get; set; }
        public string PortNumber { get; set; }
        public string IPAddress { get; set; }
        public string ODataURL { get; set; }
        public string LinuxPath { get; set; }
        public string LinuxServiceFileDescription { get; set; }
        public string PlanObjectFolderName { get; set; }

        private string Argument { get; set; }
        private Ac4yModule Ac4yModule { get; set; }

        CSODataGeneratorParameter Parameter { get; set; }

        public RunWithXml(string args, Ac4yModule ac4yModule)
        {
            Argument = args;
            Ac4yModule = ac4yModule;
        }

        public RunWithXml() { }

        public void Run()
        {
            if (Argument.Equals("UpsertController"))
            {
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertControllerGeneratorAc4yClass()
                    {
                        OutputPath = RootDirectory + Namespace + "UpsertService\\Controllers\\"
                        ,
                        Namespace = Namespace
                        ,
                        OdataUrl = ODataURL
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("UpsertService"))
            {
                Directory.CreateDirectory(RootDirectory + Namespace + "UpsertService\\Services\\");
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertServiceServiceGeneratorAc4yClass()
                    {
                        OutputPath = RootDirectory + Namespace + "UpsertService\\Services\\"
                        ,
                        Namespace = Namespace
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("UpsertResponse"))
            {
                Directory.CreateDirectory(RootDirectory + Namespace + "UpsertService\\Responses\\");
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertServiceResponseGeneratorAc4yClass()
                    {
                        OutputPath = RootDirectory + Namespace + "UpsertService\\Responses\\"
                        ,
                        Namespace = Namespace
                    }
                        .Generate(planObject);
                }
            }

            if(Argument.Equals("UpsertServiceStartup"))
            {
                new UpsertServiceStartupGeneratorAc4yClass()
                {
                    OutputPath = RootDirectory + Namespace + "UpsertService\\"
                        ,
                    Namespace = Namespace
                }
                        .Generate();
            }

            if(Argument.Equals("Ac4yRestServiceClient"))
            {
                new Ac4yRestServiceClientGeneratorAc4yClass()
                {
                    OutputPath = RootDirectory + Namespace + "UpsertService\\"
                        ,
                    Namespace = Namespace
                }
                        .Generate();
            }

            if (Argument.Equals("PlanObject"))
            {
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new PlanObjectGenerator()
                    {
                        OutputPath = RootDirectory + PlanObjectFolderName
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
                        OutputPath = RootDirectory + Namespace + "Cap\\"
                        ,
                        Namespace = Namespace
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("Context"))
            {
                new ContextGeneratorAc4yClass()
                {
                    OutputPath = RootDirectory + Namespace + "Cap\\"
                    ,
                    Namespace = Namespace
                    ,
                    ConnectionString = ConnectionString
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
                        OutputPath = RootDirectory + Namespace + "ObjectService\\"
                    ,
                        Namespace = Namespace
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
                        OutputPath = RootDirectory + Namespace + "ODataService\\Controllers\\"
                    ,
                        Namespace = Namespace
                    }
                    .Generate(ac4yClass);
                }

            }

            if (Argument.Equals("Kestrel"))
            {
                new RESTServiceProgramClassWithKestrelGenerator()
                {
                    OutputPath = RootDirectory + Namespace + "ODataService\\"
                    ,
                    IPAddress = IPAddress
                    ,
                    NameSpace = Namespace
                    ,
                    PortNumber = PortNumber

                }
                    .Generate();

            }
            if (Argument.Equals("Startup"))
            {
                new RESTServiceStartupClassWithODataGeneratorAc4yClass()
                {
                    OutputPath = RootDirectory + Namespace + "ODataService\\"
                    ,
                    NameSpace = Namespace
                    ,
                    Parameter = Ac4yModule

                }
                    .Generate();

            }

            if (Argument.Equals("OpenApiDocument"))
            {
                Directory.CreateDirectory(RootDirectory + Namespace + "ODataService\\Document\\");

                new OpenApiGeneratorAc4yClass()
                {
                    ODataUrl = ODataURL
                    ,
                    Version = "1.20201111.1"
                    ,
                    Parameter = Ac4yModule
                    ,
                    OutputPath = RootDirectory + Namespace + "ODataService\\Document\\"
                }
                    .Generate();
            }
            /*
            if (Argument.Equals("LinuxServiceFile"))
            {
                new LinuxServiceFileGenerator()
                {
                    DLLName = Namespace
                    ,
                    Description = LinuxServiceFileDescription
                    ,
                    LinuxPath = LinuxPath
                    ,
                    OutputPath = RootDirectory + Namespace + "ODataService\\"
                }
                    .Generate();
            }*/

            if (Argument.Equals("Csproj"))
            {
                new CsprojGenerator()
                {
                    OutputPath = RootDirectory + Namespace + "ODataService\\"
                    ,
                    Name = Namespace
                }
                    .Generate();
            }

            
        
        } // run
    }
}
