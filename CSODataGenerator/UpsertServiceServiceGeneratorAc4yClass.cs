using Ac4yClassModule.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class UpsertServiceServiceGeneratorAc4yClass
    {

        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "Cap";

        private const string ClassNameMask = "#className#";
        private const string NamespaceMask = "#namespace#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\UpsertService\\" + fileName + TemplateExtension;

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

            return result;
        }

        public UpsertServiceServiceGeneratorAc4yClass Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, Type.Name + "Service", OutputPath);

            return this;

        } // Generate

        public UpsertServiceServiceGeneratorAc4yClass Generate(Ac4yClass type)
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