using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library.Model
{
    public class ClassMetadata
    {
        public ClassDeclarationSyntax Node { get; protected set; }

        protected Dictionary<string, MethodDeclarationSyntax> MethodMap { get; set; }

        public ClassMetadata(ClassDeclarationSyntax node)
        {
            Node = node;
        }

        public Dictionary<string, MethodDeclarationSyntax> GetMethodMap()
        {
            if (MethodMap == null)
            {
                PopulateMethodMap();
            }

            return MethodMap;
        }

        private void PopulateMethodMap()
        {
            MethodMap = Node.GetPublicMethods();
        }

    }
}
