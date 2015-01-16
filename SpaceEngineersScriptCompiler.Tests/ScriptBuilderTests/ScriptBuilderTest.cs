using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library;
using SpaceEngineersScriptCompiler.Library.Exception;
using SpaceEngineersScriptCompiler.Library.File;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests
{
    [TestClass]
    public class ScriptBuilderTest
    {
        protected ScriptBuilder Builder { get; set; }

        protected string GoodFilePath { get; set; }

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

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void BuildShouldThrowAnExceptionOnInvalidFilePath()
        {
            var badfilePath = @"Z:\Some\BadFile.cs";

            try
            {
                Builder.Build(badfilePath);
            }
            catch (FileNotFoundException e)
            {
                var expectedMessage = @"Unable to process file.  File not found: " + badfilePath;
                Assert.AreEqual(expectedMessage, e.Message);
                throw;
            }
        }

        [TestMethod]
        public void BuildShouldReturnAMainScriptPartWithMatchingCodeBlockIfSyntaxIsValid()
        {
            var fileContents = @"void Main() { var myObj = new MyObject();  var result = myObj.SomeMethod(); }";
            AddFileMetadata(GoodFilePath, fileContents);

            var result = Builder.Build(GoodFilePath);
            
            Assert.AreEqual(fileContents, result.GetCode());
        }
        
    }
}
