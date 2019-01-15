using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// An extension to the generic List to check if two lists contain exactly the same elements.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="left">The list that this is called on.</param>
        /// <param name="right">The list to compare to.</param>
        /// <returns>True if both lists contain exactly the same elements, false otherwise.</returns>
        public static bool SetEquals<T>(this List<T> left, List<T> right)
        {
            // This code came from StackOverflow somewhere

            if (left.Count != right.Count)
                return false;

            Dictionary<T, int> dict = new Dictionary<T, int>();

            foreach (T member in left)
            {
                if (dict.ContainsKey(member) == false)
                    dict[member] = 1;
                else
                    dict[member]++;
            }

            foreach (T member in right)
            {
                if (dict.ContainsKey(member) == false)
                    return false;
                else
                    dict[member]--;
            }

            foreach (KeyValuePair<T, int> kvp in dict)
            {
                if (kvp.Value != 0)
                    return false;
            }

            return true;
        }
    }
}
