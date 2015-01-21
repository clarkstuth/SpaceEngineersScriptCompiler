using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using SpaceEngineersScriptCompiler.Library.SyntaxWalkers;
using System.Collections.Generic;
using System.Collections.Concurrent;

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
        protected readonly Dictionary<string, ClassMetadata> _classMap;
        public IReadOnlyDictionary<string, ClassMetadata> ClassMap
        {
            get { return _classMap; }
        }

        /// <summary>
        /// Name of the sub class that contains a void Main() method.
        /// Null if none found.
        /// </summary>
        public string MainMethodClassName { get; protected set; }

        /// <summary>
        /// Root of the SyntaxTree of this file.
        /// </summary>
        public CSharpSyntaxTree SyntaxTreeRoot { get; protected set; }

        public FileMetadata(string filePath, CSharpSyntaxTree baseSyntaxTree)
        {
            SyntaxTreeRoot = baseSyntaxTree;
            FilePath = filePath;
            _classMap = new Dictionary<string, ClassMetadata>();

            FillClassMap(baseSyntaxTree);
        }

        private void FillClassMap(CSharpSyntaxTree syntaxTree)
        {
            var classes = syntaxTree.FindClasses();
            foreach (var className in classes.Keys)
            {
                var classMetadata = new ClassMetadata(classes[className]);

                _classMap.Add(className, classMetadata);

                if (classMetadata.GetMethodMap().ContainsKey("Main"))
                {
                    MainMethodClassName = className;
                }
            }
        }

    }
}
