using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSODataGenerator
{
    class RESTServiceStartupClassWithODataGenerator
    {
        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string NameSpace { get; set; }
        public string References { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string nameSpace = "#namespace#";

        private const string ReferencesMask = "#references#";

        private const string entity = "#entity#";
        private const string controllerName = "#controllerName#";
        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = TemplatePath + TemplateSubPath + fileName + TemplateExtension;

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

            return ReadIntoString("Head")
                        .Replace(nameSpace, NameSpace + "ODataService")
                        .Replace(ReferencesMask, References)
                        ;

        } //GetHead

        public string GetMethods()
        {

            return
                ReadIntoString("Method")
                        .Replace(entity, Type.Name)
                        .Replace(controllerName, Type.Name + "EFRESTServiceOdata");
        } //GetMethods

        public string GetFoot()
        {
            return
                ReadIntoString("Foot");

        } //GetFoot


        public RESTServiceStartupClassWithODataGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, "Startup", OutputPath);

            return this;

        } // Generate

        public RESTServiceStartupClassWithODataGenerator Generate(Type type)
        {

            Type = type;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
