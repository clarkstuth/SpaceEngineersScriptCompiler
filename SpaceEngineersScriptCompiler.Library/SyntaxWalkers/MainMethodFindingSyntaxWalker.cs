using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    /// <summary>
    /// Finds all top level void Main() methods within a syntax tree.
    /// </summary>
    class MainMethodFindingSyntaxWalker : AbstractSyntaxWalker
    {
        SyntaxNode MainNode = null;

        public SyntaxNode FindMain(SyntaxNode rootNode)
        {
            MainNode = null;
            Visit(rootNode);
            return MainNode;
        }

        // TODO : Make this process more efficient.
        // looking for a method that matches signature void Main()
        public override void Visit(SyntaxNode node)
        {
            if (node is MethodDeclarationSyntax)
            {
                var methodName = GetChildTokenIdentifier(node).Text.Trim();

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
                    MainNode = node;
                }

                // do not re-visit base if we are already on a function.
            }
            else
            {
                base.Visit(node);
            }
        }
    }
}
