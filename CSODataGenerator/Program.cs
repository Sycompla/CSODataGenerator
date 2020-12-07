﻿
using Ac4yUtilityContainer;
using CSAc4yModule;
using CSARMetaPlan.Class;
using CSClassLibForJavaOData;
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

        private const string APPSETTINGS_LIBRARYPATH = "LIBRARYPATH";
        private const string APPSETTINGS_PLANOBJECTNAMESPACE = "PLANOBJECTNAMESPACE";
        private const string APPSETTINGS_CLASSNAME = "CLASSNAME";

        private const string APPSETTINGS_PARAMETERPATH = "PARAMETERPATH";
        private const string APPSETTINGS_PARAMETERFILENAME = "PARAMETERFILENAME";
        private const string APPSETTINGS_XMLPATH = "XMLPATH";

        private const string APPSETTINGS_LINUXPATH = "LINUXPATH";
        private const string APPSETTINGS_LINUXSERVICEFILEDESCRIPTION = "LINUXSERVICEFILEDESCRIPTION";

        private const string APPSETTINGS_ODATAURL = "ODATAURL";

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

            try
            {
                var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

                IConfiguration config = null;

                config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", true, true)
                            .Build();

                Ac4yModule ac4yClasses = null;
                string path = config[APPSETTINGS_XMLPATH];
                string xml = new StreamReader(path, Encoding.UTF8).ReadToEnd();
                ac4yClasses = (Ac4yModule) ac4yUtility.Xml2Object(xml, typeof(Ac4yModule));

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