namespace WayILookAtGames.MyNumberEngine
{
    /// <summary>
    /// MyNumber item class. It holds refrences to its left and right childs (tree structure)
    /// Result of every solution is instance of this class
    /// </summary>
    public class MyNumberItem
    {
        private const int MAX_NUMBERS = 16;
        private static char[] operatorChars = new char[]
        {
                '+', '*', '-', '/'
        };

        private MyNumberItem left;
        private MyNumberItem right;
        private MyNumberOperators operand;

        public int mask;

        public int currentValue;
        public int numbersUsed;
        public int startIndex;

        public MyNumberItem(MyNumberItem left, MyNumberItem right, int numbersUsed,
            int currentValue, MyNumberOperators operand, int mask, int startIndex)
        {
            this.left = left;
            this.right = right;
            this.currentValue = currentValue;
            this.operand = operand;
            this.mask = mask;
            this.numbersUsed = numbersUsed;
            this.startIndex = startIndex;
        }

        public MyNumberItem CalculateNew(MyNumberItem right, MyNumberOperators op, int newStartIndex)
        {
            int newValue;
            switch (op)
            {
                case MyNumberOperators.ADD:
                    newValue = this.currentValue + right.currentValue;
                    break;
                case MyNumberOperators.SUB:
                    newValue = this.currentValue - right.currentValue;
                    if (newValue <= 0) // x - (y - z) is same as x + (z - y) and zero does not make any sense
                    {
                        return null;
                    }
                    break;
                case MyNumberOperators.MULTIPLY:
                    newValue = this.currentValue * right.currentValue;
                    break;
                case MyNumberOperators.DIVIDE:
                    if (right.currentValue == 0)
                    {
                        return null;
                    }

                    newValue = currentValue / right.currentValue;

                    // not fully dividable
                    if (newValue * right.currentValue != currentValue)
                    {
                        return null;
                    }
                    break;
                default:
                    return null;
            }

            MyNumberItem item = new MyNumberItem(this, right, numbersUsed + right.numbersUsed,
                newValue, op, mask | right.mask, newStartIndex);
            return item;
        }

        public override int GetHashCode()
        {
            return mask + currentValue * (1 << MAX_NUMBERS);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MyNumberItem))
            {
                return false;
            }
            MyNumberItem other = (MyNumberItem)obj;
            return other.currentValue == currentValue && other.mask == mask;
        }

        private bool HasSubOperand()
        {
            if (operand == MyNumberOperators.NONE)
            {
                return false;
            }

            return operand == MyNumberOperators.SUB || left.HasSubOperand() || right.HasSubOperand();
        }

        public override string ToString()
        {
            // one of base numbers
            if (operand == MyNumberOperators.NONE)
            {
                return currentValue.ToString();
            }

            char c = operatorChars[(short)operand];
            if (operand == MyNumberOperators.SUB && right.HasSubOperand())
            {
                return string.Format("{0} {1} ({2})", left.ToString(), c, right.ToString());
            }
            else if (operand == MyNumberOperators.MULTIPLY || operand == MyNumberOperators.DIVIDE)
            {
                if (left.operand == MyNumberOperators.ADD || left.operand == MyNumberOperators.SUB)
                {
                    if (right.operand == MyNumberOperators.ADD || right.operand == MyNumberOperators.SUB)
                    {
                        return string.Format("({0}) {1} ({2})", left.ToString(), c, right.ToString());
                    }
                    else
                    {
                        return string.Format("({0}) {1} {2}", left.ToString(), c, right.ToString());
                    }
                }
                else if (right.operand == MyNumberOperators.ADD || right.operand == MyNumberOperators.SUB)
                {
                    return string.Format("{0} {1} ({2})", left.ToString(), c, right.ToString());
                }
            }

            return string.Format("{0} {1} {2}", left.ToString(), c, right.ToString());
        }
    }
}
