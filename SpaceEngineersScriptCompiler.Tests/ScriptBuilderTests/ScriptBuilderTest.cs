using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library.Exception;
using SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests
{
    [TestClass]
    public class ScriptBuilderTest : AbstractScriptBuilderTest
    {
        
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
            var fileContents =
                @"namespace MyNamespace { class MyClass { void Main() {var myObj = new MyObject();  var result = myObj.SomeMethod();}}}";

            var expectedCode = "void Main() {var myObj = new MyObject();  var result = myObj.SomeMethod();}";

            AddFileMetadata(GoodFilePath, fileContents);

            var result = Builder.Build(GoodFilePath);
            
            Assert.AreEqual(expectedCode, result);
        }
        
        [TestMethod]
        [ExpectedException(typeof(MainMethodNotFoundException))]
        public void BuildShouldThrowAnExceptionIfNoMainMethodIsFound()
        {
            var fileContents = @"namespace MyNamespace { class MyClass { void MyMethod() {} }}";
            
            AddFileMetadata(GoodFilePath, fileContents);

            try
            {
                Builder.Build(GoodFilePath);
            }
            catch (MainMethodNotFoundException e)
            {
                var expectedMessage = "No valid \"void Main(){}\" method was found in file: " + GoodFilePath;
                Assert.AreEqual(expectedMessage, e.Message);
                throw;
            }    
        }

    }
}
