using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MemoryLeak.Tests
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
            //documentClone = null;

            // Holy Trinity to collect all garbage.
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Assert.IsFalse(weakReference.IsAlive);

            // Tweak 2
            // Keep the "document" from garbage collecting, as the "leak" is present only during its lifetime.
            // Tweak 2 (passes only in Debug).
            //GC.KeepAlive(document);
        }

        /* - Ensure debug
         * - Show class
         * - Show test
         * - Run test
         * - Fix class
         * - Run test - it fails - JIT holds additional references to the end of the method in when
         * the code is compiled with /debug switch (in Debug)
         * - Apply tweak 1 
         * - Run the test - it passes!
         * - Wow!, we are in a dangerous game. Let's try to run the test in Release, 
         * with and without the fix applied.
         * - Wow, the test always passes!
         * - Apply tweak 2.
         * - Now tests are fine in Debug and Release, failing when the fix is not present.
         * - The story in .NET Core
         */
    }
}
