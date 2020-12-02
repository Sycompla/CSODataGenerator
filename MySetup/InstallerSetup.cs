using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MySetup
{
    [RunInstaller(true)]
    public partial class InstallerSetup : System.Configuration.Install.Installer
    {
        public InstallerSetup()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);

            try
            {
                AddConfigurationFileDetails();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: " + e.Message);
                base.Rollback(savedState);
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);

            try
            {
                GetFile();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: " + e.Message);
                base.Rollback(savedState);
            }

        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }

        private void GetFile()
        {
            string path = Context.Parameters["assemblyPath"].Replace("MySetup.dll", "");
            string jsonIn = File.ReadAllText(path + "ODataServiceGenerator.bat");

            MessageBox.Show(jsonIn);
            File.ReadAllText("srvfg");
        }

        private void AddConfigurationFileDetails()
        {
            string json = "{\n'ROOTDIRECTORY':'#rootDirectory#','PORTNUMBER':'8888','IPADDRESS':'89.40.120.71','NAMESPACE':'#namespace#','CONNECTIONSTRING':'Server=89.40.120.71;Database=EntityWithCombo;Trusted_Connection=False;uid=SA;pwd=Sycompla9999*;','ODATACONTROLLERREFERENCES':'using VendorPlanObject; using CSVendorCap; using CSVendorObjectService; using static CSVendorObjectService.VendorObjectService;','LIBRARYPATH':'#libraryPath#','PLANOBJECTNAMESPACE':'#planObjectNamespace#','LINUXPATH':'/home/d7p4n4/webapp/','LINUXSERVICEFILEDESCRIPTION':'OData Service for 2 comboboxes','CLASSNAME':'Vendor','PARAMETERPATH':'Config\\','PARAMETERFILENAME':'Parameter.xml','ODATAURL':'https://localhost:5001/odata'\n}";
            string keys = "";
            string values = "";
            string jsonIn = File.ReadAllText("ODataServiceGenerator.bat");
            foreach (string myString in Context.Parameters.Keys)
            {
                keys = keys + myString + "\n";
            }

            foreach(string myString in Context.Parameters.Values)
            {
                values = values + myString + "\n";
            }

            MessageBox.Show(jsonIn);
            try
            {
                string ROOTDIRECTORY = Context.Parameters["ROOTDIRECTORY"];
                string LIBRARYPATH = Context.Parameters["LIBRARYPATH"];
                string PLANOBJECTNAMESPACE = Context.Parameters["PLANOBJECTNAMESPACE"];
                string NAMESPACE = Context.Parameters["NAMESPACE"];

                // Get the path to the executable file that is being installed on the target computer  
                string RootDirectoryMask = "#rootDirectory#";
                string LibraryPathMask = "#libraryPath#";
                string PlanObjectNamespaceMask = "#planObjectNamespace#";
                string namespaceMask = "#namespace#";
                string path = Context.Parameters["assemblyPath"].Replace("MySetup.dll", "");

                //Directory.CreateDirectory(path.Replace("Generator\\", "") + "Appsettings\\");
                MessageBox.Show(path);

                string appsettingsTextEdited = json
                                                .Replace(RootDirectoryMask, ROOTDIRECTORY)
                                                .Replace(LibraryPathMask, LIBRARYPATH)
                                                .Replace(PlanObjectNamespaceMask, PLANOBJECTNAMESPACE)
                                                .Replace(namespaceMask, NAMESPACE)
                                                .Replace(",", ",\n")
                                                .Replace("'", "\"");

                File.WriteAllText(path + "appsettings.json", appsettingsTextEdited);

            }
            catch
            {
                throw;
            }
        }
    }
}