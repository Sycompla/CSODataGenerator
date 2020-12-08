using Ac4yClassModule.Class;
using CSAc4yModule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSODataGenerator
{
    class ContextGeneratorAc4yClass
    {

        #region members

        public string OutputPath { get; set; }
        public string Namespace { get; set; }
        public string References { get; set; }
        public string ConnectionString { get; set; }
        public Ac4yModule Parameter { get; set; }

        public Ac4yClass Type { get; set; }

        private const string TemplateExtension = ".csT";

        private const string ClassCodeMask = "#classCode#";
        private const string NamespaceMask = "#namespace#";
        private const string PlanObjectReferenceMask = "#planObjectReference#";
        private const string ConnectionStirngMask = "#connectionString#";
        private const string DbSetsMask = "#dbSets#";
        private const string EntitiesMask = "#entities#";
        private const string EntityMask = "#entity#";
        private const string EntityOneMask = "#entityOne#";
        private const string PropertyManyMask = "#propertyMany#";
        private const string ReferencePropertyMask = "#referenceProperty#";
        private const string ConnectionsMask = "#connections#";

        private const string DbSetsText = "public DbSet<#classCode#> #classCode#s { get; set; }";
        private const string EntitiesText = "modelBuilder.Entity<#classCode#>().ToTable(\"#classCode#s\");";

        private const string ClassCodeAsVariableMask = "#classCodeAsVariable#";

        #endregion members

        public string ReadIntoString(string fileName)
        {

            string textFile = "Templates\\EFContextTPC4CORE3\\" + fileName + TemplateExtension;

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
            string entitiesText = "";
            string dbSetsText = "";
            List<Ac4yClass> connectionClasses = new List<Ac4yClass>();
            string editedConnectionsText = "";

            foreach (Ac4yClass ac4yClass in Parameter.Ac4yClassList)
            {
                

                foreach (Ac4yProperty property in ac4yClass.PropertyList)
                {
                    if (property.NavigationProperty == true)
                    {
                        connectionClasses.Add(ac4yClass);
                        break;
                    }
                }
            }

            foreach (Ac4yClass ac4yClass in connectionClasses)
            {
                string connectionsText = ReadIntoString("Connections")
                                            .Replace(EntityMask, ac4yClass.Name)
                                            .Replace(PropertyManyMask, ac4yClass.Name + "s")
                                            ;

                foreach (Ac4yProperty property in ac4yClass.PropertyList)
                {

                    if (property.NavigationProperty == true)
                    {
                        connectionsText = connectionsText.Replace(EntityOneMask, property.Name)
                                                            .Replace(ReferencePropertyMask, property.Name + "Id");
                    }
                }

                editedConnectionsText = editedConnectionsText + connectionsText;
            }

            foreach (Ac4yClass ac4yClass in Parameter.Ac4yClassList)
            {
                entitiesText = entitiesText + EntitiesText.Replace(ClassCodeMask, ac4yClass.Name) + "\n";
            }

            foreach (Ac4yClass ac4yClass in Parameter.Ac4yClassList)
            {
                dbSetsText = dbSetsText + DbSetsText.Replace(ClassCodeMask, ac4yClass.Name) + "\n";
            }

            return
                ReadIntoString("Methods")
                        .Replace(EntitiesMask, entitiesText)
                        .Replace(DbSetsMask, dbSetsText)
                        .Replace(ConnectionStirngMask, ConnectionString)
                        .Replace(ConnectionsMask, editedConnectionsText)
                ;
        }

        public ContextGeneratorAc4yClass Generate()
        {

            string result = null;

            result += GetHead();

            result += GetMethods();

            result += GetFoot();

            WriteOut(result, "Context", OutputPath);

            return this;

        } // Generate

        public ContextGeneratorAc4yClass Generate(Ac4yClass type)
        {

            Type = type;

            return Generate();

        } // Generate

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }

    public class Ac4yCléass
    {
    }
} // EFGenerala