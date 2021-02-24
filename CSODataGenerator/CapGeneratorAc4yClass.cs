using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    public class CapGeneratorAc4yClass
    {

        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "Cap";

        private const string ClassCodeMask = "#classCode#";
        private const string SuffixMask = "#suffix#";
        private const string NamespaceMask = "#namespace#";
        private const string PlanObjectReferenceMask = "#planObjectReference#";
        private const string NavigationIdValueMask = "#NavigationIdValue#";
        private const string NavigationIdMask = "#NavigationId#";

        private const string ClassCodeAsVariableMask = "#classCodeAsVariable#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates/EFCAPTPC4CORE3/" + fileName + TemplateExtension;

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
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(SuffixMask, Suffix)
                        .Replace(NamespaceMask, Namespace + "Cap")
                        ;

        }

        public string GetFoot()
        {
            return
                ReadIntoString("Foot")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(SuffixMask, Suffix)
                        ;

        }

        public string GetMethods()
        {
            string result =
                ReadIntoString("Methods")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(
                                ClassCodeAsVariableMask
                                , GetNameWithLowerFirstLetter(Type.Name)
                            )
                ;

            foreach(Ac4yProperty property in Type.PropertyList)
            {
                if(property.NavigationProperty == true)
                {
                    result = result.Replace(NavigationIdMask, "int NavigationId = actual." + property.Name + "Id;")
                          .Replace(NavigationIdValueMask, "actual." + property.Name + "Id = NavigationId;");
                    return result;
                }
                else if(Type.PropertyList[Type.PropertyList.Count - 1] == property)
                {
                    result = result.Replace(NavigationIdMask, "").Replace(NavigationIdValueMask, "");
                }
            }

            return result;
        }

        public CapGeneratorAc4yClass Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, Type.Name + Suffix, OutputPath);

            return this;

        } // Generate

        public CapGeneratorAc4yClass Generate(Ac4yClass type)
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