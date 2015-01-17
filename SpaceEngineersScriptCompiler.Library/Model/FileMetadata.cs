using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.Model
{
    public class FileMetadata
    {
        /// <summary>
        /// Path to this particular file.
        /// </summary>
        public string FilePath { get; protected set; }

        /// <summary>
        /// Map of classes that exist in a corresponding file's syntax tree.
        /// </summary>
        public ConcurrentDictionary<string, ClassMetadata> ClassMap { get; protected set; }

        /// <summary>
        /// Name of the sub class that contains a void Main() method.
        /// Null if none found.
        /// </summary>
        public string MainMethodClassName { get; protected set; }

        /// <summary>
        /// Root of the SyntaxTree of this file.
        /// </summary>
        public CSharpSyntaxTree SyntaxTreeRoot { get; protected set; }

        /// <summary>
        /// List of fully qualified class names we could possibly depend upon.
        /// Right now this is more of a guess than a determination.
        /// </summary>
        public List<string> ClassDependencies { get; protected set; }
        // TODO : Throw an exception if the SyntaxTree is invalid. (Somewhere not the constructor.)

        public FileMetadata(string filePath, SyntaxTree baseSyntaxTree)
        {
            SyntaxTreeRoot = baseSyntaxTree as CSharpSyntaxTree;
            FilePath = filePath;
            ClassMap = new ConcurrentDictionary<string, ClassMetadata>();
            ClassDependencies = new List<string>();

            FillClassMap(baseSyntaxTree);
            FillDependencyList(baseSyntaxTree);
        }

        private void FillClassMap(SyntaxTree syntaxTree)
        {
            var classes = syntaxTree.FindClasses();
            foreach (var className in classes.Keys)
            {
                var classMetadata = new ClassMetadata(classes[className]);

                var classSyntaxTree = CSharpSyntaxTree.ParseText(classes[className].GetText());
                ClassMap.TryAdd(className, classMetadata);

                if (classMetadata.GetMethodMap().ContainsKey("Main"))
                {
                    MainMethodClassName = className;
                }
            }
        }


        // TODO - this belongs somewhere else.
        private void FillDependencyList(SyntaxTree syntaxTree)
        {
            var usings = syntaxTree.GetCompilationUnitRoot().Usings;

            foreach (var ns in usings)
            {
                //Split on space to remove the using from the front of each namespace
                var nameSpace = ns.ToString().Split(' ')[1];
                //remove the semicolon from the end of the namespace
                nameSpace = nameSpace.Replace(';', ' ').Trim();

                var nsParts = ns.ToString().Split('.');
                var rootNs = nsParts[0];

                // see if this is a namespace we might want to care about
                // TODO: Make ignored Namespaces into a configurable parameter.
                if (rootNs != "Microsoft" || rootNs != "System" || rootNs != "Sandbox") {
                    
                    // TODO:  get names of declared objects within this executing script.
                    

                }
            }


        }
    }
}
