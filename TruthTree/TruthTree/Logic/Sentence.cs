using System;

using TruthTree.Input;

namespace TruthTree.Logic
{
    /// <summary>
    /// An enumeration of the different types of sentences.
    /// </summary>
    public enum SentenceType
    {
        ATOM,
        FALSE,
        NOT,
        AND,
        OR,
        IF,
        IFF,
        OTHER
    }

    /// <summary>
    /// A class representing a sentence in propositional logic.
    /// </summary>
    public class Sentence : IEquatable<Sentence>
    {
        /// <summary>
        /// The type of this Sentence (if, and, not, etc.).
        /// </summary>
        public SentenceType type;

        /// <summary>
        /// The only or left part of the Sentence.
        /// Should not be null
        /// </summary>
        public Sentence left;

        /// <summary>
        /// The right part of the Sentence.
        /// May be null
        /// </summary>
        public Sentence right;

        /// <summary>
        /// If the SentenceType is ATOM, this is the atom it represents.
        /// </summary>
        public char atom;

        /// <summary>
        /// Creates an empty Sentence.
        /// </summary>
        public Sentence()
        {
            this.type = SentenceType.OTHER;
            this.left = null;
            this.right = null;
            this.atom = '\0';
        }

        /// <summary>
        /// Creates a Sentence of the given SentenceType.
        /// </summary>
        /// <param name="ty">The type of Sentence to create.</param>
        public Sentence(SentenceType ty)
        {
            this.type = ty;
            this.left = null;
            this.right = null;
            this.atom = '\0';
        }

        /// <summary>
        /// Creates an atomic Sentence.
        /// </summary>
        /// <param name="ty">The type of Sentence to create.</param>
        /// <param name="a">The atom to represent with this Sentence.</param>
        public Sentence(SentenceType ty, char a)
        {
            this.type = ty;
            this.left = null;
            this.right = null;
            this.atom = a;
        }

        /// <summary>
        /// Creates a negation Sentence.
        /// </summary>
        /// <param name="ty">The type of Sentence to create.</param>
        /// <param name="l">The sentence to negate with this Sentence.</param>
        public Sentence(SentenceType ty, Sentence l)
        {
            this.type = ty;
            this.left = l;
            this.right = null;
            this.atom = '\0';
        }

        /// <summary>
        /// Creates a sentence of the given SentenceType with the given left and right sides.
        /// </summary>
        /// <param name="ty">The type of Sentence to create.</param>
        /// <param name="l">The left side of a Sentence.</param>
        /// <param name="r">THe right side of a Sentence.</param>
        public Sentence(SentenceType ty, Sentence l, Sentence r)
        {
            this.type = ty;
            this.left = l;
            this.right = r;
            this.atom = '\0';
        }

        /// <summary>
        /// Negates this Sentence. If the Sentence is a negation this will return the inner
        /// Sentence.
        /// </summary>
        /// <returns>The negation of this Sentence.</returns>
        public Sentence negation()
        {
            if (type == SentenceType.OTHER) { return null; }
            if (type == SentenceType.NOT) { return left; }

            return new Sentence(SentenceType.NOT, this);
        }

        /// <summary>
        /// Returns the hash code for this Sentence.
        /// </summary>
        /// <returns>The hash code for this Sentence.</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to this Sentence.
        /// </summary>
        /// <param name="obj">Object to test for equality.</param>
        /// <returns>True if the given object is the same, false otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) { return base.Equals(obj); }
            if (obj is Sentence) { return Equals((Sentence)obj); }
            return base.Equals(obj);
        }

