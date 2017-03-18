using NUnit.Framework;
using WayILookAtGames.MyNumberEngine;

namespace MyNumberSlagalicaEngineUnitTest
{
    [TestFixture]
    public class MyNumberEngineTest
    {
        private MyNumberEngine engine = new MyNumberEngine();

        [TestCase(7, new int[] { 1, 6, 7, 5, 7, 15, 75 }, "7", 7)]
        [TestCase(889, new int[] { 3, 4, 4, 7, 15, 50 }, "50 * (15 + 3) - 7 - 4", 889)]
        [TestCase(467, new int[] { 4, 1, 8, 7, 15, 50 }, "4 + 8 + 7 * (50 + 15)", 467)]
        [TestCase(371, new int[] { 8, 3, 8, 4, 25, 75 }, "75 * (8 - 3) - 4", 371)]
        [TestCase(965, new int[] { 1, 1, 3, 2, 6, 25, 50 }, "(50 - 6) * (25 - 3) - 2 + 1", 965)]
        public void FindSolution_ShouldReturnResult(int number, int[] numbers, string expression, int result)
        {
            MyNumberItem myItem = engine.FindSolution(number, numbers);
            Assert.IsNotNull(myItem);
            Assert.AreEqual(result, myItem.currentValue);
            Assert.AreEqual(expression, myItem.ToString());
        }
    }
}
