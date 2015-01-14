namespace SpaceEngineersScriptCompiler.Library.File
{
    public class FileImplementation : IFileAccess
    {
        public bool Exists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        public string ReadAllText(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }
    }
}
