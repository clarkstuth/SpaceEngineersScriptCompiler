using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    public static class CSharpSyntaxTreeExtensions
    {
        public static SyntaxNode FindMainMethod(this SyntaxTree tree)
        {
            var walker = new MainMethodFindingTreeWalker();

            return walker.FindMain(tree.GetRoot());
        }

        public static Dictionary<string, SyntaxNode> FindClasses(this SyntaxTree tree)
        {
            var walker = new ClassFindingTreeWalker();

            walker.Visit(tree.GetRoot());

            return walker.ClassMap;
        }

        /// <summary>
        /// Finds all top level classes within a given syntax tree.
        /// </summary>
        class ClassFindingTreeWalker : SyntaxWalker
        {
            public Dictionary<string, SyntaxNode> ClassMap = null;

            public override void Visit(SyntaxNode node)
            {
                ClassMap = new Dictionary<string, SyntaxNode>();

                if (node is ClassDeclarationSyntax)
                {
                    var tokenSelectionQuery =
                        from token in node.ChildTokens()
                        where token.CSharpKind() == SyntaxKind.IdentifierToken
                        select token;

                    var className = tokenSelectionQuery.FirstOrDefault().Text;

                    ClassMap.Add(className, node);

                    // Do not visit Base.  This stops recursion on this specific leg of the syntax tree.
                }
                else
                {
                    base.Visit(node);
                }
            }
        }

        /// <summary>
        /// Finds all top level void Main() methods within a syntax tree.
        /// </summary>
        class MainMethodFindingTreeWalker : SyntaxWalker
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
                    var methodNameToken = from token in node.ChildTokens()
                                          where token.CSharpKind() == SyntaxKind.IdentifierToken
                                          select token;
                    var methodName = methodNameToken.FirstOrDefault().Text.Trim();

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
}
