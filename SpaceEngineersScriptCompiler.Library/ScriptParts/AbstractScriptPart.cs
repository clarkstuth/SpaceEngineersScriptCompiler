using System.Linq;
using Microsoft.CodeAnalysis;

namespace SpaceEngineersScriptCompiler.Library.ScriptParts
{
    public abstract class AbstractScriptPart
    {
        protected SyntaxTree SyntaxTree { get; set; }

        protected AbstractScriptPart(SyntaxTree syntaxTree)
        {
            SyntaxTree = syntaxTree;
        }

        private bool ValidateSyntaxTree()
        {
            // GetDiagnostics returns parse errors surrounding the given piece of code.
            // The code does not need to include all constructs (namespace, class) but it must be parsable.
            return !SyntaxTree.GetDiagnostics().Any();
        }

        public virtual bool Validate()
        {
            return ValidateSyntaxTree();
        }

        public abstract string GetCode();
    }
}
