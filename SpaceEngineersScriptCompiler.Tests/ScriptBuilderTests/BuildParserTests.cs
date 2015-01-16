using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceEngineersScriptCompiler.Library.File;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    [TestClass]
    public class BuildParserTests : ScriptBuilderTest
    {
        private void RunCodeParseTestAndAssert(string code, string expectedCode)
        {
            AddFileMetadata(GoodFilePath, code);

            var scriptPart = Builder.Build(GoodFilePath);

            Assert.AreEqual(expectedCode, scriptPart.GetCode());
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
            var code = "using System; namespace AGreatNamespace { public static class Main { private void DoSomething() {} public void Main() {var i = 1 + 2;} } }";

            var expectedCode = "void Main() {var i = 1 + 2;}";

            RunCodeParseTestAndAssert(code, expectedCode);
        }

        
        public void BuildShouldReturnMainPlusAReferencedFunctionFromTheSameFile()
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

            RunCodeParseTestAndAssert(code, expectedCode);

        }

        // BuildShouldIgnoreCallsToGridTerminalSystem
        // 

    }
}
