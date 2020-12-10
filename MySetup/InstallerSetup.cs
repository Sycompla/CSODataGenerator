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
                GetFile();
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
                MessageBox.Show("Rollback");
                //GetFile();
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
            //MessageBox.Show(Context.Parameters["RUN"]);
            try
            {
                string ROOTDIRECTORY = Context.Parameters["ROOTDIRECTORY"];
                string LIBRARYPATH = Context.Parameters["LIBRARYPATH"];
                string PLANOBJECTNAMESPACE = Context.Parameters["PLANOBJECTNAMESPACE"];
                string NAMESPACE = Context.Parameters["NAMESPACE"];
                string CLASSNAMELIST = Context.Parameters["CLASSNAMELIST"];
                string DATABASENAME = Context.Parameters["DATABASENAME"];
                string RUN = Context.Parameters["RUN"];
                string PLANOBJECTFOLDERNAME = Context.Parameters["PLANOBJECTFOLDERNAME"];
                List<string> classNameList = CLASSNAMELIST.Split(',').ToList();
                string path = Context.Parameters["assemblyPath"].Replace("MySetup.dll", "");

                new AppsettingsGenerator()
                {
                    OutputPath = path
                    ,
                    RootDirectory = ROOTDIRECTORY
                    ,
                    InputPath = path
                    ,
                    Namespace = NAMESPACE
                    ,
                    LibraryPath = LIBRARYPATH
                    ,
                    PlanObjectNamespace = PLANOBJECTNAMESPACE
                    ,
                    DatabaseName = DATABASENAME
                    ,
                    PlanObjectFolderName = PLANOBJECTFOLDERNAME
                }
                    .Generate();

                new BatFileGenerator()
                {
                    OutputPath = path
                    ,
                    InputPath = path
                    ,
                    GeneratorDirectory = path
                    ,
                    RootDiectory = ROOTDIRECTORY
                    ,
                    Namespace = NAMESPACE
                    ,
                    PlanObjectNamespace = PLANOBJECTFOLDERNAME
                }
                    .Generate();

                new ParameterGenerator()
                {
                    OutputPath = path + "Config\\"
                    ,
                    InputPath = path
                    ,
                    ClassNames = classNameList
                    ,
                    Namespace = PLANOBJECTNAMESPACE
                }
                    .Generate();

                //new RunBatFile()
                //{
                //    Path = path
                //    ,
                //    Run = RUN
                //}
                //    .RunBat();

                //MessageBox.Show(path);

            } catch(Exception exception)
            {
                MessageBox.Show("Error> " + exception.Message + "\n\n" + exception.StackTrace);
            }
        }

        private void AddConfigurationFileDetails()
        {
        }
    }
}