using SpaceEngineersScriptCompiler.Library.Model;
using System.Collections.Concurrent;

namespace SpaceEngineersScriptCompiler.Library.Model
{
    /// <summary>
    /// Key is the file path to a specific piece of metadata.  Value is the Metadata saved for the specific file.
    /// </summary>
    public class ThreadSafeFileCollection : ConcurrentDictionary<string, FileMetadata>
    {
    }
}