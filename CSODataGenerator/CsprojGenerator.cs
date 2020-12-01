using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class CsprojGenerator
    {

        #region members

        public string OutputPath { get; set; }
        public string Name { get; set; }

        public Type Type { get; set; }

        private const string TemplateExtension = ".csproj";

        private const string Suffix = "Cap";

        private const string ProjectMask = "</project>";
        private const string ItemGroup = "  <ItemGroup>\n" +
                                            "    < None Update=\"Document\\index.html\">\n" +
                                                "      <CopyToOutputDirectory>Always</CopyToOutputDirectory>\n" +
                                            "    </None>\n" +
                                         "  </ItemGroup>\n\n" +
                                        "</project>";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = OutputPath + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".csproj", text);

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
            return ReadIntoString(Name + "ODataService")
                        .Replace(ProjectMask, ItemGroup)
                        ;

        }
        public CsprojGenerator Generate()
        {

            string result = null;

            result += GetHead();

            WriteOut(result, Name + "ODataService", OutputPath);

            return this;

        } // Generate

        public CsprojGenerator Generate(Type type)
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