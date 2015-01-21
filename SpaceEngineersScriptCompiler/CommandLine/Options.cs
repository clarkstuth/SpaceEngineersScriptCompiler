using CommandLine;

namespace SpaceEngineersScriptCompiler.CommandLine
{
    class Options
    {
        [Option('s', "solution", Required = true)]
        public string SolutionPath { get; set; }
    }



}
