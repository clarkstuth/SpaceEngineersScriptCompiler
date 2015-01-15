using System;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using SpaceEngineersScriptCompiler.Library.Exception;
using SpaceEngineersScriptCompiler.Library.File;
using SpaceEngineersScriptCompiler.Library.ScriptParts;

namespace SpaceEngineersScriptCompiler.Library
{
    public class ScriptBuilder
    {
        protected IFileAccess FileWrapper { get; set; }
        protected FileMetadataCollection FileCollection { get; set; }
        protected string FileName { get; set; }

        public ScriptBuilder(IFileAccess fileWrapper, FileMetadataCollection fileCollection)
        {
            FileWrapper = fileWrapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="SpaceEngineersScriptCompiler.Library.Exception.InvalidFileFormatException"></exception>
        /// <returns></returns>
        public MainScriptPart Build(string filePath)
        {
            ThrowExceptionIfFileDoesNotExit(filePath);

            var fileContents = FileWrapper.ReadAllText(filePath);
            var syntaxTree = CSharpSyntaxTree.ParseText(fileContents);
            var mainScript = new MainScriptPart(syntaxTree);

            ThrowExceptionIfScriptDoesNotValidate(mainScript, filePath);

            return mainScript;
        }

        private void ThrowExceptionIfFileDoesNotExit(string filePath)
        {
            if (!FileWrapper.Exists(filePath))
            {
                var error = String.Format("Unable to process file.  File not found: {0}", filePath);
                throw new FileNotFoundException(error);
            }
        }

        private static void ThrowExceptionIfScriptDoesNotValidate(MainScriptPart scriptPart, string fileName)
        {
            if (!scriptPart.Validate())
            {
                var error = String.Format("Unable to process file.  Parse error in file: {0}", fileName);
                throw new InvalidFileFormatException(error);
            }
        }

    }
}
