using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TruthTree.Logic
{
    /// <summary>
    /// An enumeration of the different states that a truth tree can have
    /// </summary>
    public enum NodeState
    {
        INCOMPLETE,
        CLOSED,
        OPEN,
        ERROR
    }

    /// <summary>
    /// The one class I really do not want to comment.
    /// </summary>
    public class TreeNode
    {
        private static Font font = new Font("Courier New", 9);
        private static int height = font.Height;
        private static Color done = Color.Blue;
        private static Color problem = Color.Red;

        public List<Pair<Sentence, bool>> sentences;
        public TreeNode left;
        public TreeNode right;
        public TreeNode parent;
        public NodeState state;

        private bool selected;

        public Size fullbox;
        private Rectangle selfbox;
        private Rectangle leftbox;
        private Rectangle rightbox;

        public TreeNode()
        {
            sentences = new List<Pair<Sentence, bool>>();
            parent = null;
            left = null;
            right = null;
            state = NodeState.INCOMPLETE;
            selected = false;
        }

        public TreeNode(List<Sentence> sen) : this()
        {
            foreach (Sentence s in sen)
            {
                Pair<Sentence, bool> p = new Pair<Sentence, bool>();
                p.First = s;
                p.Second = s.isDecomposable();
                sentences.Add(p);
            }
        }

        public void setDecomposition(Decomposition decomp)
        {
            if (decomp == null) { return; }

            if (decomp.hasLeft())
            {
                left = new TreeNode(decomp.left);
                left.parent = this;

                if (decomp.hasRight())
                {
                    right = new TreeNode(decomp.right);
                    right.parent = this;
                }
            }
        }

        public bool isClosed() { return state == NodeState.CLOSED; }

        public bool isOpen() { return state == NodeState.OPEN; }

        public bool isIncomplete() { return state == NodeState.INCOMPLETE; }
        
        public TreeNode checkClickForSelection(int x, int y)
        {
            selected = false;

            if (selfbox.Contains(x, y))
            {
                selected = true;

                if (left != null) { left.checkClickForSelection(-1, -1); }
                if (right != null) { right.checkClickForSelection(-1, -1); }

                return this;
            }

            TreeNode l = null;
            TreeNode r = null;
            if (left != null)
            {
                l = left.checkClickForSelection(x - leftbox.Left, y - leftbox.Top);
            }
            if (right != null)
            {
                r = right.checkClickForSelection(x - rightbox.Left, y - rightbox.Top);
            }

            if (l != null) { return l; }
            if (r != null) { return r; }

            return null;
        }

        public TreeNode getSelection()
        {
            if (selected) { return this; }

            if (left != null)
            {
                TreeNode l = left.getSelection();
                if (l != null) { return l; }
            }

            if (right != null)
            {
                TreeNode r = right.getSelection();
                if (r != null) { return r; }
            }

            return null;
        }

        public List<Sentence> getUnsatisfied(bool all)
        {
            List<Sentence> uns = new List<Sentence>();

            if (all && parent != null)
            {
                uns.AddRange(parent.getUnsatisfied(all));
            }

            foreach (Pair<Sentence, bool> p in sentences)
            {
                if (p.Second) { uns.Add(p.First); }
            }

            return uns;
        }

        public List<Sentence> getSentences(bool all)
        {
            List<Sentence> sen = new List<Sentence>();

            if (all && parent != null)
            {
                sen.AddRange(parent.getSentences(all));
            }

            foreach (Pair<Sentence, bool> p in sentences)
            {
                sen.Add(p.First);
            }

            return sen;
        }

        public bool decompose()
        {
            // Don't do anything if I am already open or closed
            if (state != NodeState.INCOMPLETE) { return false; }

            // If I have children, decompose them
            if (left != null)
            {
                if (right != null) { return left.decompose() || right.decompose(); }
                else { return left.decompose(); }
            }

            // See if we can decompose any of the unsatisfied sentences
            List<Sentence> unsat = getUnsatisfied(true);
            foreach (Sentence s in unsat)
            {
                if (!s.isDecomposable()) { continue; }
                    
                Decomposition deco = s.decompose();
                if (deco == null) { continue; }

                // If this is a valid decomposition then use it
                if (isValidDecomposition(deco))
                {
                    setDecomposition(deco);
                    return true;
                }
            }

            return false;
        }

        private void leafCheckOpenClosed()
        {
            if (!isIncomplete()) { return; }

            List<Sentence> allsentences = getSentences(true);
            for (int a = 0; a < allsentences.Count; a++)
            {
                // If this is the contradiction then I am closed
                if (allsentences[a].type == SentenceType.FALSE) { state = NodeState.CLOSED; return; }

                // Is this sentence the negation of any other sentence?
                for (int b = a + 1; b < allsentences.Count; b++)
                {
                    if (allsentences[a].type == SentenceType.NOT)
                    {
                        if (allsentences[a].left.Equals(allsentences[b])) { state = NodeState.CLOSED; return; }
                    }
                    if (allsentences[b].type == SentenceType.NOT)
                    {
                        if (allsentences[b].left.Equals(allsentences[a])) { state = NodeState.CLOSED; return; }
                    }
                }
            }

            // If there are any unsatisfied sentences then this is not complete
            if (getUnsatisfied(true).Count != 0) { state = NodeState.INCOMPLETE; return; }

            // This means we had no contradictions and there are no unsatisfied sentences
            state = NodeState.OPEN;
        }

        public bool treeContains(Sentence s, bool recur)
        {
            if (sentences.Exists(p => (p.First.Equals(s)))) { return true; }

            if (!recur) { return false; }

            if (left != null)
            {
                if (left.treeContains(s, recur)) { return true; }
            }

            if (right != null)
            {
                if (right.treeContains(s, recur)) { return true; }
            }

            return false;
        }

        public bool treeContains(List<Sentence> sen, bool recur)
        {
            if (sen.Count == 0) { return false; } // TrueForAll is true when the list is empty
            return sen.TrueForAll(s => treeContains(s, recur));
        }

        private bool upperTreeContainsDecomposition(Decomposition decomp)
        {
            if (decomp == null) { return true; }

            TreeNode cur = parent;
            while (cur != null)
            {
                if (cur.childrenAreDecomposition(decomp)) { return true; }

                cur = cur.parent;
            }

            return false;
        }

        private bool childrenAreDecomposition(Decomposition decomp)
        {
            if (decomp == null) { return true; } // Not sure what I want to return here, neither true nor false make sense

            if (left != null)
            {
                if (right != null && decomp.hasLeft() && decomp.hasRight())
                {
                    if ((left.treeContains(decomp.left, false) && right.treeContains(decomp.right, false)) ||
                        (left.treeContains(decomp.right, false) && right.treeContains(decomp.left, false)))
                    {
                        return true;
                    }
                }
                else if (decomp.hasLeft() || decomp.hasRight())
                {
                    if (left.treeContains(decomp.left, false) || left.treeContains(decomp.right, false))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool treeContainsDecomposition(Decomposition decomp)
        {
            if (decomp == null) { return true; }

            // If I am closed then it doesn't matter (I technically include it)
            if (!isIncomplete()) { return true; }

            // Are my children this decomposition?
            if (childrenAreDecomposition(decomp)) { return true; }

            // Otherwise, my children must contain the decomposition
            if (right != null)
            {
                return left.treeContainsDecomposition(decomp) && right.treeContainsDecomposition(decomp);
            }
            else if (left != null)
            {
                return left.treeContainsDecomposition(decomp);
            }

            return false;
        }

        public void checkSentenceSatisfaction()
        {
            // See if the tree below me has the decomposition of any of my sentences
            foreach (Pair<Sentence, bool> p in sentences)
            {
                if (!p.Second) { continue; }

                Decomposition deco = p.First.decompose();
                if (deco == null) { continue; }

                if (treeContainsDecomposition(deco)) { p.Second = false; }
            }

            // Check my children too
            if (left != null) { left.checkSentenceSatisfaction(); }
            if (right != null) { right.checkSentenceSatisfaction(); }
        }

        public bool isValidDecomposition(Decomposition decomp)
        {
            if (decomp == null) { return false; }

            // Does this decomposition already exist above me?
            if (upperTreeContainsDecomposition(decomp)) { return false; }
            
            // Do any of the unsatisfied sentences decompose into this?
            List<Sentence> unsat = getUnsatisfied(true);
            return unsat.Any(s => decomp.Equals(s.decompose()));
        }

        public bool isSatisfied()
        {
            checkSentenceSatisfaction();
            if (left == null) { leafCheckOpenClosed(); }

            // This one can only be satisfied if both its children are
            if (left != null)
            {
                bool ls = left.isSatisfied();

                if (ls && left.isOpen()) { state = NodeState.OPEN; return true; }

                if (right != null)
                {
                    bool rs = right.isSatisfied();
                    if (ls && rs)
                    {
                        if (left.isClosed() && right.isClosed()) { state = NodeState.CLOSED; }
                        else if (left.isOpen() || right.isOpen()) { state = NodeState.OPEN; }
                        else { state = NodeState.INCOMPLETE; }

                        return true;
                    }

                    return false;
                }

                if (ls)
                {
                    if (left.isClosed()) { state = NodeState.CLOSED; }
                    else if (left.isOpen()) { state = NodeState.OPEN; }
                    else { state = NodeState.INCOMPLETE; }

                    return true;
                }

                return false;
            }

            if (isIncomplete()) { return false; }

            return true;
        }

        public void satisfy()
        {
            while (!isSatisfied()) { decompose(); }
        }

        public void reset()
        {
            selected = false;
            state = NodeState.INCOMPLETE;

            foreach (Pair<Sentence, bool> p in sentences)
            {
                p.Second = p.First.isDecomposable();
            }

            if (left != null) { left.reset(); }
            if (right != null) { right.reset(); }
        }

        public void deslect()
        {
            selected = false;

            if (left != null) { left.deslect(); }
            if (right != null) { right.deslect(); }
        }

        public void drawToGraphics(Graphics g, int x, int y)
        {
            Brush b;
            if (selected) { b = new SolidBrush(Color.FromArgb(175, 175, 255)); }
            else { b = new SolidBrush(Color.White); }
            g.FillRectangle(b,
                x + selfbox.Left,
                y + selfbox.Top,
                selfbox.Width,
                selfbox.Height);

            Pen pe;
            if (isClosed()) { pe = new Pen(Color.Blue); }
            else if (isOpen()) { pe = new Pen(Color.Green); }
            else if (isIncomplete()) { pe = new Pen(Color.Purple); }
            else { pe = new Pen(Color.Red); }

            g.DrawRectangle(pe,
                x + selfbox.Left,
                y + selfbox.Top,
                selfbox.Width,
                selfbox.Height);

            int num = 0;
            foreach (Pair<Sentence, bool> p in sentences)
            {
                Brush br;

                if (p.Second) { br = new SolidBrush(problem); }
                else { br = new SolidBrush(done); }

                g.DrawString(p.First.ToString(),
                    font,
                    br,
                    x + selfbox.Left + (selfbox.Width / 2) - ((9 * p.First.ToString().Length) / 2),
                    y + (num * height));
                num++;
            }

            if (left == null)
            {
                String str = "";
                if (isOpen()) { str = "O"; }
                else if (isClosed()) { str = "X"; }

                g.DrawString(str,
                    font,
                    new SolidBrush(Color.Black),
                    x + selfbox.Left + (selfbox.Width / 2) - 4,
                    y + (num * height));
            }

            if (left != null)
            {
                g.DrawLine(new Pen(Color.Black),
                    x + selfbox.Left + (selfbox.Width / 2),
                    y + selfbox.Bottom,
                    x + leftbox.Left + (leftbox.Width / 2),
                    y + leftbox.Top);

                left.drawToGraphics(g, x + leftbox.Left, y + leftbox.Top);
            }

            if (right != null)
            {
                g.DrawLine(new Pen(Color.Black),
                    x + selfbox.Left + (selfbox.Width / 2),
                    y + selfbox.Bottom,
                    x + rightbox.Left + (rightbox.Width / 2),
                    y + rightbox.Top);

                right.drawToGraphics(g, x + rightbox.Left, y + rightbox.Top);
            }
        }

        public void updateSizes()
        {
            int num = 0;
            int lon = 0;

            foreach(Pair<Sentence, bool> p in sentences)
            {
                if (p.First.ToString().Length > lon) { lon = p.First.ToString().Length; }
                num++;
            }

            if (left == null)
            {
                if (isOpen() || isClosed()) { num++; }
            }

            selfbox.Size = new Size((lon * 9) + 10, (num * height) + 3);

            if (left == null)
            {
                selfbox.Y = 0;
                selfbox.X = 0;

                fullbox.Width = selfbox.Width + 2;
                fullbox.Height = selfbox.Height + 2;
            }
            else if (right == null)
            {
                left.updateSizes();

                fullbox.Width = Math.Max(selfbox.Width, left.fullbox.Width) + 2;
                fullbox.Height = selfbox.Height + left.fullbox.Height + 10;

                selfbox.Y = 0;
                selfbox.X = (fullbox.Width / 2) - (selfbox.Width / 2);

                leftbox.X = (fullbox.Width / 2) - (left.fullbox.Width / 2);
                leftbox.Y = selfbox.Bottom + 10;
                leftbox.Width = left.fullbox.Width;
                leftbox.Height = left.fullbox.Height;
            }
            else
            {
                left.updateSizes();
                right.updateSizes();

                fullbox.Width = Math.Max(selfbox.Width, (left.fullbox.Width + right.fullbox.Width + 11)) + 2;
                fullbox.Height = selfbox.Height + 11 + Math.Max(left.fullbox.Height, right.fullbox.Height);

                selfbox.Y = 0;
                selfbox.X = (fullbox.Width / 2) - (selfbox.Width / 2);

                leftbox.X = 0;
                leftbox.Y = selfbox.Bottom + 10;
                leftbox.Width = left.fullbox.Width;
                leftbox.Height = left.fullbox.Height;

                rightbox.X = fullbox.Width - right.fullbox.Width;
                rightbox.Y = selfbox.Bottom + 10;
                rightbox.Width = right.fullbox.Width;
                rightbox.Height = right.fullbox.Height;
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (Pair<Sentence, bool> p in sentences)
            {
                s += p.First.ToString() + "\n";
            }

            return s;
        }
    }
}
