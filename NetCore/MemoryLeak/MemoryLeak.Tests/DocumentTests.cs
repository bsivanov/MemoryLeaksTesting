using MemoryLeak;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

// Tweak 3
// Tweak 3
#if DEBUG
//[assembly: Debuggable(DebuggableAttribute.DebuggingModes.None)]
#endif

namespace MemoryLeakTest.Tests
{
    [TestClass]
    public class DocumentTests
    {
        [TestMethod]
        public void Clone_DoesNotLeakMemory()
        {
            // Arrange
            Document document = new Document();

            // Act
            Document documentClone = document.Clone();

            // Assert
            WeakReference weakReference = new WeakReference(documentClone);

            // Tweak 1 
            // Tweak 1 (passes only in Release);
            documentClone = null;

            // Holy Trinity to collect all garbage.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(weakReference.IsAlive);

            // Tweak 2
            // Keep the document template from garbage collecting, as the "leak" is present only during its lifetime.
            // Tweak 2 (passes only in Debug).
            GC.KeepAlive(document);
        }

        /* - I decided to use .NET Core for the example, but...
         * - in Debug, we need additional attribute which  modifies code generation for the JIT debugging.
         * - Apply Tweak 3 
         * - Run the tests with and without the fix in Debug - it works as expected
         * - As I am even greater hacker, I decided to use .NET Core 3.0 nightly build for the example.
         * - Where...
         * - The test passes with the fix only in Debug :D
         */
    }
}
