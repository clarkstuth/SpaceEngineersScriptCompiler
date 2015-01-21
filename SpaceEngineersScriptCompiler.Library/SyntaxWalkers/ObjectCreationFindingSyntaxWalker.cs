using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    class ObjectCreationFindingSyntaxWalker : SyntaxWalker
    {
        private Dictionary<string, ObjectCreationExpressionSyntax> ObjectCreations;

        private List<string> IgnoredObjectNames;

        public ObjectCreationFindingSyntaxWalker(List<string> ignoredObjectNames)
        {
            IgnoredObjectNames = ignoredObjectNames;
        }

        public Dictionary<string, ObjectCreationExpressionSyntax> FindObjectCreationExpressions(CSharpSyntaxNode node)
        {
            ObjectCreations = new Dictionary<string, ObjectCreationExpressionSyntax>();
            Visit(node);
            return ObjectCreations;
        }

        public override void Visit(SyntaxNode node)
        {
            if (node is ObjectCreationExpressionSyntax)
            {
                var objNode = node as ObjectCreationExpressionSyntax;
                var name = objNode.Type.ToString();

                if (IgnoredObjectNames.Any() && !IgnoredObjectNames.Contains(name))
                {
                    ObjectCreations.Add(name, objNode);
                }
            }

            base.Visit(node);
        }

    }
}