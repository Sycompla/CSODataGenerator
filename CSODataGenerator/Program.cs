
using Ac4yClassModule.Class;
using Ac4yUtilityContainer;
using CSARMetaPlan.Class;
using CSRunWithXmlRequest;
//using CSClassLibForJavaOData;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace CSODataGenerator
{
    class Program
    {

        #region members

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Assembly _library { get; set; }
        

        CSODataGeneratorParameter Parameter { get; set; }
        public static Ac4yUtility ac4yUtility = new Ac4yUtility();

        public IConfiguration Config { get; set; }

        #endregion members

        public Program(IConfiguration config)
        {

            Config = config;

        } // Program


        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                Console.WriteLine(arg);
            }

            string APPSETTINGS_REQUESTPATH = args[1];
            string APPSETTINGS_XMLPATH = args[2];

            string requestXml = File.ReadAllText(APPSETTINGS_REQUESTPATH);
            RunWithXmlRequest RunWithXmlRequest = (RunWithXmlRequest) new Ac4yUtility().Xml2Object(requestXml, typeof(RunWithXmlRequest));

            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                IConfiguration config = null;

                config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();


                Console.WriteLine("Path >>" + APPSETTINGS_XMLPATH);
                Ac4yModule ac4yClasses = (Ac4yModule) ac4yUtility.Xml2ObjectFromFile(APPSETTINGS_XMLPATH, typeof(Ac4yModule));

                RunWithXmlRequest.Ac4yModule = ac4yClasses;
                RunWithXmlRequest.Argument = args[0];

                new RunWithXml(RunWithXmlRequest) { }.Run();
                /*
                new RunWithDll(args[0])
                {
                    RootDirectory = config[APPSETTINGS_ROOTDIRECTORY]
                    ,
                    ODataURL = config[APPSETTINGS_ODATAURL]
                    ,
                    ConnectionString = config[APPSETTINGS_CONNECTIONSTRING]
                    ,
                    LinuxServiceFileDescription = config[APPSETTINGS_LINUXSERVICEFILEDESCRIPTION]
                    ,
                    IPAddress = config[APPSETTINGS_IPADDRESS]
                    ,
                    LibraryPath = config[APPSETTINGS_LIBRARYPATH]
                    ,
                    LinuxPath = config[APPSETTINGS_LINUXPATH]
                    ,
                    Namespace = config[APPSETTINGS_NAMESPACE]
                    ,
                    ParameterFileName = config[APPSETTINGS_PARAMETERFILENAME]
                    ,
                    ParameterPath = config[APPSETTINGS_PARAMETERPATH]
                    ,
                    PortNumber = config[APPSETTINGS_PORTNUMBER]
                }
                    .Run();
                */
            }

            catch (Exception exception)
            {

                log.Error(exception.Message);
                log.Error(exception.StackTrace);
                Console.WriteLine(exception.Message + "\n" + exception.StackTrace);

                Console.ReadLine();

            }

        } // Main

    } // Program

} // EFGeneralas