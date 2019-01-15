using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using TruthTree.Logic;

namespace TruthTree.UI
{
    public partial class BaseInputDialog : Form
    {
        private Logic.TreeNode result;

        public BaseInputDialog()
        {
            InitializeComponent();
            result = null;
        }

        public Logic.TreeNode getResult() { return result; }

        private void bAccept_Click(object sender, EventArgs e)
        {
            string[] premises = tbPremises.Text.Split('\n');
            string conclusion = tbConclusions.Text;

            List<Sentence> sentences = new List<Sentence>();

            foreach (string p in premises)
            {
                Sentence s = Sentence.parseFromString(p);
                if (s != null && s.type != SentenceType.OTHER) { sentences.Add(s); }
            }

            Sentence se = Sentence.parseFromString(conclusion);
            if (se != null && se.type != SentenceType.OTHER) { sentences.Add(se.negation()); }

            if (sentences.Count > 0)
            {
                result = new Logic.TreeNode(sentences);
            }
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
