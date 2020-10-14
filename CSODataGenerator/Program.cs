
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

        private const string APPSETTINGS_OUTPUTPATH = "OUTPUTPATH";

        private const string APPSETTINGS_CAPOUTPUTPATH = "CAPOUTPUTPATH";
        private const string APPSETTINGS_SERVICEOUTPUTPATH = "SERVICEOUTPUTPATH";
        private const string APPSETTINGS_RESTSERVICEOUTPUTPATH = "RESTSERVICEOUTPUTPATH";
        private const string APPSETTINGS_RESTSERVICEODATAOUTPUTPATH = "RESTSERVICEODATAOUTPUTPATH";
        private const string APPSETTINGS_SERVICEBASEOUTPUTPATH = "SERVICEBASEOUTPUTPATH";
        private const string APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH = "RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH";

        private const string APPSETTINGS_TEMPLATEPATH = "TEMPLATEPATH";

        private const string APPSETTINGS_TEMPLATESUBPATH = "TEMPLATESUBPATH";

        private const string APPSETTINGS_CAPTEMPLATESUBPATH = "CAPTEMPLATESUBPATH";
        private const string APPSETTINGS_SERVICETEMPLATESUBPATH = "SERVICETEMPLATESUBPATH";
        private const string APPSETTINGS_RESTSERVICETEMPLATESUBPATH = "RESTSERVICETEMPLATESUBPATH";
        private const string APPSETTINGS_RESTSERVICEODATATEMPLATESUBPATH = "RESTSERVICEODATATEMPLATESUBPATH";
        private const string APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELTEMPLATESUBPATH = "RESTSERVICEPROGRAMWITHKESTRELTEMPLATESUBPATH";
        private const string APPSETTINGS_RESTSERVICESTARTUPWITHODATATEMPLATESUBPATH = "RESTSERVICESTARTUPWITHODATATEMPLATESUBPATH";

        private const string APPSETTINGS_PROJECTNAME = "PROJECTNAME";
        private const string APPSETTINGS_PORTNUMBER = "PORTNUMBER";
        private const string APPSETTINGS_IPADDRESS = "IPADDRESS";
        private const string APPSETTINGS_NAMESPACE = "NAMESPACE";

        // JAVA //

        private const string APPSETTINGS_JAVAODATASERVICESUBPATH = "JAVAODATASERVICESUBPATH";
        private const string APPSETTINGS_CLASSNAME = "CLASSNAME";
        private const string APPSETTINGS_PACKAGENAME = "PACKAGENAME";
        private const string APPSETTINGS_PERSISTENCENAME = "PERSISTENCENAME";
        private const string APPSETTINGS_JAVAODATASERVICEOUTPUTPATH = "JAVAODATASERVICEOUTPUTPATH";

        private const string APPSETTINGS_IP = "IP";
        private const string APPSETTINGS_PORT = "PORT";
        private const string APPSETTINGS_USERNAME = "USERNAME";
        private const string APPSETTINGS_PASSWORD = "PASSWORD";
        private const string APPSETTINGS_DATABASENAME = "DATABASENAME";
        private const string APPSETTINGS_JAVAPERSISTENCESUBPATH = "JAVAPERSISTENCESUBPATH";
        private const string APPSETTINGS_JAVAPERSISTENCEOUTPUTPATH = "JAVAPERSISTENCEOUTPUTPATH";


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
                    TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                    ,
                    TemplateSubPath = Config[APPSETTINGS_CAPTEMPLATESUBPATH]
                    ,
                    OutputPath = Config[APPSETTINGS_CAPOUTPUTPATH]
                    ,
                    ProjectName = Config[APPSETTINGS_PROJECTNAME]
                }
                    .Generate(typeof(Vendor));
            }

            if (argument.Equals("ObjectService"))
            {
                new ObjectServiceGenerator()
                {
                    TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                    ,
                    TemplateSubPath = Config[APPSETTINGS_SERVICETEMPLATESUBPATH]
                    ,
                    OutputPath = Config[APPSETTINGS_SERVICEOUTPUTPATH]
                    ,
                    ProjectName = Config[APPSETTINGS_PROJECTNAME]

                }
                    .Generate(typeof(Vendor));
            }

            if (argument.Equals("ODataController"))
            {
                new RESTServiceODataControllerGenerator()
                { 
                    TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                    ,
                    TemplateSubPath = Config[APPSETTINGS_RESTSERVICEODATATEMPLATESUBPATH]
                    ,
                    OutputPath = Config[APPSETTINGS_RESTSERVICEODATAOUTPUTPATH]
                    ,
                    ProjectName = Config[APPSETTINGS_PROJECTNAME]

                }
                    .Generate(typeof(Vendor));

            }

            if (argument.Equals("Kestrel"))
            {
                new RESTServiceProgramClassWithKestrelGenerator()
                {
                    TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                    ,
                    TemplateSubPath = Config[APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELTEMPLATESUBPATH]
                    ,
                    OutputPath = Config[APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH]
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
                    TemplatePath = Config[APPSETTINGS_TEMPLATEPATH]
                    ,
                    TemplateSubPath = Config[APPSETTINGS_RESTSERVICESTARTUPWITHODATATEMPLATESUBPATH]
                    ,
                    OutputPath = Config[APPSETTINGS_RESTSERVICEPROGRAMWITHKESTRELOUTPUTPATH]
                    ,
                    NameSpace = Config[APPSETTINGS_NAMESPACE]

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