
using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    public class PlanObjectGenerator
    {

        #region members

        public string OutputPath { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".txt";

        private const string Suffix = "Cap";

        private const string ClassNameMask = "#className#";
        private const string TypeMask = "#type#";
        private const string NamespaceMask = "#namespace#";
        private const string NameMask = "#name#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates/PlanObject/" + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + "/" + fileName + ".cs", text);

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
                        .Replace(NamespaceMask, Type.Namespace)
                        .Replace(ClassNameMask, Type.Name)
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
            string propertiesText = ReadIntoString("Body");
            string propertiesTextEdited = "";
            foreach(Ac4yProperty property in Type.PropertyList)
            {
                if (isCollection(property))
                {
                    propertiesTextEdited = propertiesTextEdited + propertiesText.Replace(TypeMask, "List<" + property.Type + ">")
                                                                                .Replace(NameMask, property.Name);

                }
                else
                {
                    propertiesTextEdited = propertiesTextEdited + propertiesText.Replace(TypeMask, property.Type)
                                                                                .Replace(NameMask, property.Name);

                }
            }

            return propertiesTextEdited;
        }

        public bool isCollection(Ac4yProperty property)
        {
            return
                property.Cardinality.Equals("COLLECTION");
        }

        public PlanObjectGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, Type.Name, OutputPath);

            return this;

        } // Generate

        public PlanObjectGenerator Generate(Ac4yClass type)
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