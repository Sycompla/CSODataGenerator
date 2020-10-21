
using CSARMetaPlan.Class;
using CSClassLibForJavaOData;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CSODataGenerator
{
    class Program
    {

        #region members

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        private const string APPSETTINGS_CAPOUTPUTPATH = "CAPOUTPUTPATH";
        private const string APPSETTINGS_SERVICEOUTPUTPATH = "SERVICEOUTPUTPATH";
        private const string APPSETTINGS_RESTSERVICEODATAOUTPUTPATH = "RESTSERVICEODATAOUTPUTPATH";
        private const string APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH = "RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH";

        private const string APPSETTINGS_TEMPLATEPATH = "TEMPLATEPATH";
        private const string APPSETTINGS_ROOTDIRECTORY = "ROOTDIRECTORY";
        
        private const string APPSETTINGS_PORTNUMBER = "PORTNUMBER";
        private const string APPSETTINGS_IPADDRESS = "IPADDRESS";
        private const string APPSETTINGS_NAMESPACE = "NAMESPACE";
        private const string APPSETTINGS_CONNECTIONSTRING = "CONNECTIONSTRING";

        private const string APPSETTINGS_CAPREFERENCES = "CAPREFERENCES";
        private const string APPSETTINGS_OBJECTSERVICEREFERENCES = "OBJECTSERVICEREFERENCES";
        private const string APPSETTINGS_ODATACONTROLLERREFERENCES = "ODATACONTROLLERREFERENCES";

        public IConfiguration Config { get; set; }

        #endregion members

        public Program(IConfiguration config)
        {

            Config = config;

        } // Program

        public void Run(string argument)
        {
            if (argument.Equals("Cap")) {
                new CapGenerator()
                {
                    OutputPath = Config[APPSETTINGS_ROOTDIRECTORY] + Config[APPSETTINGS_CAPOUTPUTPATH]
                    ,
                    Namespace = Config[APPSETTINGS_NAMESPACE]
                    ,
                    References = Config[APPSETTINGS_CAPREFERENCES]
                }
                    .Generate(typeof(Vendor));
            }

            if (argument.Equals("Context"))
            {
                new ContextGenerator()
                {
                    OutputPath = Config[APPSETTINGS_ROOTDIRECTORY] + Config[APPSETTINGS_CAPOUTPUTPATH]
                    ,
                    Namespace = Config[APPSETTINGS_NAMESPACE]
                    ,
                    References = Config[APPSETTINGS_CAPREFERENCES]
                    ,
                    ConnectionString = Config[APPSETTINGS_CONNECTIONSTRING]
                }
                    .Generate(typeof(Vendor));
            }


            if (argument.Equals("ObjectService"))
            {
                new ObjectServiceGenerator()
                {
                    OutputPath = Config[APPSETTINGS_ROOTDIRECTORY] + Config[APPSETTINGS_SERVICEOUTPUTPATH]
                    ,
                    References = Config[APPSETTINGS_OBJECTSERVICEREFERENCES]
                    ,
                    Namespace = Config[APPSETTINGS_NAMESPACE]
                }
                    .Generate(typeof(Vendor));
            }

            if (argument.Equals("ODataController"))
            {
                new RESTServiceODataControllerGenerator()
                { 
                    OutputPath = Config[APPSETTINGS_ROOTDIRECTORY] + Config[APPSETTINGS_RESTSERVICEODATAOUTPUTPATH]
                    ,
                    Namespace = Config[APPSETTINGS_NAMESPACE]
                    ,
                    References = Config[APPSETTINGS_ODATACONTROLLERREFERENCES]
                }
                    .Generate(typeof(Vendor));

            }

            if (argument.Equals("Kestrel"))
            {
                new RESTServiceProgramClassWithKestrelGenerator()
                {
                    OutputPath = Config[APPSETTINGS_ROOTDIRECTORY] + Config[APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH]
                    ,
                    IPAddress = Config[APPSETTINGS_IPADDRESS]
                    ,
                    NameSpace = Config[APPSETTINGS_NAMESPACE]
                    ,
                    PortNumber = Config[APPSETTINGS_PORTNUMBER]

                }
                    .Generate(typeof(Vendor));

            }
            if (argument.Equals("Startup"))
            {
                new RESTServiceStartupClassWithODataGenerator()
                {
                    OutputPath = Config[APPSETTINGS_ROOTDIRECTORY] + Config[APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH]
                    ,
                    NameSpace = Config[APPSETTINGS_NAMESPACE]
                    ,
                    References = Config[APPSETTINGS_CAPREFERENCES]

                }
                    .Generate(typeof(Vendor));

            }


        } // run

        static void Main(string[] args)
        {

            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                IConfiguration config = null;

                config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();

                new Program(config).Run(args[0]);

            }
            catch (Exception exception)
            {

                log.Error(exception.Message);
                log.Error(exception.StackTrace);

                Console.ReadLine();

            }

        } // Main

    } // Program

} // EFGeneralas