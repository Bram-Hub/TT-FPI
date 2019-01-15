using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using TruthTree.Logic;

namespace TruthTree.UI
{
    public partial class MainWindow : Form
    {
        Logic.TreeNode tree;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void resizeStuff()
        {
            if (tree == null) { return; }

            tree.updateSizes();
            panel1.Size = tree.fullbox;
        }

        private void checkTree()
        {
            if (tree == null) { return; }

            tree.isSatisfied();

            switch (tree.state)
            {
                case NodeState.CLOSED:
                    lResult.Text = "Closed";
                    lResult.ForeColor = Color.Black;
                    break;
                case NodeState.OPEN:
                    lResult.Text = "Open";
                    lResult.ForeColor = Color.Black;
                    break;
                case NodeState.INCOMPLETE:
                    lResult.Text = "Incomplete";
                    lResult.ForeColor = Color.Red;
                    break;
                case NodeState.ERROR:
                    lResult.Text = "Error";
                    lResult.ForeColor = Color.Red;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BaseInputDialog bid = new BaseInputDialog();
            if (bid.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tree = bid.getResult();

                if (cbAuto.Checked) { checkTree(); }

                resizeStuff();
                panel1.Refresh();
                bGrow.Enabled = true;
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tree == null) { return; }

            Logic.TreeNode selection = tree.getSelection();

            if (selection == null) { return; }

            bool overwrite = (selection.left != null);

            DecompositionInputDialog did = new DecompositionInputDialog();
            if (did.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Decomposition decomp = did.getResult();
                if (decomp == null) { return; }

                if (overwrite)
                {
                    selection.left = null;
                    selection.right = null;
                    tree.reset();
                    if (cbAuto.Checked) { checkTree(); }
                }

                if (!selection.isValidDecomposition(decomp))
                {
                    MessageBox.Show("That is not a valid decomposition", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                selection.setDecomposition(decomp);

                tree.deslect();

                if (cbAuto.Checked) { checkTree(); }

                resizeStuff();
                panel1.Refresh();
                button2.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (tree == null) { return; }

            panel1.VerticalScroll.Value = 0;
            panel1.HorizontalScroll.Value = 0;

            Graphics g = e.Graphics;

            tree.drawToGraphics(g, 0, 0);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tree == null) { return; }

            int tx = panel1.HorizontalScroll.Value + e.X;
            int ty = panel1.VerticalScroll.Value + e.Y;

            if (tree.checkClickForSelection(tx, ty) == null) { button2.Enabled = false; }
            else { button2.Enabled = true; }

            panel1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tree == null) { return; }

            tree.satisfy();

            checkTree();

            resizeStuff();
            panel1.Refresh();
        }

        private void cbAuto_CheckedChanged(object sender, EventArgs e)
        {
            bCheck.Enabled = !cbAuto.Checked;
        }

        private void bCheck_Click(object sender, EventArgs e)
        {
            if (tree == null) { return; }

            checkTree();

            resizeStuff();
            panel1.Refresh();
        }

        private void Thing_Load(object sender, EventArgs e)
        {
            // Do tests here?
        }

    }
}
