using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    /// <summary>
    /// Finds all top level classes within a given syntax tree.
    /// </summary>
    class ClassFindingSyntaxWalker : AbstractSyntaxWalker
    {
        private Dictionary<string, ClassDeclarationSyntax> ClassMap = null;
        private string Namespace { get; set; }

        public Dictionary<string, ClassDeclarationSyntax> GetClassMap(SyntaxTree tree)
        {
            ClassMap = new Dictionary<string, ClassDeclarationSyntax>();
            Namespace = null;

            Visit(tree.GetRoot());

            return ClassMap;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is ClassDeclarationSyntax)
            {
                var classNode = node as ClassDeclarationSyntax;
                var className = string.Concat(Namespace, ".", classNode.GetClassName());

                System.Console.WriteLine(className);

                ClassMap.Add(className, classNode);

                return;
            }
            
            if (node is NamespaceDeclarationSyntax)
            {
                Namespace = (node as NamespaceDeclarationSyntax).Name.ToString();
            }
            base.Visit(node);
        }
    }
}
