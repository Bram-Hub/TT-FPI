namespace TruthTree
{
    /// <summary>
    /// Really simple generic Pair class.
    /// </summary>
    /// <typeparam name="T">First type.</typeparam>
    /// <typeparam name="U">Second type.</typeparam>
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };
}
