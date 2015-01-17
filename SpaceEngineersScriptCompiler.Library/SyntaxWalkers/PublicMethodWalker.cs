using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    class PublicMethodWalker : AbstractSyntaxWalker
    {
        Dictionary<string, MethodDeclarationSyntax> Methods = null;

        public Dictionary<string, MethodDeclarationSyntax> FindTopLevelPublicMethods(ClassDeclarationSyntax syntax)
        {
            Methods = new Dictionary<string, MethodDeclarationSyntax>();

            Visit(syntax);

            return Methods;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is MethodDeclarationSyntax)
            {
                var methodNode = node as MethodDeclarationSyntax;

                var methodName = methodNode.GetMethodName();
                Methods.Add(methodName, methodNode);
            }
            else
            {
                base.Visit(node);
            }            
        }
    }
}
