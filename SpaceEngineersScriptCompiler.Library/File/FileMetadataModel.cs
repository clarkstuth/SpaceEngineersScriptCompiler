using System.IO;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;

namespace SpaceEngineersScriptCompiler.Library.File
{
    class FileMetadataModel
    {
        /// <summary>
        /// Path to this particular file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Map of methods within this given file and the corresponding method's syntax tree.
        /// </summary>
        public ConcurrentDictionary<string, SyntaxTree> MethodMap { get; set; }
    }
}
