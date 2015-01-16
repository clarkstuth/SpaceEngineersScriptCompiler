using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    /// <summary>
    /// Finds all top level classes within a given syntax tree.
    /// </summary>
    class ClassFindingSyntaxWalker : AbstractSyntaxWalker
    {
        public Dictionary<string, SyntaxNode> ClassMap = null;

        public Dictionary<string, SyntaxNode> GetClassMap(SyntaxTree tree)
        {
            ClassMap = new Dictionary<string, SyntaxNode>();

            Visit(tree.GetRoot());

            return ClassMap;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is ClassDeclarationSyntax)
            {
                var className = GetChildTokenIdentifier(node).Text;

                ClassMap.Add(className, node);
            }
            else
            {
                base.Visit(node);
            }
        }
    }
}
