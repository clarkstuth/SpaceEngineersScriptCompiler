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
        protected ThreadSafeFileCollection FileCollection { get; set; }
        protected string FileName { get; set; }

        public ScriptBuilder(ThreadSafeFileCollection fileCollection)
        {
            FileCollection = fileCollection;
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
            ThrowExceptionIfFileDoesNotExist(filePath);

            var mainClassName = FileCollection[filePath].MainMethodClassName;

            var classMap = FileCollection[filePath].ClassMap;

            // TODO: Identify classes that contain a void Main() {} method.  Find a way to select those at this level.
            var mainSyntaxTree = classMap[mainClassName];

            var mainScript = new MainScriptPart(mainSyntaxTree);

            return mainScript;
        }

        private void ThrowExceptionIfFileDoesNotExist(string filePath)
        {
            if (!FileCollection.Keys.Contains(filePath))
            {
                var error = String.Format("Unable to process file.  File not found: {0}", filePath);
                throw new FileNotFoundException(error);
            }
        }

    }
}
