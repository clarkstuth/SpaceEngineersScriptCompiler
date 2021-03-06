﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using SpaceEngineersScriptCompiler.Library.Exception;
using SpaceEngineersScriptCompiler.Library.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpaceEngineersScriptCompiler.Library
{
    public class ScriptBuilder
    {
        protected ThreadSafeFileCollection FileCollection { get; set; }
        protected IDependencyResolver DependencyResolver { get; set; }
        
        protected string FileName { get; set; }

        public ScriptBuilder(IDependencyResolver dependencyResolver, ThreadSafeFileCollection fileCollection)
        {
            DependencyResolver = dependencyResolver;
            FileCollection = fileCollection;
        }

        /// <summary>
        /// Builds a new complete output string from the given Source file.
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

            var classMetadata = classMap[mainClassName];
            var mainMethodNode = classMetadata.GetMethodMap()["Main"];

            // create Main() starter output string
            var stringBuilder = new StringBuilder(BuildSyntaxReturnString(mainMethodNode));

            // add other class methods and vars to output string
            var otherMethods = classMap[mainClassName].GetMethodMap();
            AddMethodsToScript(otherMethods, stringBuilder);
            var propsAndVars = classMap[mainClassName].Node.GetPropsAndVars();
            AddVarsToScript(propsAndVars, stringBuilder);

            // add other objects to output string
            var otherObjects = DependencyResolver.ResolveObjectDependencies(filePath);

            foreach (var depFilePath in otherObjects.Keys)
            {
                foreach (var depClassName in otherObjects[depFilePath])
                {
                    var classNode = FileCollection[depFilePath].ClassMap[depClassName];

                    var classBody = (classNode.Node as ClassDeclarationSyntax).WithModifiers(classNode.Node.Modifiers);

                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.Append(classBody.ToString().Trim());
                }
            }

            return stringBuilder.ToString();
        }

        private static void AddMethodsToScript(IReadOnlyDictionary<string, MethodDeclarationSyntax> otherMethods, StringBuilder stringBuilder)
        {
            foreach (var method in otherMethods.Keys)
            {
                if (method != "Main")
                {
                    var methodBody = (otherMethods[method] as MethodDeclarationSyntax).WithModifiers(new SyntaxTokenList());

                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine();
                    stringBuilder.Append(methodBody.ToString().Trim());
                }
            }
        }

        private static void AddVarsToScript(IEnumerable<SyntaxNode> propsAndVars, StringBuilder stringBuilder)
        {
            if (propsAndVars.Any())
            {
                stringBuilder.Insert(0, Environment.NewLine);
            }
            foreach (var prop in propsAndVars)
            {
                string body;
                if (prop is PropertyDeclarationSyntax)
                {
                    body = (prop as PropertyDeclarationSyntax).WithModifiers(new SyntaxTokenList()).ToString();
                }
                else
                {
                    body = (prop as VariableDeclarationSyntax).ToString() + ';';
                }
                    
                stringBuilder.Insert(0, Environment.NewLine);
                stringBuilder.Insert(0, body);
            }

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
            // I know all root nodes here should be a MethodDeclarationSyntax due to how the file is built.
            var outputTree = CSharpSyntaxTree.Create(syntaxNode).GetRoot() as MethodDeclarationSyntax;

            // remove all modifiers from the Main method (public, static, abstract, etc).
            outputTree = outputTree.WithModifiers(new SyntaxTokenList());

            return outputTree.ToString().Trim();
        }
    }
}

