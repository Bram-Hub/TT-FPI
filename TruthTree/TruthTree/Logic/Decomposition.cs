using System;
using System.Collections.Generic;
using ExtensionMethods;

namespace TruthTree.Logic
{
    /// <summary>
    /// A class representing the decomposition of a Sentence.
    /// </summary>
    public class Decomposition : IEquatable<Decomposition>
    {
        /// <summary>
        /// The left side of the decomposition.
        /// </summary>
        public List<Sentence> left;

        /// <summary>
        /// The right side of the decomposition.
        /// </summary>
        public List<Sentence> right;

        /// <summary>
        /// Create an empty decomposition.
        /// </summary>
        public Decomposition()
        {
            left = new List<Sentence>();
            right = new List<Sentence>();
        }

        /// <summary>
        /// Create a decomposition with the given left and right sides.
        /// </summary>
        /// <param name="l">The left side of the decomposition.</param>
        /// <param name="r">The right side of the decomposition.</param>
        public Decomposition(List<Sentence> l, List<Sentence> r)
        {
            if (l == null) { left = new List<Sentence>(); }
            else { left = l; }

            if (r == null) { right = new List<Sentence>(); }
            else { right = r; }

            // The left should always have something if this is a real
            // decomposition
            if (left.Count < 1) { left.AddRange(right); right.Clear(); }
        }

        /// <summary>
        /// Returns the hash code for this decomposition.
        /// </summary>
        /// <returns>The hash code for this decomposition.</returns>
        public override int GetHashCode()
        {
            return left.GetHashCode() + right.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to this Decomposition.
        /// </summary>
        /// <param name="obj">Object to test for equality.</param>
        /// <returns>True if ths given object is the same, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) { return base.Equals(obj); }
            if (obj is Decomposition) { return Equals((Decomposition)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Determines whether the specified Decomposition is equal to this Decomposition.
        /// </summary>
        /// <param name="o">Decomposition to test for equality.</param>
        /// <returns>True if the given Decomposition is the same, false otherwise.</returns>
        public bool Equals(Decomposition o)
        {
            if (o == null) { return false; }

            return (left.SetEquals(o.left) && right.SetEquals(o.right)) ||
                (left.SetEquals(o.right) && right.SetEquals(o.left));
        }

        /// <summary>
        /// Check if this Decomposition has a left side.
        /// </summary>
        /// <returns>True if the left side exists, false otherwise.</returns>
        public bool hasLeft() { return left != null && left.Count != 0; }

        /// <summary>
        /// Check if this Decomposition has a right side.
        /// </summary>
        /// <returns>True if the right side exists, false otherwise.</returns>
        public bool hasRight() { return right != null && right.Count != 0; }

        /// <summary>
        /// Gets a string representation of this Decomposition.
        /// </summary>
        /// <returns>A string representing this Decomposition.</returns>
        public override string ToString()
        {
            string s = "Left:";
            foreach (Sentence a in left) { s += "\n" + a.ToString(); }

            s += "\nRight:";
            foreach (Sentence a in right) { s += "\n" + a.ToString(); }

            return s;
        }
    }
}
