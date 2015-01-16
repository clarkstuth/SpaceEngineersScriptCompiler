using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    public static class CSharpSyntaxExtensions
    {
        public static string GetMethodName(this MethodDeclarationSyntax syntax)
        {
            var methodNameQuery = from token in syntax.ChildTokens()
                                  where token.CSharpKind() == SyntaxKind.IdentifierToken
                                  select token;

            return methodNameQuery.FirstOrDefault().Text;
        }
    }
}
