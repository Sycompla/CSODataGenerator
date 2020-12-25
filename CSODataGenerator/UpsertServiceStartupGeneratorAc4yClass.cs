using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class UpsertServiceStartupGeneratorAc4yClass
    {
        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }


        private const string TemplateExtension = ".csT";

        private const string nameSpace = "#namespace#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\UpsertServiceStartup\\" + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".cs", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetHead()
        {

            return ReadIntoString("head")
                        .Replace(nameSpace, Namespace)
                        ;

        } //GetHead

        public string GetMethods()
        {

            return
                ReadIntoString("methods");
        } //GetMethods

        public string GetFoot()
        {
            return
                ReadIntoString("foot");

        } //GetFoot


        public UpsertServiceStartupGeneratorAc4yClass Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, "Startup", OutputPath);

            return this;

        } // Generate


        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
