using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class ObjectServiceGenerator
    {

        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string References { get; set; }
        public string Namespace { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "ObjectService";

        private const string ClassCodeMask = "#classCode#";
        private const string SuffixMask = "#suffix#";
        private const string NamespaceMask = "#namespace#";
        private const string ReferencesMask = "#references#";

        private const string ClassCodeAsVariableMask = "#classCodeAsVariable#";

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
                        .Replace(ReferencesMask, References)
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

        public ObjectServiceGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetRequestResponseClasses();

            result += GetFoot();

            WriteOut(result, Type.Name + Suffix, OutputPath);

            return this;

        } // Generate

        public ObjectServiceGenerator Generate(Type type)
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