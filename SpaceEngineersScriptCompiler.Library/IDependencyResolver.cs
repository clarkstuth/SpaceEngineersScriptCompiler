using SpaceEngineersScriptCompiler.Library.Model;
using System;
using System.Collections.Generic;

namespace SpaceEngineersScriptCompiler.Library
{
    public interface IDependencyResolver
    {
        /// <summary>
        /// Returns a list of Files keyed with a list of Objects within the files.
        /// </summary>
        /// <param name="fileName">Name of the file to use as root for resolve dependencies.</param>
        /// <returns></returns>
        IDictionary<string, IList<string>> ResolveObjectDependencies(string fileName);

        // TODO: Make depenendency and property resolution methods on this same interface.

    }
}
