using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SpaceEngineersScriptCompiler.Library.ScriptParts
{
    public class MainScriptPart : AbstractScriptPart
    {
        public MainScriptPart(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override string GetCode()
        {
            var treeRoot = SyntaxTree.GetRoot();

            var scriptWalker = new MainFindingScriptWalker();
            var mainMethodNode = scriptWalker.FindMain(treeRoot);

            return mainMethodNode.GetText().ToString().Trim();
        }

        class MainFindingScriptWalker : SyntaxWalker
        {
            static Type MainNodeType = 
                typeof(Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax);

            SyntaxNode MainNode = null;

            public SyntaxNode FindMain(SyntaxNode rootNode)
            {
                MainNode = null;
                Visit(rootNode);
                return MainNode;
            }

            public override void Visit(SyntaxNode node)
            {
                if (node.GetType() == MainNodeType)
                {
                    var mainMethodQuery =
                        from token in node.ChildTokens()
                        where token.Text == "Main"
                        select token;

                    if (mainMethodQuery.Any())
                    {
                        MainNode = node;
                        return;
                    }
                }

                base.Visit(node);
            }
        }

    }
}
