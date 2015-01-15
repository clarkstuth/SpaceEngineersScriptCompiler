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

            var mainMethodCode = mainMethodNode.GetText().ToString();

            // need to remove public, private, protected from the front of this method.
            // easy way to do this is to just find where void starts.
            var voidLocation = mainMethodCode.IndexOf("void");

            return mainMethodCode.Substring(voidLocation).Trim();
        }

        class MainFindingScriptWalker : SyntaxWalker
        {
            static Type MainNodeType = 
                typeof(Microsoft.CodeAnalysis.CSharp.Syntax.MethodDeclarationSyntax);

            SyntaxNode MainNode = null;

            /// <summary>
            /// Finds the first SyntaxNode containing a Main() method and returns it.
            /// </summary>
            /// <param name="rootNode"></param>
            /// <returns>SyntaxNode containing a Main() method.</returns>
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
