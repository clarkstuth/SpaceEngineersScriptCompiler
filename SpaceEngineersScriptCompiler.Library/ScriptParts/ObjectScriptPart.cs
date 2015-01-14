using System;
using Microsoft.CodeAnalysis;

namespace SpaceEngineersScriptCompiler.Library.ScriptParts
{
    public class ObjectScriptPart : AbstractScriptPart
    {
        public ObjectScriptPart(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override string GetCode()
        {
            throw new NotImplementedException();
        }
    }
}
