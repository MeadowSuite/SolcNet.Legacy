using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        }

        [TestMethod]
        public void Resolve_v0_4_21()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_21);
            StringAssert.Contains(path, "v0.4.21");
        }

        [TestMethod]
        public void Resolve_v0_4_22()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_22);
            StringAssert.Contains(path, "v0.4.22");
        }

        [TestMethod]
        public void Resolve_v0_4_23()
        {
            var path = LibPath.GetLibPath(SolcVersion.v0_4_23);
            StringAssert.Contains(path, "v0.4.23");
        }
    }
}
