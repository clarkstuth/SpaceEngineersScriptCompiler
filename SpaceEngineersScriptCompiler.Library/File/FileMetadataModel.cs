using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Concurrent;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.File
{
    public class FileMetadataModel
    {
        /// <summary>
        /// Path to this particular file.
        /// </summary>
        public string FilePath { get; protected set; }

        /// <summary>
        /// Map of classes that exist in a corresponding file's syntax tree.
        /// </summary>
        public ConcurrentDictionary<string, SyntaxTree> ClassMap { get; protected set; }

        /// <summary>
        /// Name of the sub class that contains a void Main() method.
        /// Null if none found.
        /// </summary>
        public string MainMethodClassName { get; protected set; }

        // TODO : Throw an exception if the SyntaxTree is invalid. (Somewhere)

        public FileMetadataModel(string filePath, SyntaxTree baseSyntaxTree)
        {
            FilePath = filePath;
            ClassMap = new ConcurrentDictionary<string, SyntaxTree>();

            FillClassMap(baseSyntaxTree);
        }

        private void FillClassMap(SyntaxTree syntaxTree)
        {
            var classes = syntaxTree.FindClasses();
            foreach (var className in classes.Keys)
            {
                var classSyntaxTree = CSharpSyntaxTree.ParseText(classes[className].GetText());
                ClassMap.TryAdd(className, classSyntaxTree);

                InitializeMainMethodClassName(className, classSyntaxTree);
            }
        }

        private void InitializeMainMethodClassName(string className, SyntaxTree classSyntaxTree)
        {
            var classMethods = classSyntaxTree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();

            foreach (var method in classMethods)
            {
                if (method.GetMethodName() == "Main")
                {
                    MainMethodClassName = className;
                }
            }
        }



    }
}
