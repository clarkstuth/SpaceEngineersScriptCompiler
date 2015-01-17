using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.SyntaxWalkers;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    static class CSharpSyntaxTreeExtensions
    {
        public static Dictionary<string, ClassDeclarationSyntax> FindClasses(this SyntaxTree tree)
        {
            var walker = new ClassFindingSyntaxWalker();
            return walker.GetClassMap(tree);
        }
    }
}
