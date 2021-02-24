using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    public class ObjectServiceGeneratorAc4yClass
    {

        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "ObjectService";

        private const string ClassCodeMask = "#classCode#";
        private const string SuffixMask = "#suffix#";
        private const string NamespaceMask = "#namespace#";

        private const string ClassCodeAsVariableMask = "#classCodeAsVariable#";
        private const string PlanObjectReferenceMask = "#planObjectReference#";
        private const string CapReferenceMask = "#capReference#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates/EFServiceTPC4CORE3/" + fileName + TemplateExtension;

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
                        .Replace(CapReferenceMask, Namespace + "Cap")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(SuffixMask, Suffix)
                        .Replace(NamespaceMask, Namespace + Suffix)
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

        public string GetRequestResponseClasses()
        {

            return
                ReadIntoString("RequestResponseClasses")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(
                                ClassCodeAsVariableMask
                                , GetNameWithLowerFirstLetter(Type.Name)
                            )
                ;

        } // RequestResponseClasses

        public string GetMethods()
        {

            return
                ReadIntoString("Methods")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(
                                ClassCodeAsVariableMask
                                , GetNameWithLowerFirstLetter(Type.Name)
                            )
                ;

        } // GetMethods

        public ObjectServiceGeneratorAc4yClass Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetRequestResponseClasses();

            result += GetFoot();

            WriteOut(result, Type.Name + Suffix, OutputPath);

            return this;

        } // Generate

        public ObjectServiceGeneratorAc4yClass Generate(Ac4yClass type)
        {

            Type = type;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    } // EFServiceGenerator

} // EFGeneralas