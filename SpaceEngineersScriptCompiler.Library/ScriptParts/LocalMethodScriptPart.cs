using Microsoft.CodeAnalysis;
using System;

namespace SpaceEngineersScriptCompiler.Library.ScriptParts
{
    class LocalMethodScriptPart : AbstractScriptPart
    {
        public LocalMethodScriptPart(SyntaxTree tree)
            : base(tree)
        {
        }

        public override string GetCode()
        {
            throw new NotImplementedException();
        }
    }
}
