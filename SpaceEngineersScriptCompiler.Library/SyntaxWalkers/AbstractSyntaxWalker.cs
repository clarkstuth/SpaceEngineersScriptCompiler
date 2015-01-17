using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.DataExtensions;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.SyntaxWalkers
{
    // TODO - see if we actually need this abstract class.
    abstract class AbstractSyntaxWalker : SyntaxWalker
    {

        protected bool IsMainMethodValid(MethodDeclarationSyntax methodSyntax)
        {
            var methodName = methodSyntax.GetMethodName();

            var methodReturnTypeToken = from childNode in methodSyntax.ChildNodes()
                                        where childNode.CSharpKind() == SyntaxKind.PredefinedType
                                        select childNode;
            var returnType = methodReturnTypeToken.FirstOrDefault().GetText().ToString().Trim();

            var methodParameters = from childNode in methodSyntax.ChildNodes()
                                   where childNode.CSharpKind() == SyntaxKind.ParameterList
                                   select childNode;
            var parameterList = methodParameters.FirstOrDefault().ToString().Replace(" ", "");

            if (methodName == "Main" && returnType == "void" && parameterList == "()")
            {
                return true;
            }

            return false;
        }

    }
}
