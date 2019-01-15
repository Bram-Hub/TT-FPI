using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using TruthTree.Logic;

namespace TruthTree.UI
{
    public partial class DecompositionInputDialog : Form
    {
        private Decomposition result;

        public DecompositionInputDialog()
        {
            InitializeComponent();
        }

        public Decomposition getResult() { return result; }

        private void bAccept_Click(object sender, EventArgs e)
        {
            string[] lsen = tbLeft.Text.Split('\n');
            string[] rsen = tbRight.Text.Split('\n');
            result = new Decomposition();

            foreach (string p in lsen)
            {
                Sentence s = Sentence.parseFromString(p);
                if (s != null && s.type != SentenceType.OTHER) { result.left.Add(s); }
            }

            foreach (string p in rsen)
            {
                Sentence s = Sentence.parseFromString(p);
                if (s != null && s.type != SentenceType.OTHER) { result.right.Add(s); }
            }

            if (result.left.Count == 0) { result.left.AddRange(result.right); result.right.Clear(); }

            if (result.left == null) { result = null; }

            Visible = false;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Visible = false;
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
