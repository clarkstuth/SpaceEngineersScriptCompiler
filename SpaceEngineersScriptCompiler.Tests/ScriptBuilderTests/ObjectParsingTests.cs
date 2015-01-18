using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library.Model;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    [TestClass]
    public class ObjectParsingTests : AbstractScriptBuilderTest
    {
        string GoodFilePath2 = @"C:\Users\MyDocuments\MyOtherReallySuperAwesomeCodeFile.txt";

        [TestMethod]
        public void BuildShouldBeAbleToIncludeOtherReferencedObjects()
        {
            var code1 = @"namespace MyNamespace {
    class MyClass
    {
        public override void Main()
        {
            var someOtherObject = new MyOtherObject();
        }
    }
}";
            var code2 = @"namespace MyNamespace {
    class MyOtherObject
    {
    }
}";
            AddFileMetadata(GoodFilePath, code1);
            AddFileMetadata(GoodFilePath2, code2);

            var expectedCode = @"void Main()
        {
            var someOtherObject = new MyOtherObject();
        }

class MyOtherObject
    {
    }";

            var result = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, result);
        }
    }
}
