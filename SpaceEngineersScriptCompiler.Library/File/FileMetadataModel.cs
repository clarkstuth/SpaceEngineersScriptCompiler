using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Concurrent;
using System.IO;

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
            }
        }

    }
}
