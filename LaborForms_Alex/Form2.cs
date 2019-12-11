using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaborForms_Alex
{
    public partial class Form2 : Form
    {
        Color colorResult;
        Color color;
        public Form2()
        {
            InitializeComponent();
             Form1 main = this.Owner as Form1;
                hScrollBar1.Tag = numericUpDown1;
                hScrollBar2.Tag = numericUpDown2;
                hScrollBar3.Tag = numericUpDown3;

                numericUpDown1.Tag = hScrollBar1;
                numericUpDown2.Tag = hScrollBar2;
                numericUpDown3.Tag = hScrollBar3;

                numericUpDown1.Value = color.R;
                numericUpDown2.Value = color.G;
                numericUpDown3.Value = color.B;
            
        }
        
        

            
        
        

        

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
        }

        private void NumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
        }

        private void HScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
            NumericUpDown numericUpDown = (NumericUpDown)sender;
            ScrollBar scrollBar = (ScrollBar)numericUpDown.Tag;
            scrollBar.Value = (int)numericUpDown.Value;
        }

        private void HScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
        }

        private void HScrollBar3_ValueChanged(object sender, EventArgs e)
        {
            UpdateColor();
            ScrollBar scrollBar = (ScrollBar)sender;
            NumericUpDown numericUpDown = (NumericUpDown)scrollBar.Tag;
            numericUpDown.Value = scrollBar.Value;
            
        }
        private void UpdateColor()
        {
            colorResult = Color.FromArgb(hScrollBar1.Value, hScrollBar2.Value, hScrollBar3.Value);
            pictureBox1.BackColor = colorResult;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                hScrollBar1.Value = colorDialog.Color.R;
                hScrollBar2.Value = colorDialog.Color.G;
                hScrollBar3.Value = colorDialog.Color.B;

                colorResult = colorDialog.Color;

                UpdateColor();
            }
        }
    }
}
