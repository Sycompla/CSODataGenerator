using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class RESTServiceProgramClassWithKestrelGenerator
    { 


        #region members

    public string TemplatePath { get; set; }
    public string TemplateSubPath { get; set; }
    public string OutputPath { get; set; }
    public string IPAddress { get; set; }
    public string PortNumber { get; set; }
    public string NameSpace { get; set; }

    public Type Type { get; set; }

    private const string TemplateExtension = ".csT";

    private const string nameSpace = "#namespace#";

    private const string ipAddress = "#ipAddress#";
    private const string portNumber = "#portNumber#";
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
                    .Replace(nameSpace, NameSpace)
                    ;

    } //GetHead

    public string GetMethods()
    {

        return
            ReadIntoString("Method")
                    .Replace(ipAddress, IPAddress)
                    .Replace(portNumber, PortNumber);
    } //GetMethods

    public string GetFoot()
    {
        return
            ReadIntoString("Foot");

    } //GetFoot


    public RESTServiceProgramClassWithKestrelGenerator Generate()
    {

        string result = null;

        result += GetHead();

        result += GetMethods();

        result += GetFoot();

        WriteOut(result, "Program", OutputPath);

        return this;

    } // Generate

    public RESTServiceProgramClassWithKestrelGenerator Generate(Type type)
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