using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library.Model;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    [TestClass]
    public class ObjectParsingTests : AbstractScriptBuilderTest
    {
        string GoodFilePath2 = @"C:\Users\MyDocuments\MyOtherReallySuperAwesomeCodeFile.txt";

        [TestMethod]
        public void BuildShouldBeAbleToIncludeOtherReferencedObjectsInSameNamespace()
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

        [TestMethod]
        public void BuildShouldBeAbleToIgnoreNonReferencedObjectsInSameNamespace()
        {
            var code1 = @"namespace MyNamespace {
    class MyClass
    {
        public override void Main()
        {
            var i = 1 + 2;
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
            var i = 1 + 2;
        }";

            var result = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, result);
        }

        [TestMethod]
        public void BuildShouldBeAbleToLoadDependenciesFromOtherNamespaces()
        {
            var code1 = @"using MyNamespace.MySubNamespace;

namespace MyNamespace {
    class MyClass
    {
        public override void Main()
        {
            var obj = new MyLoadedObject();
        }
    }
}
";
            var code2 = @"namespace MyNamespace.MySubNamespace {
    class MyLoadedObject
    {
    }
}";

            AddFileMetadata(GoodFilePath, code1);
            AddFileMetadata(GoodFilePath2, code2);

            var expectedCode = @"void Main()
        {
            var obj = new MyLoadedObject();
        }

class MyLoadedObject
    {
    }";

            var result = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, result);
        }

        [TestMethod]
        public void BuildShouldBeAbleToResolveDependenciesInBothTheCurrentAndRemoteNamespaces()
        {
            throw new System.NotImplementedException();
        }

//        [TestMethod]
//        public void BuildShouldBeAbleToIgnoreReservedNamespaces()
//        {
//            var code = @"
//using Sytem.MyCustomNamespace;
//
//namespace MySpaceEngineers
//{
//    class MyDoorOpener
//    {
//        void Main()
//        {
//            var object = new MyCustomObject();
//        }
//    }
//}
//";
//            var code2 = @"
//namespace Sytem.MyCustomNamespace
//{
//    public class MyCustomObject()
//{}
//}
//";
//            AddFileMetadata(GoodFilePath, code);
//            AddFileMetadata(GoodFilePath2, code2);

//            var expectedCode = @"void Main()
//        {
//            var object = new MyCustomObject();
//        }";

//            var result = Builder.Build(GoodFilePath);

//            Assert.AreEqual(expectedCode, result);
//        }

    }
}
