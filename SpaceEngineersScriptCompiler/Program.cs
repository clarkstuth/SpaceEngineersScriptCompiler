namespace SpaceEngineersScriptCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new Console();
            var output = console.Run(args);
            System.Console.WriteLine(output);
        }
    }
}
