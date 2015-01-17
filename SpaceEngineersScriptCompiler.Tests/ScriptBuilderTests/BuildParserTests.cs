using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    [TestClass]
    public class BuildParserTests : AbstractScriptBuilderTest
    {
        private void RunCodeParseTestAndAssert(string code, string expectedCode)
        {
            AddFileMetadata(GoodFilePath, code);

            var result = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, result);
        }

        [TestMethod]
        public void BuildShouldReturnJustMainRemovingNamespaceAndClass()
        {
            var code = @"
                    namespace MyApp.MyNamespace.SubNamespace
                    {
                        public class MyClass
                        {
                            public void Main() {var i = 0;}
                        }
                    }";

            var expectedCode = "void Main() {var i = 0;}";

            RunCodeParseTestAndAssert(code, expectedCode);
        }

        [TestMethod]
        public void BuildShouldReturnJustMainRemovingClass()
        {
            var code = "public class SomeOtherClass { public void Main() {Console.WriteLine(\"Hi!\");}}";

            var expectedCode = "void Main() {Console.WriteLine(\"Hi!\");}";

            RunCodeParseTestAndAssert(code, expectedCode);
        }

        [TestMethod]
        public void BuildShouldReturnJustMainRemovingUsingNamespaceAndClass()
        {
            var code = @" using System;
                       using System.Collections.Generic;

                       namespace MyNamepsace { class Main { public void Main() {/*I'm an empty main method!*/} } }";

            var expectedCode = "void Main() {/*I'm an empty main method!*/}";

            RunCodeParseTestAndAssert(code, expectedCode);
        }

        [TestMethod]
        public void BuildShouldReturnMainPlusAMethodFromTheSameFile()
        {
            var code = @"
            using System;
            
            namespace MyNamespace {
                class MyOpenDoorScript {
                    public void Main() {
                        var i = 3 / 2 + 7;
                        MyOtherMethod(i);
                    }

                    private void MyOtherMethod(int i) {
                        i = 7;
                    }
                }
            }";

            var expectedCode = @"void Main() {
                        var i = 3 / 2 + 7;
                        MyOtherMethod(i);
                    }

void MyOtherMethod(int i) {
                        i = 7;
                    }";

            RunCodeParseTestAndAssert(code.Trim(), expectedCode.Trim());

        }

        [TestMethod]
        public void BuildShouldReturnMainPlusMultipleMethodsFromTheSameFile()
        {
            var code = @"using Sandbox.ModAPI.InGame;

namepsace MyNamespace {
    class MyClass {
        private void Main() {
            // this main doesn't do anything
        }
        private void SecondMethod(int i) {
            int j = i;
        }
        private int ThirdMethod() {
            return ""something"";
        }
    }
}";

            var expectedCode = @"void Main() {
            // this main doesn't do anything
        }

void SecondMethod(int i) {
            int j = i;
        }

int ThirdMethod() {
            return ""something"";
        }";

            RunCodeParseTestAndAssert(code, expectedCode);
        }

        // BuildShouldIgnoreCallsToGridTerminalSystem
        

    }
}
