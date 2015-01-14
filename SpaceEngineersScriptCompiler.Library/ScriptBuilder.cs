using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SpaceEngineersScriptCompiler.Library.Exception;

namespace SpaceEngineersScriptCompiler.Library
{
    public class ScriptBuilder
    {
        protected IFileAccess FileWrapper { get; set; }

        public ScriptBuilder(IFileAccess fileWrapper)
        {
            FileWrapper = fileWrapper;
        }

        public void Build(string filePath)
        {
            if (!FileWrapper.Exists(filePath))
            {
                var error = String.Format("Unable to process file.  File not found: {0}", filePath);
                throw new FileNotFoundException(error);
            }

            var fileContents = FileWrapper.ReadAllText(filePath);

            var syntaxTree = CSharpSyntaxTree.ParseText(fileContents);

            // GetDiagnostics returns parse errors surrounding the given piece of code.
            // This does not need to include all constructs (namespace, class) but it must be parsable.
            if (syntaxTree.GetDiagnostics().Any())
            {
                var error = String.Format("Unable to process file.  Parse error in file: {0}", filePath);
                throw new InvalidFileFormatException(error);
            }

        }
    }
}
