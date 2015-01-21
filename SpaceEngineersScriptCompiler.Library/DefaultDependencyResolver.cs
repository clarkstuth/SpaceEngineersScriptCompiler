using SpaceEngineersScriptCompiler.Library.DataExtensions;
using SpaceEngineersScriptCompiler.Library.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceEngineersScriptCompiler.Library
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        protected ThreadSafeFileCollection FileCollection { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileCollection">Collection of all Metadata to be scanned for Dependencies.</param>
        public DefaultDependencyResolver(ThreadSafeFileCollection fileCollection)
        {
            FileCollection = fileCollection;
        }

        public IDictionary<string, IList<string>> ResolveObjectDependencies(string fileName)
        {
            var resultDict = new Dictionary<string, IList<string>>();
            ResolveDependencyList(fileName, resultDict, new List<string>());
            return resultDict;
        }

        /// <summary>
        /// Recursive method to resolve dependencies.
        /// </summary>
        /// <param name="filePath">Name of the file to resolve object dependencies for.</param>
        /// <param name="fileAndClassMap">Map of files to be filled with dependency information.</param>
        /// <param name="alreadyProcessedFiles">Collection of files already processed used to end recursion.</param>
        protected void ResolveDependencyList(string filePath, IDictionary<string, IList<string>> fileAndClassMap, IList<string> alreadyProcessedFiles)
        {
            var fileMetadata = FileCollection[filePath];

            if (alreadyProcessedFiles.Contains(filePath))
            {
                return;
            }
            alreadyProcessedFiles.Add(filePath);

            var possibleDependencies = fileMetadata.SyntaxTreeRoot.FindPossibleDependencies();

            foreach (var fileName in FileCollection.Keys)
            {
                var matchingClasses = FileCollection[fileName].ClassMap.Keys.Intersect(possibleDependencies);

                if (!fileAndClassMap.ContainsKey(fileName))
                {
                    fileAndClassMap[fileName] = new List<string>();
                }

                foreach (var className in matchingClasses)
                {
                    if (!fileAndClassMap[fileName].Contains(className))
                    {
                        fileAndClassMap[fileName].Add(className);
                    }
                }
            }

            foreach (var depFilePath in fileAndClassMap.Keys)
            {
                ResolveDependencyList(depFilePath, fileAndClassMap, alreadyProcessedFiles);
            }
        }
    }
}