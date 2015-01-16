using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    abstract class AbstractSyntaxWalker : SyntaxWalker
    {
        protected SyntaxToken GetChildTokenIdentifier(SyntaxNode node)
        {
            var tokenSelectionQuery = from token in node.ChildTokens()
                    where token.CSharpKind() == SyntaxKind.IdentifierToken
                    select token;

            return tokenSelectionQuery.FirstOrDefault();
        }
    }
}
