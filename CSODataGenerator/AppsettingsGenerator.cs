using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class AppsettingsGenerator
    {

        #region members

        public string OutputPath { get; set; }
        public string IpAddress { get; set; }
        public string System { get; set; }
        public string PortNumber { get; set; }
        public string DBIP { get; set; }
        public string DBName { get; set; }
        public string DBUserName { get; set; }
        public string DBPassword { get; set; }

        private const string TemplateExtension = ".txt";

        string IpAddressMask = "#ipAddress#";
        string SystemMask = "#system#";
        string PortNumberMask = "#portNumber#";
        string DatabaseIpMask = "#databaseIp#";
        string DatabaseNameMask = "#databaseName#";
        string UsernameMask = "#username#";
        string DatabasePasswordMask = "#databasePassword#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\Appsetting\\" + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".json", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetAppsettings()
        {

            return ReadIntoString("appsettingsTemplate")
                        .Replace(IpAddressMask, IpAddress)
                        .Replace(PortNumberMask, PortNumber)
                        .Replace(SystemMask, System)
                        .Replace(DatabaseIpMask, DBIP)
                        .Replace(DatabaseNameMask, DBName)
                        .Replace(DatabasePasswordMask, DBPassword)
                        .Replace(UsernameMask, DBUserName)
                        ;

        }

        public AppsettingsGenerator Generate()
        {

            string result = null;

            result += GetAppsettings();

            WriteOut(result, "appsettings", OutputPath);

            return this;

        } // Generate

        public AppsettingsGenerator Generate(Type type)
        {

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // EFGenerala