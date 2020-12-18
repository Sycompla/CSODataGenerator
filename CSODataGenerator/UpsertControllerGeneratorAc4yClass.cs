using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class UpsertControllerGeneratorAc4yClass
    {

        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }
        public string OdataUrl { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "Cap";

        private const string ClassNameMask = "#className#";
        private const string NamespaceMask = "#namespace#";
        private const string PlanObjectReferenceMask = "#planObjectReference#";
        private const string PropertyNameMask = "#propertyName#";
        private const string jsonIgnoredListMask = "#jsonIgnoredList#";

        private const string jsonIgnoredListText = "newObject.#propertyName# = null;";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\UpsertController\\" + fileName + TemplateExtension;

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
                        .Replace(PlanObjectReferenceMask, Type.Namespace)
                        .Replace(ClassNameMask, Type.Name)
                        .Replace(NamespaceMask, Namespace)
                        ;

        }

        public string GetFoot()
        {
            return
                ReadIntoString("Foot")
                        ;

        }

        public string GetMethods()
        {
            string result =
                ReadIntoString("Methods")
                        .Replace(ClassNameMask, Type.Name)
                ;

            string lists = "";

            foreach(Ac4yProperty property in Type.PropertyList)
            {
                if(property.Cardinality.Equals("COLLECTION"))
                {
                    lists = lists + jsonIgnoredListText.Replace(PropertyNameMask, property.Name) + "\n";
                }
            }

            result = result.Replace(jsonIgnoredListMask, lists);

            return result;
        }

        public UpsertControllerGeneratorAc4yClass Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, Type.Name + "UpsertController", OutputPath);

            return this;

        } // Generate

        public UpsertControllerGeneratorAc4yClass Generate(Ac4yClass type)
        {

            Type = type;

            return Generate();

        } // Generate


        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // EFGenerala