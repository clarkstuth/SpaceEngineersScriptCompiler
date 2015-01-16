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
        public static SyntaxNode FindMainMethod(this SyntaxTree tree)
        {
            var walker = new MainMethodFindingSyntaxWalker();
            return walker.FindMain(tree.GetRoot());
        }

        public static Dictionary<string, SyntaxNode> FindClasses(this SyntaxTree tree)
        {
            var walker = new ClassFindingSyntaxWalker();
            return walker.GetClassMap(tree);
        }
    }
}
