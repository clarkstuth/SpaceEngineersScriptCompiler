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
        public void BuildShouldReturnJustMainRemovingUsingNamespaceClassAndLeadingMethod()
        {
            var code = "using System; namespace AGreatNamespace { public static class DoSomething { private void NotMain() {} public void Main() {var i = 1 + 2;} } }";

            var expectedCode = "void Main() {var i = 1 + 2;}";

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

        
        public void BuildShouldNotIncludeExtraMainFunctionsIfNotCalled()
        {
            var code = @"namepsace MyNamespace {
                            class MyClass {
                                private void Main() {}
                                private void NotMain() {}
                            }
                         }";

            
        }

        // BuildShouldIgnoreCallsToGridTerminalSystem
        

    }
}
