using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.SyntaxWalkers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    public static class CSharpSyntaxExtensions
    {
        public static string GetMethodName(this MethodDeclarationSyntax syntax)
        {
            return GetIdentifierToken(syntax).Text;
        }

        public static string GetClassName(this ClassDeclarationSyntax syntax)
        {
            return GetIdentifierToken(syntax).Text;
        }

        public static Dictionary<string, MethodDeclarationSyntax> GetPublicMethods(this ClassDeclarationSyntax syntax) {
            var walker = new PublicMethodWalker();
            return walker.FindTopLevelPublicMethods(syntax);
        }

        public static CSharpSyntaxNode GetEndOfFile(this CSharpSyntaxNode node)
        {
            var walker = new EndOfFileWalker();
            return walker.FindEndOfFile(node);
        }

        private static SyntaxToken GetIdentifierToken(SyntaxNode syntax)
        {
            var identifierQuery = from token in syntax.ChildTokens()
                            where token.CSharpKind() == SyntaxKind.IdentifierToken
                            select token;

            return identifierQuery.FirstOrDefault();
        }
        
    }
}
