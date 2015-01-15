using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library;
using SpaceEngineersScriptCompiler.Library.Exception;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests
{
    [TestClass]
    public class ScriptBuilderTest
    {
        protected ScriptBuilder Builder { get; set; }
        protected string GoodFilePath { get; set; }

        protected IFileAccess FileAccessStub { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            GoodFilePath = @"C:\Users\MyUser\SomeValidProject\SomeValidFile.cs";

            FileAccessStub = Mock.Create<IFileAccess>();
            Mock.Arrange(() => FileAccessStub.Exists(GoodFilePath)).Returns(true);

            Builder = new ScriptBuilder(FileAccessStub);
        }

        [TestCleanup]
        public void TearDown()
        {
            Mock.Reset();
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void BuildShouldThrowAnExceptionOnInvalidFilePath()
        {
            var badfilePath = @"Z:\Some\BadFile.cs";

            Mock.Arrange(() => FileAccessStub.Exists(badfilePath)).Returns(false);

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
        [ExpectedException(typeof(InvalidFileFormatException))]
        public void BuildShouldThrowAnExceptionIfFileContainsNoParsableSyntaxTree()
        {
            var fileContents = @"Clark\'s blog post!
                10 reasons I love cats.
                void Main() {int i = 0;}";

            Mock.Arrange(() => FileAccessStub.ReadAllText(GoodFilePath)).Returns(fileContents);

            try
            {
                Builder.Build(GoodFilePath);
            }
            catch (InvalidFileFormatException e)
            {
                var expectedMessage = "Unable to process file.  Parse error in file: " + GoodFilePath;
                Assert.AreEqual(expectedMessage, e.Message);
                throw;
            }
        }

        [TestMethod]
        public void BuildShouldReturnAMainScriptPartWithMatchingCodeBlockIfSyntaxIsValid()
        {
            var fileContents = @"void Main() { var myObj = new MyObject();  var result = myObj.SomeMethod(); }";

            Mock.Arrange(() => FileAccessStub.ReadAllText(GoodFilePath)).Returns(fileContents);

            
            var result = Builder.Build(GoodFilePath);
            
            Assert.AreEqual(fileContents, result.GetCode());
        }
        
    }
}
