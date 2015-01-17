using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library;
using SpaceEngineersScriptCompiler.Library.Model;
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

        protected FileMetadata CreateFileMetadata(string fileName, string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            return new FileMetadata(fileName, syntaxTree);
        }

        protected void AddFileMetadata(string fileName, string code)
        {
            var metadata = CreateFileMetadata(fileName, code);
            FileCollection.TryAdd(fileName, metadata);
        }

        protected void RunCodeParseTestAndAssert(string code, string expectedCode)
        {
            AddFileMetadata(GoodFilePath, code);

            var result = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, result);
        }

    }
}
