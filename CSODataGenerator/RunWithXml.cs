using Ac4yClassModule.Class;
using Ac4yUtilityContainer;
using CSRunWithXmlRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    public class RunWithXml
    {
        private RunWithXmlRequest RunWithXmlRequest { get; set; }

        private string Argument { get; set; }
        private Ac4yModule Ac4yModule { get; set; }

        public RunWithXml(RunWithXmlRequest request)
        {
            RunWithXmlRequest = request;
            Argument = RunWithXmlRequest.Argument;
            Ac4yModule = RunWithXmlRequest.Ac4yModule;
        }

        public RunWithXml() { }

        public string GeneratePlanObject()
        {

            Ac4yUtility utility = new Ac4yUtility();
            Ac4yClass ac4yClass = (Ac4yClass)utility.Xml2Object(RunWithXmlRequest.ac4yClassXml, typeof(Ac4yClass));

            string result = new PlanObjectGenerator()
            {
                OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.PlanObjectFolderName
            }
                .Generate(ac4yClass);

            return EncodeTo64(result);
        }

        public string EncodeTo64(string toEncode)

        {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        public void Run()
        {
            if(Argument.Equals("Appsettings"))
            {
                new AppsettingsGenerator()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/"
                    ,
                    IpAddress = RunWithXmlRequest.IPAddress
                    ,
                    PortNumber = RunWithXmlRequest.PortNumber
                    ,
                    DBIP = RunWithXmlRequest.DBIP
                    ,
                    DBName = RunWithXmlRequest.DBName
                    ,
                    DBPassword = RunWithXmlRequest.DBPassword
                    ,
                    DBUserName = RunWithXmlRequest.DBUsername
                    ,
                    System = RunWithXmlRequest.System
                }.Generate();
            }

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
                        OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/Controllers/"
                        ,
                        Namespace = RunWithXmlRequest.Namespace
                        ,
                        OdataUrl = RunWithXmlRequest.ODataURL
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("UpsertService"))
            {
                Directory.CreateDirectory(RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/Services/");
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertServiceServiceGeneratorAc4yClass()
                    {
                        OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/Services/"
                        ,
                        Namespace = RunWithXmlRequest.Namespace
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("UpsertResponse"))
            {
                Directory.CreateDirectory(RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/Responses/");
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {
                    new UpsertServiceResponseGeneratorAc4yClass()
                    {
                        OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/Responses/"
                        ,
                        Namespace = RunWithXmlRequest.Namespace
                    }
                        .Generate(planObject);
                }
            }

            if(Argument.Equals("UpsertServiceStartup"))
            {
                new UpsertServiceStartupGeneratorAc4yClass()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/"
                        ,
                    Namespace = RunWithXmlRequest.Namespace
                }
                        .Generate();
            }

            if(Argument.Equals("Ac4yRestServiceClient"))
            {
                new Ac4yRestServiceClientGeneratorAc4yClass()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "UpsertService/"
                        ,
                    Namespace = RunWithXmlRequest.Namespace
                }
                        .Generate();
            }

            if (Argument.Equals("PlanObject"))
            {
                Ac4yUtility utility = new Ac4yUtility();
                Ac4yClass ac4yClass = (Ac4yClass)utility.Xml2Object(RunWithXmlRequest.ac4yClassXml, typeof(Ac4yClass));

                new PlanObjectGenerator()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.PlanObjectFolderName
                }
                    .Generate(ac4yClass);
            /*
            foreach (Ac4yClass planObject in Ac4yModule.ClassList)
            {
                new PlanObjectGenerator()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.PlanObjectFolderName
                }
                    .Generate(planObject);
            }*/
        }

            if (Argument.Equals("Cap"))
            {
                foreach (Ac4yClass planObject in Ac4yModule.ClassList)
                {

                    new CapGeneratorAc4yClass()
                    {
                        OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "Cap/"
                        ,
                        Namespace = RunWithXmlRequest.Namespace
                    }
                        .Generate(planObject);
                }
            }

            if (Argument.Equals("Context"))
            {
                new ContextGeneratorAc4yClass()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "Cap/"
                    ,
                    Namespace = RunWithXmlRequest.Namespace
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
                        OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ObjectService/"
                    ,
                        Namespace = RunWithXmlRequest.Namespace
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
                        OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/Controllers/"
                    ,
                        Namespace = RunWithXmlRequest.Namespace
                    }
                    .Generate(ac4yClass);
                }

            }

            if (Argument.Equals("Kestrel"))
            {
                new RESTServiceProgramClassWithKestrelGenerator()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/"
                    ,
                    IPAddress = RunWithXmlRequest.IPAddress
                    ,
                    NameSpace = RunWithXmlRequest.Namespace
                    ,
                    PortNumber = RunWithXmlRequest.PortNumber

                }
                    .Generate();

            }
            if (Argument.Equals("Startup"))
            {
                new RESTServiceStartupClassWithODataGeneratorAc4yClass()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/"
                    ,
                    NameSpace = RunWithXmlRequest.Namespace
                    ,
                    Parameter = Ac4yModule

                }
                    .Generate();

            }

            if (Argument.Equals("OpenApiDocument"))
            {
                Directory.CreateDirectory(RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/Document/");

                new OpenApiGeneratorAc4yClass()
                {
                    ODataUrl = RunWithXmlRequest.ODataURL
                    ,
                    Version = "1.20201111.1"
                    ,
                    Parameter = Ac4yModule
                    ,
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/Document/"
                }
                    .Generate();
            }
            /*
            if (Argument.Equals("LinuxServiceFile"))
            {
                new LinuxServiceFileGenerator()
                {
                    DLLName = RunWithXmlRequest.Namespace
                    ,
                    Description = LinuxServiceFileDescription
                    ,
                    LinuxPath = LinuxPath
                    ,
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/"
                }
                    .Generate();
            }*/

            if (Argument.Equals("Csproj"))
            {
                new CsprojGenerator()
                {
                    OutputPath = RunWithXmlRequest.RootDirectory + RunWithXmlRequest.Namespace + "ODataService/"
                    ,
                    Name = RunWithXmlRequest.Namespace
                }
                    .Generate();
            }

            
        
        } // run
    }
}