        /// <summary>
        /// Determines whether the specified Sentence is equal to this Sentence.
        /// </summary>
        /// <param name="other">Sentence to test for equality.</param>
        /// <returns>True if the given Sentence is the same, false otherwise.</returns>
        public bool Equals(Sentence other)
        {
            if (other == null) { return false; }
            if (type == SentenceType.OTHER || other.type == SentenceType.OTHER) { return false; }
            if (type != other.type) { return false; }

            bool ret = false;
            switch (type)
            {
                case SentenceType.AND:
                case SentenceType.OR:
                case SentenceType.IF:
                case SentenceType.IFF:
                    ret = left.Equals(other.left) && right.Equals(other.right);
                    break;
                case SentenceType.NOT:
                    ret = left.Equals(other.left);
                    break;
                case SentenceType.ATOM:
                    ret = atom.Equals(other.atom);
                    break;
                case SentenceType.FALSE:
                    ret = other.type == SentenceType.FALSE;
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Determine if the specified Sentence is equivalent to this Sentence.
        /// Currently only checks for pure equality.
        /// </summary>
        /// <param name="other">Sentence to test for equivalence.</param>
        /// <returns>True if the given Sentence is equivalent, false otherwise.</returns>
        public bool isEquivalent(Sentence other)
        {
            return (this.Equals(other));
        }

        /// <summary>
        /// Check to see if this Sentence is decomposable.
        /// </summary>
        /// <returns>True if this Sentence is decomposable, false otherwise.</returns>
        public bool isDecomposable()
        {
            if (type == SentenceType.ATOM || type == SentenceType.FALSE || type == SentenceType.OTHER) { return false; }
            if (type == SentenceType.NOT) { return left.type == SentenceType.NOT || left.isDecomposable(); }
            return true;
        }

        /// <summary>
        /// Decompose this Sentence based on the rules of Truth Trees.
        /// </summary>
        /// <returns>The decomposition of this Sentence if it is decomposable, null otherwise.</returns>
        public Decomposition decompose()
        {
            if (!isDecomposable()) { return null; }

            Decomposition ret = new Decomposition();

            if (type == SentenceType.AND)
            {
                ret.left.Add(left);
                ret.left.Add(right);
            }
            else if (type == SentenceType.OR)
            {
                ret.left.Add(left);
                ret.right.Add(right);
            }
            else if (type == SentenceType.IF)
            {
                ret.left.Add(left.negation());
                ret.right.Add(right);
            }
            else if (type == SentenceType.IFF)
            {
                ret.left.Add(left);
                ret.left.Add(right);
                ret.right.Add(left.negation());
                ret.right.Add(left.negation());
            }
            else if (type == SentenceType.NOT)
            {
                if (left.type == SentenceType.AND)
                {
                    ret.left.Add(left.left.negation());
                    ret.right.Add(left.right.negation());
                }
                else if (left.type == SentenceType.OR)
                {
                    ret.left.Add(left.left.negation());
                    ret.left.Add(left.right.negation());
                }
                else if (left.type == SentenceType.IF)
                {
                    ret.left.Add(left.left);
                    ret.left.Add(left.right.negation());
                }
                else if (left.type == SentenceType.IFF)
                {
                    ret.left.Add(left.left);
                    ret.left.Add(left.right.negation());
                    ret.right.Add(left.left.negation());
                    ret.right.Add(left.right);
                }
                else if (left.type == SentenceType.NOT)
                {
                    ret.left.Add(left.left);
                }
            }

            return ret;
        }

        /// <summary>
        /// Gets a string representation of this Sentence.
        /// </summary>
        /// <returns>A string representing this sentence.</returns>
        public override string ToString()
        {
            string ret = "";

            switch (type)
            {
                case SentenceType.AND:
                    ret = "(" + left.ToString() + " ʌ " + right.ToString() + ")";
                    break;
                case SentenceType.OR:
                    ret = "(" + left.ToString() + " v " + right.ToString() + ")";
                    break;
                case SentenceType.IF:
                    ret = "(" + left.ToString() + " → " + right.ToString() + ")";
                    break;
                case SentenceType.IFF:
                    ret = "(" + left.ToString() + " ↔ " + right.ToString() + ")";
                    break;
                case SentenceType.NOT:
                    ret = "¬" + left.ToString();
                    break;
                case SentenceType.ATOM:
                    ret = "" + atom;
                    break;
                case SentenceType.FALSE:
                    ret = "ϟ";
                    break;
            }

            return ret;
        }

        /// <summary>
        /// Uses the Parser to parse a Sentence from a string.
        /// </summary>
        /// <param name="str">A string to parse.</param>
        /// <returns>The result from the Parser parsing the given string.</returns>
        public static Sentence parseFromString(string str)
        {
            return Parser.parseString(str);
        }
    }
}
