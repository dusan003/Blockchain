using Common;

namespace Testovi
{
    public class BlockValidationTest
    {
        private Client client = new Client();
        private Block block = null;

        [SetUp]
        public void Setup()
        {
            block = new Block(client);
        }



        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        [TestCase(true)]
        [TestCase(false)]
        [TestCase(false)]
        public void ValidationTest(bool param)
        {
            int solution;
            block.Solve( out solution);
            var vrednost = block.ValidateBlock(solution);

            Assert.AreEqual(param,vrednost);

            Assert.Pass();
        }
    }
}