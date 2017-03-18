using NUnit.Framework;
using WayILookAtGames.MyNumberEngine;

namespace MyNumberSlagalicaEngineUnitTest
{
    [TestFixture]
    public class MyNumberItemTest
    {
        [Test]
        public void CalculateNew_Add_ShouldReturnNewItem()
        {
            MyNumberItem left = new MyNumberItem(null, null, 1, 10, MyNumberOperators.ADD, 2, 0);
            MyNumberItem right = new MyNumberItem(null, null, 1, 19, MyNumberOperators.ADD, 4, 1);
            MyNumberItem newItem = left.CalculateNew(right, MyNumberOperators.ADD, 2);
            Assert.AreEqual(29, newItem.currentValue);
            Assert.AreEqual(2, newItem.numbersUsed);
            Assert.AreEqual(2, newItem.startIndex);
            Assert.AreEqual(6, newItem.mask);
        }

        [Test]
        public void CalculateNew_Sub_ShouldReturnNewItem()
        {
            MyNumberItem left = new MyNumberItem(null, null, 1, 33, MyNumberOperators.ADD, 1, 0);
            MyNumberItem right = new MyNumberItem(null, null, 2, 19, MyNumberOperators.ADD, 12, 1);
            MyNumberItem newItem = left.CalculateNew(right, MyNumberOperators.SUB, 4);
            Assert.AreEqual(14, newItem.currentValue);
            Assert.AreEqual(3, newItem.numbersUsed);
            Assert.AreEqual(4, newItem.startIndex);
            Assert.AreEqual(13, newItem.mask);
        }

        [Test]
        public void CalculateNew_Mul_ShouldReturnNewItem()
        {
            MyNumberItem left = new MyNumberItem(null, null, 1, 5, MyNumberOperators.ADD, 2, 0);
            MyNumberItem right = new MyNumberItem(null, null, 3, 7, MyNumberOperators.ADD, 28, 1);
            MyNumberItem newItem = left.CalculateNew(right, MyNumberOperators.MULTIPLY, 14);
            Assert.AreEqual(35, newItem.currentValue);
            Assert.AreEqual(4, newItem.numbersUsed);
            Assert.AreEqual(14, newItem.startIndex);
            Assert.AreEqual(30, newItem.mask);
        }

        [Test]
        public void CalculateNew_Div_ShouldReturnNewItem()
        {
            MyNumberItem left = new MyNumberItem(null, null, 1, 21, MyNumberOperators.ADD, 2, 0);
            MyNumberItem right = new MyNumberItem(null, null, 3, 7, MyNumberOperators.ADD, 28, 1);
            MyNumberItem newItem = left.CalculateNew(right, MyNumberOperators.DIVIDE, 14);
            Assert.AreEqual(3, newItem.currentValue);
        }

        [Test]
        public void CalculateNew_DivNotDividable_ShouldReturnNull()
        {
            MyNumberItem left = new MyNumberItem(null, null, 1, 5, MyNumberOperators.ADD, 2, 0);
            MyNumberItem right = new MyNumberItem(null, null, 3, 7, MyNumberOperators.ADD, 28, 1);
            MyNumberItem newItem = left.CalculateNew(right, MyNumberOperators.DIVIDE, 14);
            Assert.IsNull(newItem);
            left = new MyNumberItem(null, null, 1, 13, MyNumberOperators.ADD, 2, 0);
            newItem = left.CalculateNew(right, MyNumberOperators.DIVIDE, 14);
            Assert.IsNull(newItem);
        }

        [Test]
        public void CalculateNew_SubResultLessThan1_ShouldReturnNull()
        {
            MyNumberItem left = new MyNumberItem(null, null, 1, 1, MyNumberOperators.ADD, 2, 0);
            MyNumberItem right = new MyNumberItem(null, null, 3, 7, MyNumberOperators.ADD, 28, 1);
            MyNumberItem newItem = left.CalculateNew(right, MyNumberOperators.SUB, 14);
            Assert.IsNull(newItem);
            left = new MyNumberItem(null, null, 1, 7, MyNumberOperators.ADD, 2, 0);
            newItem = left.CalculateNew(right, MyNumberOperators.SUB, 14);
            Assert.IsNull(newItem);
        }
    }
}
