using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using SpaceEngineersScriptCompiler.Library.Exception;
using SpaceEngineersScriptCompiler.Library.File;
using SpaceEngineersScriptCompiler.Library.Model;
using System;
using System.IO;
using System.Linq;

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

            // add other class methods to output class
            var otherMethods = classMap[mainClassName].GetMethodMap();

            foreach (var method in otherMethods.Keys)
            {
                if (method != "Main")
                {
                    
                    //mainMethodNode.InsertNodesAfter(mainMethodNode, otherMethods[method].DescendantNodesAndTokensAndSelf());
                }
            }

            return BuildSyntaxReturnString(mainMethodNode);
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

        private string BuildSyntaxReturnString(CSharpSyntaxNode syntaxNode)
        {
            var outputTree = CSharpSyntaxTree.Create(syntaxNode).GetRoot() as MethodDeclarationSyntax;

            // remove all modifiers from the Main method (public, static, sealed etc).
            outputTree = outputTree.WithModifiers(new SyntaxTokenList());

            return outputTree.ToString().Trim();
        }

    }
}

