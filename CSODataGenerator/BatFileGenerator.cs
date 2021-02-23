using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSODataGenerator
{
    public class BatFileGenerator
    {

        #region members

        public string OutputPath { get; set; }
        public string InputPath { get; set; }
        public string RootDiectory { get; set; }
        public string Namespace { get; set; }
        public string PlanObjectNamespace { get; set; }
        public string GeneratorDirectory { get; set; }

        private const string TemplateExtension = ".txt";

        string RootDirectoryMask = "#rootDirectory#";
        string PlanObjectNamespaceMask = "#planObjectNamespace#";
        string namespaceMask = "#namespace#";
        string generatorDirectoryMask = "#generatorDirectory#";
        string RootDiskMask = "#rootDisk#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = InputPath + "Templates\\Bat\\" + fileName + TemplateExtension;

            return File.ReadAllText(textFile);

        } // ReadIntoString

        public void WriteOut(string text, string fileName, string outputPath)
        {
            File.WriteAllText(outputPath + fileName + ".bat", text);

        }

        public string GetNameWithLowerFirstLetter(String Code)
        {
            return
                char.ToLower(Code[0])
                + Code.Substring(1)
                ;

        } // GetNameWithLowerFirstLetter

        public string GetBatFile()
        {

            return ReadIntoString("GeneratorStart")
                        .Replace(RootDirectoryMask, RootDiectory)
                        .Replace(PlanObjectNamespaceMask, PlanObjectNamespace)
                        .Replace(namespaceMask, Namespace)
                        .Replace(generatorDirectoryMask, GeneratorDirectory)
                        .Replace(RootDiskMask, RootDiectory.Substring(0, 2))
                        ;

        }

        public BatFileGenerator Generate()
        {

            string result = null;

            result += GetBatFile();

            WriteOut(result, "GeneratorStart", OutputPath);

            return this;

        } // Generate

        public BatFileGenerator Generate(Type type)
        {

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

} // EFGenerala