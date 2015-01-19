using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SpaceEngineersScriptCompiler.Library.SyntaxWalkers;
using System.Collections.Generic;
using System.Linq;

namespace SpaceEngineersScriptCompiler.Library.DataExtensions
{
    static class CSharpSyntaxTreeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tree"></param>
        /// <returns>key is ClassName</returns>
        public static IReadOnlyDictionary<string, ClassDeclarationSyntax> FindClasses(this CSharpSyntaxTree tree)
        {
            var walker = new ClassFindingSyntaxWalker();
            return walker.GetClassMap(tree);
        }

        public static IReadOnlyList<string> FindPossibleDependencies(this CSharpSyntaxTree tree)
        {
            var possibleClassList = new List<string>();
            var walker = new ObjectCreationFindingSyntaxWalker();
            var root = tree.GetCompilationUnitRoot();

            var usings = root.Usings;
            var thisTreesNamespace = FindNamespace(tree);
            var declarations = walker.FindObjectCreationExpressions(root);

            foreach (var name in declarations.Keys)
            {
                foreach (var stmt in usings)
                {
                    possibleClassList.Add(string.Concat(stmt.Name, ".", name));
                }

                possibleClassList.Add(string.Concat(thisTreesNamespace, ".", name).Trim());
            }

            return possibleClassList;
        }

        private static string FindNamespace(CSharpSyntaxTree tree)
        {
            var thisTreesNamespace = (from node in tree.GetRoot().DescendantNodes()
                                         where node is NamespaceDeclarationSyntax
                                         select node as NamespaceDeclarationSyntax).FirstOrDefault();

            if (thisTreesNamespace == null) {
                return "";
            }

            return thisTreesNamespace.Name.ToString().Trim(); ;
        }

    }
}
