using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CSODataGenerator
{
    class RESTServiceStartupClassWithODataGenerator
    {
        #region members

        public string OutputPath { get; set; }
        public string NameSpace { get; set; }
        public CSODataGeneratorParameter Parameter { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string nameSpace = "#namespace#";

        private const string PlanObjectReferenceMask = "#planObjectReference#";

        private const string entity = "#entity#";
        private const string EntitySetsMask = "#entitySets#";

        private const string EntitySetsText = "builder.EntitySet<#entity#>(\"#entity#\");";
        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\RESTServiceODataStartupCORE3\\" + fileName + TemplateExtension;

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
                        .Replace(PlanObjectReferenceMask, Parameter.PlanObjectReferenceList[0].classType.Namespace)
                        ;

        } //GetHead

        public string GetMethods()
        {
            string entitySetsText = "";

            foreach (PlanObjectReference planObject in Parameter.PlanObjectReferenceList)
            {
                entitySetsText = entitySetsText + EntitySetsText.Replace(entity, planObject.className) + "\n";
            }

            return
                ReadIntoString("Method")
                        .Replace(EntitySetsMask, entitySetsText);
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
