using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class NuspecGenerator
    {

        #region members

        public string OutputPath { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Author { get; set; }

        private const string TemplateExtension = ".txt";

        string NameMask = "#name#";
        string VersionMask = "#version#";
        string AuthorMask = "#author#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\Nuget\\" + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".nuspec", text);

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

            return ReadIntoString("Nuspec")
                        .Replace(NameMask, Name + "ODataService")
                        .Replace(VersionMask, Version)
                        .Replace(AuthorMask, Author)
                        ;

        }

        public NuspecGenerator Generate()
        {

            string result = null;

            result += GetAppsettings();

            WriteOut(result, "Package", OutputPath);

            return this;

        } // Generate

        public NuspecGenerator Generate(Type type)
        {

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // EFGenerala