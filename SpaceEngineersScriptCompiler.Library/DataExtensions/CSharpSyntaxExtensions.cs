using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.SyntaxWalkers;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    static class CSharpSyntaxExtensions
    {
        public static string GetMethodName(this MethodDeclarationSyntax syntax)
        {
            return syntax.GetIdentifierToken().Text;
        }

        public static string GetClassName(this ClassDeclarationSyntax syntax)
        {
            return syntax.GetIdentifierToken().Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="syntax"></param>
        /// <returns>key is MethodName</returns>
        public static IReadOnlyDictionary<string, MethodDeclarationSyntax> GetPublicMethods(this ClassDeclarationSyntax syntax)
        {
            var walker = new PublicMethodWalker();
            return walker.FindTopLevelPublicMethods(syntax);
        }

        public static SyntaxToken GetIdentifierToken(this CSharpSyntaxNode syntax)
        {
            var identifierQuery = from token in syntax.ChildTokens()
                                  where token.CSharpKind() == SyntaxKind.IdentifierToken
                                  select token;

            return identifierQuery.FirstOrDefault();
        }

    }
}
