using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.SyntaxWalkers;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    public static class CSharpSyntaxTreeExtensions
    {
        public static Dictionary<string, ClassDeclarationSyntax> FindClasses(this SyntaxTree tree)
        {
            var walker = new ClassFindingSyntaxWalker();
            return walker.GetClassMap(tree);
        }
    }
}
