using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using SpaceEngineersScriptCompiler.Library.Exception;
using SpaceEngineersScriptCompiler.Library.File;
using SpaceEngineersScriptCompiler.Library.Model;

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
        public string Build(string filePath)
        {
            ThrowExceptionIfFileDoesNotExist(filePath);
            ThrowExceptionIfNoValidMainMethod(filePath);

            var mainClassName = FileCollection[filePath].MainMethodClassName;
            var classMap = FileCollection[filePath].ClassMap;

            var primaryClassNode = classMap[mainClassName].Node;
            var mainMethodNode = primaryClassNode.FindMainMethod();

            var buildOutput = CSharpSyntaxTree.Create(mainMethodNode).ToString();

            var voidPos = buildOutput.IndexOf("void");
            return buildOutput.Substring(voidPos).Trim();
        }

        private void ThrowExceptionIfFileDoesNotExist(string filePath)
        {
            if (!FileCollection.Keys.Contains(filePath))
            {
                var error = String.Format("Unable to process file.  File not found: {0}", filePath);
                throw new FileNotFoundException(error);
            }
        }

        private void ThrowExceptionIfNoValidMainMethod(string filePath)
        {
            if (FileCollection[filePath].MainMethodClassName == null)
            {
                var error = "No valid \"void Main(){}\" method was found in file: " + filePath;
                throw new MainMethodNotFoundException(error);
            }
        }

    }
}

