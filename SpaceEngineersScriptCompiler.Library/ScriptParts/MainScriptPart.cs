using Microsoft.CodeAnalysis;

namespace SpaceEngineersScriptCompiler.Library.ScriptParts
{
    public class MainScriptPart : AbstractScriptPart
    {
        public MainScriptPart(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override string GetCode()
        {
            return SyntaxTree.GetText().ToString();
        }

    }
}
