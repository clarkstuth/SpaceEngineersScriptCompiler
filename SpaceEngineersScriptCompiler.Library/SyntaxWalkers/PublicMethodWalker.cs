using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    class PublicMethodWalker : SyntaxWalker
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

                if (methodName == "Main")
                {
                    if (IsMainMethodValid(methodNode))
                    {
                        Methods.Add(methodName, methodNode);
                    }
                }
                else
                {
                    Methods.Add(methodName, methodNode);
                }
            }
            else
            {
                base.Visit(node);
            }            
        }

        protected bool IsMainMethodValid(MethodDeclarationSyntax methodSyntax)
        {
            var methodName = methodSyntax.GetMethodName();

            var methodReturnTypeToken = from childNode in methodSyntax.ChildNodes()
                                        where childNode.CSharpKind() == SyntaxKind.PredefinedType
                                        select childNode;
            var returnType = methodReturnTypeToken.FirstOrDefault().GetText().ToString().Trim();

            var methodParameters = from childNode in methodSyntax.ChildNodes()
                                   where childNode.CSharpKind() == SyntaxKind.ParameterList
                                   select childNode;
            var parameterList = methodParameters.FirstOrDefault().ToString().Replace(" ", "");

            if (methodName == "Main" && returnType == "void" && parameterList == "()")
            {
                return true;
            }

            return false;
        }

    }
}
