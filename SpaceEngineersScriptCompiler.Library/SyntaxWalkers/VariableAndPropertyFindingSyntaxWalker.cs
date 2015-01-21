using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    class VariableAndPropertyFindingSyntaxWalker : SyntaxWalker
    {
        List<CSharpSyntaxNode> VarsAndProps { get; set; }

        public IEnumerable<CSharpSyntaxNode> FindVariablesAndProperties(ClassDeclarationSyntax node)
        {
            VarsAndProps = new List<CSharpSyntaxNode>();
            Visit(node);
            return VarsAndProps;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is VariableDeclarationSyntax ||
                node is PropertyDeclarationSyntax)
            {
                VarsAndProps.Add(node as CSharpSyntaxNode);
            }
            else if (!(node is MethodDeclarationSyntax))
            {
                base.Visit(node);
            }
        }
    }
}
