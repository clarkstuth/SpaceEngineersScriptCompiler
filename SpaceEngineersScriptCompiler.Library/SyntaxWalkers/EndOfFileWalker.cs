using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    class EndOfFileWalker : SyntaxWalker
    {
        CSharpSyntaxNode EndOfFile = null;

        public CSharpSyntaxNode FindEndOfFile(CSharpSyntaxNode node)
        {
            EndOfFile = null;
            Visit(node);
            return EndOfFile;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is BlockSyntax)
            {
                EndOfFile = node as CSharpSyntaxNode;
            }
            else
            {
                base.Visit(node);
            }            
        }
    }
}
