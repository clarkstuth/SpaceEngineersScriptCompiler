using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    public static class CSharpSyntaxTreeExtensions
    {
        public static Dictionary<string, SyntaxNode> FindClasses(this SyntaxTree tree)
        {
            var walker = new ClassFindingTreeWalker();

            walker.Visit(tree.GetRoot());

            return walker.ClassMap;
        }

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
                }
                else
                {
                    base.Visit(node);
                }
            }

        }

    }
}
