using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class ContextGenerator
    {

        #region members

        public string TemplatePath { get; set; }
        public string TemplateSubPath { get; set; }
        public string OutputPath { get; set; }
        public string Namespace { get; set; }
        public string References { get; set; }
        public string ConnectionString { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csT";
        
        private const string ClassCodeMask = "#classCode#";
        private const string NamespaceMask = "#namespace#";
        private const string ReferencesMask = "#references#";
        private const string ConnectionStirngMask = "#connectionString#";

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
                        .Replace(NamespaceMask, Namespace + "Cap")
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
            return
                ReadIntoString("Methods")
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(ConnectionStirngMask, ConnectionString)
                ;
        }

        public ContextGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, "Controller", OutputPath);

            return this;

        } // Generate

        public ContextGenerator Generate(Type type)
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