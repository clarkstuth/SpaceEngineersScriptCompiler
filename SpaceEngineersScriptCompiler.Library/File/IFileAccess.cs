namespace SpaceEngineersScriptCompiler.Library
{
    public interface IFileAccess
    {
        bool Exists(string filePath);

        string ReadAllText(string filePath);
    }
}