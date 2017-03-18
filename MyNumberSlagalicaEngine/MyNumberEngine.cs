using System;
using System.Collections.Generic;

namespace WayILookAtGames.MyNumberEngine
{
    /// <summary>
    /// My Number base class
    /// </summary>
    public class MyNumberEngine
    {
        /// <summary>
        /// Tries to find best combination of numbers which (hopefully) give desired number
        /// </summary>
        /// <param name="number">Desired number</param>
        /// <param name="numbers">Numbers which combination should give as result desired number</param>
        /// <returns>MyNumberItem instance as result</returns>
        public MyNumberItem FindSolution(int number, int[] numbers)
        {
            // check if any of numbers alone is solution
            for (int i = 0; i < numbers.Length; ++i)
            {
                if (number == numbers[i])
                {
                    return new MyNumberItem(null, null, 1, numbers[i], MyNumberOperators.NONE, 1 << i, 0);
                }
            }

            // must keep all calculated numbers (number + mask)
            HashSet<MyNumberItem> calculatedNumbers = new HashSet<MyNumberItem>();
            List<MyNumberItem> itemsList = new List<MyNumberItem>(64536);
            calculatedNumbers.Add(null);
            for (int i = numbers.Length - 1; i >= 0; --i)
            {
                MyNumberItem item = new MyNumberItem(null, null, 1, numbers[i], MyNumberOperators.NONE, 1 << i, numbers.Length - i);
                calculatedNumbers.Add(item);
                itemsList.Add(item);
            }

            int countOfItemsInList;
            MyNumberItem solutionItem = itemsList[0];
            do
            {
                countOfItemsInList = itemsList.Count;
                for (int i = 0; i < countOfItemsInList; ++i)
                {
                    MyNumberItem leftItem = itemsList[i];

                    int endForJ = itemsList.Count;
                    // before Ith element just calculate divide
                    for (int j = leftItem.startIndex; j < endForJ; ++j)
                    {
                        MyNumberItem rightItem = itemsList[j];
                        // check if left and right expression can be combined
                        if ((leftItem.mask & rightItem.mask) != 0)
                        {
                            continue;
                        }

                        MyNumberItem newItem;
                        // left (+, -, *, /) right                       
                        for (int k = 0; k < 4; ++k)
                        {
                            newItem = leftItem.CalculateNew(rightItem, (MyNumberOperators)k, itemsList.Count + 1);
                            if (!calculatedNumbers.Contains(newItem)) // new item is actually added
                            {
                                calculatedNumbers.Add(newItem);
                                itemsList.Add(newItem);
                                int diff = Math.Abs(solutionItem.currentValue - number) -
                                      Math.Abs(newItem.currentValue - number);
                                if (diff > 0 || (diff == 0 && newItem.numbersUsed < solutionItem.numbersUsed))
                                {
                                    solutionItem = newItem;
                                }
                            }
                        }

                        // sub and division are not comutative 
                        // right / left
                        for (int k = 2; k < 4; ++k)
                        {
                            newItem = rightItem.CalculateNew(leftItem, (MyNumberOperators)k, itemsList.Count + 1);
                            if (!calculatedNumbers.Contains(newItem)) // new item is actually added
                            {
                                calculatedNumbers.Add(newItem);
                                itemsList.Add(newItem);
                                int diff = Math.Abs(solutionItem.currentValue - number) -
                                      Math.Abs(newItem.currentValue - number);
                                if (diff > 0 || (diff == 0 && newItem.numbersUsed < solutionItem.numbersUsed))
                                {
                                    solutionItem = newItem;
                                }
                            }
                        }
                    }

                    // next start for this item is at the current end of items list
                    leftItem.startIndex = itemsList.Count;
                }

            }
            // if some new items are added to list - continue with processing
            while (itemsList.Count > countOfItemsInList);

            return solutionItem;
        }
    }
}