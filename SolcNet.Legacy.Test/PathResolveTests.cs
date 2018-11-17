using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SolcNet.Legacy.Test
{
    [TestClass]
    public class PathResolveTests
    {
        [TestMethod]
        public void Resolve_v0_4_20()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_20);
            StringAssert.Contains(path, "v0.4.20");

            path = LibPath.GetLibPath(Version.Parse("0.4.20"));
            StringAssert.Contains(path, "v0.4.20");
        }

        [TestMethod]
        public void Resolve_v0_4_21()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_21);
            StringAssert.Contains(path, "v0.4.21");

            path = LibPath.GetLibPath(Version.Parse("0.4.21"));
            StringAssert.Contains(path, "v0.4.21");
        }

        [TestMethod]
        public void Resolve_v0_4_22()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_22);
            StringAssert.Contains(path, "v0.4.22");

            path = LibPath.GetLibPath(Version.Parse("0.4.22"));
            StringAssert.Contains(path, "v0.4.22");
        }

        [TestMethod]
        public void Resolve_v0_4_23()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_23);
            StringAssert.Contains(path, "v0.4.23");

            path = LibPath.GetLibPath(Version.Parse("0.4.23"));
            StringAssert.Contains(path, "v0.4.23");
        }

        [TestMethod]
        public void Resolve_v0_4_24()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_24);
            StringAssert.Contains(path, "v0.4.24");

            path = LibPath.GetLibPath(Version.Parse("0.4.24"));
            StringAssert.Contains(path, "v0.4.24");
        }

        [TestMethod]
        public void Resolve_v0_4_25()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_25);
            StringAssert.Contains(path, "v0.4.25");

            path = LibPath.GetLibPath(Version.Parse("0.4.25"));
            StringAssert.Contains(path, "v0.4.25");
        }
    }
}
