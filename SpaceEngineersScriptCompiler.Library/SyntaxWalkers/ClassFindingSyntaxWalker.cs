using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    /// <summary>
    /// Finds all top level classes within a given syntax tree.
    /// </summary>
    class ClassFindingSyntaxWalker : AbstractSyntaxWalker
    {
        public Dictionary<string, ClassDeclarationSyntax> ClassMap = null;

        public Dictionary<string, ClassDeclarationSyntax> GetClassMap(SyntaxTree tree)
        {
            ClassMap = new Dictionary<string, ClassDeclarationSyntax>();

            Visit(tree.GetRoot());

            return ClassMap;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is ClassDeclarationSyntax)
            {
                var classNode = node as ClassDeclarationSyntax;

                var className = classNode.GetClassName();

                ClassMap.Add(className, classNode);
            }
            else
            {
                base.Visit(node);
            }
        }
    }
}
