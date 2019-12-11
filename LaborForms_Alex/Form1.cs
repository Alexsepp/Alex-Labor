using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaborForms_Alex
{
    public partial class Form1 : Form
    {
        bool drawing;
        int historyCounter;//history counter
        Image imgOriginal;
        GraphicsPath currentPath;
        Point oldLocation;
        public Pen currentPen;
        Color historyColor;//saving current color before using eraser
        List<Image> History;//list for history
        public Form1()
        {
            InitializeComponent();
            drawing = false;//This is for drawing
            currentPen = new Pen(Color.Black);//initializating pen with black color
            currentPen.Width = trackBar1.Value;//initializating pen width
            History = new List<Image>();//initializating list for history
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            History.Clear();
            historyCounter = 0;
            Bitmap pic = new Bitmap(661, 393);
            pictureBox1.Image = pic;
            History.Add(new Bitmap(pictureBox1.Image));
            if (pictureBox1.Image != null)
            {
                var result = MessageBox.Show("Save the current Image before Creating the new one?", "Warning", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No:break;
                    case DialogResult.Yes: saveMenu_Click(sender,e);break;
                    case DialogResult.Cancel:break;
                }
            }
        }

        private void saveMenu_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveD = new SaveFileDialog();
            SaveD.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveD.Title = "Save an Image File";
            SaveD.FilterIndex = 4;//By default Saving gonna be PNG
            SaveD.ShowDialog();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveD = new SaveFileDialog();
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.DrawImage(pictureBox1.Image, 0, 0, 661, 393);
            SaveD.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveD.Title = "Save an Image File";
            SaveD.FilterIndex = 4;//By default Saving gonna be PNG
            SaveD.ShowDialog();
            if (SaveD.FileName != "")//if entered not none name
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveD.OpenFile();
                switch (SaveD.FilterIndex)
                {
                    case 1: this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2: this.pictureBox1.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3: this.pictureBox1.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4: this.pictureBox1.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
            
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1;//By default it will open jpg file
            if (OP.ShowDialog() != DialogResult.Cancel)
                pictureBox1.Load(OP.FileName);
            pictureBox1.AutoSize = true;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            Bitmap pic = new Bitmap(661, 393);
            pictureBox1.Image = pic;
            if (pictureBox1.Image != null)
            {
                var result = MessageBox.Show("Save the current Image before Creating the new one?", "Warning", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.No: break;
                    case DialogResult.Yes: saveMenu_Click(sender, e); break;
                    case DialogResult.Cancel: break;
                }
            }
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OP = new OpenFileDialog();
            OP.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            OP.Title = "Open an Image File";
            OP.FilterIndex = 1;//By default it will open jpg file
            if (OP.ShowDialog() != DialogResult.Cancel)
                pictureBox1.Load(OP.FileName);
            pictureBox1.AutoSize = true;
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveD = new SaveFileDialog();
            SaveD.Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|GIF Image|*.gif|PNG Image|*.png";
            SaveD.Title = "Save an Image File";
            SaveD.FilterIndex = 4;//By default Saving gonna be PNG
            SaveD.ShowDialog();
            if (SaveD.FileName != "")//if entered not none name
            {
                System.IO.FileStream fs = (System.IO.FileStream)SaveD.OpenFile();
                switch (SaveD.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Jpeg);
                        break;
                    case 2:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Bmp);
                        break;
                    case 3:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Gif);
                        break;
                    case 4:
                        this.pictureBox1.Image.Save(fs, ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }

        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("First, create a new file!");
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
            }
            if (e.Button == MouseButtons.Right)
            {
                drawing = true;
                oldLocation = e.Location;
                currentPath = new GraphicsPath();
                //Graphics g = Graphics.FromImage(pictureBox1.Image);
                //g.DrawPath(currentPen = new Pen(Color.White), currentPath);
                currentPen = new Pen(Color.White);
            }
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //erasing no need history
            History.RemoveRange(historyCounter + 1,History.Count - historyCounter - 1);
            History.Add(new Bitmap(pictureBox1.Image));
            if (historyCounter + 1 < 10) historyCounter++;
            if (History.Count - 1 == 10) History.RemoveAt(0);
            drawing = false;
            try
            {
                currentPath.Dispose();
                
            }
            catch { };
            imgOriginal = pictureBox1.Image;
        }
        /*Image Zoom(Image img,int size)
        {
           
        }*/

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = e.X.ToString() + ", " + e.Y.ToString();
            if (drawing)
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                currentPath.AddLine(oldLocation, e.Location);
                g.DrawPath(currentPen, currentPath);
                oldLocation = e.Location;
                g.Dispose();
                pictureBox1.Invalidate();
                
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e)
        {
            currentPen.Width = trackBar1.Value;
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (History.Count != 0 && historyCounter != 0)
            {
                pictureBox1.Image = new Bitmap(History[--historyCounter]);
            }
            else MessageBox.Show("History is empty");
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (historyCounter < History.Count - 1)
            {
                pictureBox1.Image = new Bitmap(History[++historyCounter]);
            }
            else MessageBox.Show("History is empty");
        }

        private void SolidToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Solid;
            solidToolStripMenuItem1.Checked= true;
            dotToolStripMenuItem.Checked= false;
            dashDotDotToolStripMenuItem.Checked= false;
        }

        private void DotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.Dot;
            solidToolStripMenuItem1.Checked = false;
            dotToolStripMenuItem.Checked = true;
            dashDotDotToolStripMenuItem.Checked = false;
        }

        private void DashDotDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentPen.DashStyle = DashStyle.DashDotDot;
            solidToolStripMenuItem1.Checked = false;
            dotToolStripMenuItem.Checked = false;
            dashDotDotToolStripMenuItem.Checked = true;
        }

        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            Form2 fomm = new Form2();
            fomm.Owner = this;
            fomm.ShowDialog();
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            pictureBox1.Image = Zoom(imgOriginal, trackBar2.Value);
        }
    }
}
