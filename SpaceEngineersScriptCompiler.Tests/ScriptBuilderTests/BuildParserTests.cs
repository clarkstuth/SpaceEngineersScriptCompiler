using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    [TestClass]
    public class BuildParserTests : ScriptBuilderTest
    {
        [TestMethod]
        public void BuildShouldRemoveNamespaceAndClassDeclarationsFromCodeSurroundingMain()
        {
            var code = @"
            namespace MyApp.MyNamespace.SubNamespace
            {
                public class MyClass
                {
                    public void Main() {var i = 0;}
                }
            }";

            var expectedCode = "public void Main() {var i = 0;}";

            Mock.Arrange(() => FileAccessStub.ReadAllText(GoodFilePath)).Returns(code);

            var scriptPart = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, scriptPart.GetCode());
        }
    }
}
