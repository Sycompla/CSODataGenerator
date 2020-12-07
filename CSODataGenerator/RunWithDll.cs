
using Ac4yUtilityContainer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CSODataGenerator
{
    public class RunWithDll
    {
        public string LibraryPath { get; set; }
        public string ParameterPath { get; set; }
        public string ParameterFileName { get; set; }
        public string RootDirectory { get; set; }
        public string Namespace { get; set; }
        public string ConnectionString { get; set; }
        public string PortNumber { get; set; }
        public string IPAddress { get; set; }
        public string ODataURL { get; set; }
        public string LinuxPath { get; set; }
        public string LinuxServiceFileDescription { get; set; }

        private string Argument { get; set; }
        private Assembly _library { get; set; }

        CSODataGeneratorParameter Parameter { get; set; }
        
        public RunWithDll(string args)
        {
            Argument = args;
        }

        public RunWithDll() { }

        public void Run()
        {

            _library = Assembly.LoadFile(
                        LibraryPath
                    );

            Parameter =
                (CSODataGeneratorParameter)
                new Ac4yUtility().Xml2ObjectFromFile(
                        ParameterPath
                        + ParameterFileName
                        , typeof(CSODataGeneratorParameter)
                    );

            foreach (PlanObjectReference planObject in Parameter.PlanObjectReferenceList)
            {
                planObject.classType = _library.GetType(
                                                    planObject.namespaceName
                                                    + planObject.className
                                                    );

            }

            if (Argument.Equals("Cap"))
            {
                foreach (PlanObjectReference planObject in Parameter.PlanObjectReferenceList)
                {
                    new CapGenerator()
                    {
                        OutputPath = RootDirectory + Namespace + "Cap\\"
                        ,
                        Namespace = Namespace
                    }
                        .Generate(planObject.classType);
                }
            }

            if (Argument.Equals("Context"))
            {
                new ContextGenerator()
                {
                    OutputPath = RootDirectory + Namespace + "Cap\\"
                    ,
                    Namespace = Namespace
                    ,
                    ConnectionString = ConnectionString
                    ,
                    Parameter = Parameter
                }
                    .Generate(Parameter.PlanObjectReferenceList[0].classType);
            }


            if (Argument.Equals("ObjectService"))
            {
                foreach (PlanObjectReference planObject in Parameter.PlanObjectReferenceList)
                {
                    new ObjectServiceGenerator()
                    {
                        OutputPath = RootDirectory + Namespace + "ObjectService\\"
                    ,
                        Namespace = Namespace
                    }
                    .Generate(planObject.classType);
                }
            }

            if (Argument.Equals("ODataController"))
            {
                foreach (PlanObjectReference planObject in Parameter.PlanObjectReferenceList)
                {
                    new RESTServiceODataControllerGenerator()
                    {
                        OutputPath = RootDirectory + Namespace + "ODataService\\Controllers\\"
                    ,
                        Namespace = Namespace
                    }
                    .Generate(planObject.classType);
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
                    .Generate(Parameter.PlanObjectReferenceList[0].classType);

            }
            if (Argument.Equals("Startup"))
            {
                new RESTServiceStartupClassWithODataGenerator()
                {
                    OutputPath = RootDirectory + Namespace + "ODataService\\"
                    ,
                    NameSpace = Namespace
                    ,
                    Parameter = Parameter

                }
                    .Generate();

            }
            if (Argument.Equals("OpenApiDocument"))
            {
                Directory.CreateDirectory(RootDirectory + Namespace + "ODataService\\Document\\");

                new OpenApiGenerator()
                {
                    ODataUrl = ODataURL
                    ,
                    Version = "1.20201111.1"
                    ,
                    Parameter = Parameter
                    ,
                    OutputPath = RootDirectory + Namespace + "ODataService\\Document\\"
                }
                    .Generate();
            }

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
            }

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
