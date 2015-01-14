namespace SpaceEngineersScriptCompiler.Library.Exception
{
    public class InvalidFileFormatException : System.Exception
    {
        public InvalidFileFormatException(string message) : base(message)
        {
        }

        public InvalidFileFormatException(string message, System.Exception exception) : base(message, exception)
        {
        }
    }
}
