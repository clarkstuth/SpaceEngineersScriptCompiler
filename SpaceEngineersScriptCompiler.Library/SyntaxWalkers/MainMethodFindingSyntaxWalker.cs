using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    /// <summary>
    /// Finds all top level void Main() methods within a class syntax tree.
    /// </summary>
    class MainMethodFindingSyntaxWalker : AbstractSyntaxWalker
    {
        MethodDeclarationSyntax MainNode = null;

        public MethodDeclarationSyntax FindMain(CSharpSyntaxNode rootNode)
        {
            MainNode = null;
            Visit(rootNode);
            return MainNode;
        }

        // TODO : Make this process more efficient.
        // looking for a method that matches signature void Main()
        public override void Visit(SyntaxNode node)
        {
            if (MainNode != null)
            {
                return;
            }

            if (node is MethodDeclarationSyntax)
            {
                var methodNode = node as MethodDeclarationSyntax;

                var methodName = methodNode.GetMethodName();

                var methodReturnTypeToken = from childNode in node.ChildNodes()
                                            where childNode.CSharpKind() == SyntaxKind.PredefinedType
                                            select childNode;
                var returnType = methodReturnTypeToken.FirstOrDefault().GetText().ToString().Trim();

                var methodParameters = from childNode in node.ChildTokens()
                                       where childNode.CSharpKind() == SyntaxKind.ParameterList
                                       select childNode;
                var parameterList = methodParameters.FirstOrDefault().Text;

                if (methodName == "Main" && returnType == "void" && string.IsNullOrWhiteSpace(parameterList))
                {
                    MainNode = methodNode;
                }
            }
            else
            {
                base.Visit(node);
            }
        }
    }
}
