using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class RESTServiceODataControllerGenerator
    {
        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string Suffix = "Controller";

        private const string ClassCodeMask = "#classCode#";
        private const string SuffixMask = "#suffix#";
        private const string planObjectReferenceMask = "#planObjectReference#";
        private const string capReferenceMask = "#capReference#";
        private const string objectServiceReferenceMask = "#objectServiceReference#";
        private const string staticObjectServiceReferenceMask = "#staticObjectServiceReference#";
        private const string NamespaceMask = "#namespace#";


        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates/EFRESTServiceODataControllerTPC4CORE3/" + fileName + TemplateExtension;

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
                        .Replace(ClassCodeMask, Type.Name)
                        .Replace(SuffixMask, Suffix)
                        .Replace(NamespaceMask, Namespace + "ODataService")
                        .Replace(planObjectReferenceMask, Type.Namespace)
                        .Replace(capReferenceMask, Namespace + "Cap")
                        .Replace(objectServiceReferenceMask, Namespace + "ObjectService")
                        .Replace(staticObjectServiceReferenceMask, Namespace + "ObjectService." + Type.Name + "ObjectService")
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
            return
                ReadIntoString("Methods")
                        .Replace(ClassCodeMask, Type.Name)
                ;
        }

        public RESTServiceODataControllerGenerator Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, Type.Name + Suffix, OutputPath);

            return this;

        } // Generate

        public RESTServiceODataControllerGenerator Generate(Type type)
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