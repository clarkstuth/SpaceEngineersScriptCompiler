using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library;
using SpaceEngineersScriptCompiler.Library.File;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    public abstract class AbstractScriptBuilderTest
    {
        protected ScriptBuilder Builder { get; set; }

        protected string GoodFilePath {get;set;}

        protected ThreadSafeFileCollection FileCollection { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            GoodFilePath = @"C:\Users\MyUser\SomeValidProject\SomeValidFile.cs";

            FileCollection = new ThreadSafeFileCollection();

            Builder = new ScriptBuilder(FileCollection);
        }

        [TestCleanup]
        public void TearDown()
        {
            Mock.Reset();
        }

        protected FileMetadataModel CreateFileMetadata(string fileName, string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            return new FileMetadataModel(fileName, syntaxTree);
        }

        protected void AddFileMetadata(string fileName, string code)
        {
            var metadata = CreateFileMetadata(fileName, code);
            FileCollection.TryAdd(fileName, metadata);
        }

    }
}
