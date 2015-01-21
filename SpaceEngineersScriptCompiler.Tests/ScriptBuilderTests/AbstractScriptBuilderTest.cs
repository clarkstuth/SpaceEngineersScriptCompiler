using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library;
using SpaceEngineersScriptCompiler.Library.Model;
using System.Collections.Generic;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Library.Tests.ScriptBuilderTests
{
    public abstract class AbstractScriptBuilderTest
    {
        protected ScriptBuilder Builder { get; set; }
        protected IDependencyResolver DependencyResolver { get; set; }

        protected string GoodFilePath {get;set;}

        protected ThreadSafeFileCollection FileCollection { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            GoodFilePath = @"C:\Users\MyUser\SomeValidProject\SomeValidFile.cs";

            var bannedObjectNames = new List<string>{"GridTerminalSystem"};
            var bannedNamespaces = new List<string> { "System", "Sandbox" };

            FileCollection = new ThreadSafeFileCollection();
            DependencyResolver = new DefaultDependencyResolver(FileCollection, bannedObjectNames, bannedNamespaces);

            Builder = new ScriptBuilder(DependencyResolver, FileCollection); 
        }

        [TestCleanup]
        public void TearDown()
        {
            Mock.Reset();
        }

        protected FileMetadata CreateFileMetadata(string fileName, string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code) as CSharpSyntaxTree;
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
