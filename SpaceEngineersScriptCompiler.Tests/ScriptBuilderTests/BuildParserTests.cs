using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Telerik.JustMock;

namespace SpaceEngineersScriptCompiler.Tests.ScriptBuilderTests
{
    [TestClass]
    public class BuildParserTests : ScriptBuilderTest
    {
        /// <summary>
        /// Data provider for unit testing parsing of Main method.
        /// </summary>
        /// <returns>Key is a specific code segment to test.  Value is the expected code result.
        /// </returns>
        private Dictionary<string, string> RemoveNamespaceAndClassesDataProvider()
        {
            return new Dictionary<string, string>
            {
                {
                    //simple example with namespace and class
                    @"
                    namespace MyApp.MyNamespace.SubNamespace
                    {
                        public class MyClass
                        {
                            public void Main() {var i = 0;}
                        }
                    }",
                    "public void Main() {var i = 0;}"
                },

                    // example with just a class
                {
                    "public class SomeOtherClass { public void Main() {Console.WriteLine(\"Hi!\");}}",
                    "public void Main() {Console.WriteLine(\"Hi!\");}"
                },

                {
                    // example adding using statements
                    @" using System;
                       using System.Collections.Generic;

                       namespace MyNamepsace { class YetAnotherClass { public void Main() {/*I'm an empty main method!*/} } }",
                    "public void Main() {/*I'm an empty main method!*/}"
                },

                    // example where Main is not the first method

                {
                    "using System; namespace AGreatNamespace { class ClassyClass { private void DoSomething() {} public void Main() {GetString();} } }",
                    "public void Main() {GetString();}"
                }

            };
        }

        [TestMethod]
        public void BuildShouldRemoveNamespaceAndClassDeclarationsFromCodeSurroundingMain()
        {
            (from testCase in RemoveNamespaceAndClassesDataProvider() select testCase).ToList().ForEach((keyValuePair) => {

                var code = keyValuePair.Key;
                var expectedCode = keyValuePair.Value;

                Mock.Arrange(() => FileAccessStub.ReadAllText(GoodFilePath)).Returns(code);

                var scriptPart = Builder.Build(GoodFilePath);

                Assert.AreEqual(expectedCode, scriptPart.GetCode());
            });
        }

    }
}
