using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.ScriptParts
{
    public class MainScriptPart : AbstractScriptPart
    {
        public MainScriptPart(SyntaxTree syntaxTree) : base(syntaxTree)
        {
        }

        public override string GetCode()
        {
            var mainMethodNode = SyntaxTree.FindMainMethod();

            var mainMethodCode = mainMethodNode.GetText().ToString();

            // TODO: find a better way to remove the public and void from a Main method.
            var voidLocation = mainMethodCode.IndexOf("void");

            return mainMethodCode.Substring(voidLocation).Trim();
        }
    }
}
