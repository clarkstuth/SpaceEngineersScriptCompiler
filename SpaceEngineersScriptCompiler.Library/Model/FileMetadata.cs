using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
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

        // TODO : Throw an exception if the SyntaxTree is invalid. (Somewhere)

        public FileMetadata(string filePath, SyntaxTree baseSyntaxTree)
        {
            SyntaxTreeRoot = baseSyntaxTree as CSharpSyntaxTree;
            FilePath = filePath;
            ClassMap = new ConcurrentDictionary<string, ClassMetadata>();

            FillClassMap(baseSyntaxTree);
        }

        private void FillClassMap(SyntaxTree syntaxTree)
        {
            var classes = syntaxTree.FindClasses();
            foreach (var className in classes.Keys)
            {
                var classMetadata = new ClassMetadata(classes[className]);

                var classSyntaxTree = CSharpSyntaxTree.ParseText(classes[className].GetText());
                ClassMap.TryAdd(className, classMetadata);

                InitializeMainMethodClassName();
            }
        }

        private void InitializeMainMethodClassName()
        {
            foreach (var className in ClassMap.Keys)
            {
                if (ClassMap[className].GetMethodMap().ContainsKey("Main"))
                {
                    MainMethodClassName = className;
                }
            }
        }



    }
}
