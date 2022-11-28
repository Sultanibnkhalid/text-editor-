
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class Form1 : Form
    {
        
        string path;
        public Form1()
        {
            InitializeComponent();
            this.tsslFileName.Text = "file Name : ";
            this.tsslLength.Text = "length : 0 ";
            this.tssllines.Text = "lines : 0";
            this.tsslProduct.Text = string.Format("Product Name: {0}", Application.ProductName);
            CheckForIllegalCrossThreadCalls = false;
            var th = Properties.Settings.Default.themes;
            skinEngine1.SkinFile = th;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            path = string.Empty;
            textBox1.Clear();



        }

      
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            path = ofd.FileName;
                            Task<string> text = sr.ReadToEndAsync();
                            textBox1.Text = text.Result;
                            this.tsslFileName.Text =string.Format("file Name : {0}", ofd.FileName) ;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            path = sfd.FileName;
                            this.tsslFileName.Text = string.Format("file Name : {0}",sfd.FileName);
                            using (StreamWriter sw = new StreamWriter(path))
                            {
                                await sw.WriteLineAsync(textBox1.Text);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
            else
            {
                try
                {

                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        await sw.WriteLineAsync(textBox1.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(path))
                        {
                            await sw.WriteLineAsync(textBox1.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Thread thread = new Thread(Th_Exec);
            thread.Start();
            try
            {
                
                toolStripComboBox1.SelectedIndex = 0;
            }
            catch
            {

            }
           // this.WindowState = FormWindowState.Maximized;
        }

        private void Th_Exec()
        {
            Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
           WindowState = FormWindowState.Maximized;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Copy();
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Paste();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Cut();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectedText = "";
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.SelectAll();
        }

        private void enduToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.textBox1.Undo();
          
        }

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.WordWrap = (this.textBox1.WordWrap == true) ? false : true;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                using (FontDialog fd = new FontDialog())
                {
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        Font x = fd.Font;

                        this.textBox1.Font = x;
                    }
                }
           
        }

        private void statesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.statusStrip1.Visible = (this.statusStrip1.Visible == true) ? false : true;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmAbout frm = new frmAbout())
            {
                frm.ShowDialog();
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Font = new Font(this.textBox1.Font.FontFamily, this.textBox1.Font.Size + 2);
            }
            catch
            {
                this.textBox1.Font = this.textBox1.Font;
            }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Font = new Font(this.textBox1.Font.FontFamily, this.textBox1.Font.Size - 2);
            }
            catch
            {
                this.textBox1.Font = this.textBox1.Font;
            }
        }

        private void defualtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBox1.Font = DefaultFont;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.tsslLength.Text =string.Format( "length : {0} ",this.textBox1.TextLength);
            this.tssllines.Text =string.Format( "lines : {0}",this.textBox1.Lines.Length);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
    
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.Document = this.printDocument1;
            if (printPreviewDialog1.ShowDialog()==DialogResult.OK)
            {
               this.printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(textBox1.Text, new Font("Times New Roman ", 14, FontStyle.Bold), Brushes.Black, new Point(100, 100));
        }

        private void pageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pageSetupDialog1.PageSettings = new System.Drawing.Printing.PageSettings();
            this.pageSetupDialog1.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            this.pageSetupDialog1.ShowDialog();
            if (this.pageSetupDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.DefaultPageSettings = this.pageSetupDialog1.PageSettings;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void defualtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.textBox1.Font = DefaultFont;
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
           
        
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = toolStripComboBox1.SelectedIndex;
            if (x == 1)
            {
                skinEngine1.SkinFile = "mp10pink.ssk";
                Properties.Settings.Default.themes = "mp10pink.ssk";
                Properties.Settings.Default.Save();


            }
            else if (x == 2)
            {
                skinEngine1.SkinFile = "mp10purple.ssk";
                Properties.Settings.Default.themes = "mp10pink.ssk";
                Properties.Settings.Default.Save();


            }
            else if (x == 3)
            {
                skinEngine1.SkinFile = "mp10green.ssk";
                Properties.Settings.Default.themes = "mp10green.ssk";
                Properties.Settings.Default.Save();


            }

            else if (x == 4)
            {
                skinEngine1.SkinFile = "SportsBlue.ssk";
                Properties.Settings.Default.themes = "SportsBlue.ssk";
                Properties.Settings.Default.Save();

            }
            else if (x == 5)
            {
                skinEngine1.SkinFile = "MP10.ssk";
                Properties.Settings.Default.themes = "MP10.ssk";
                Properties.Settings.Default.Save();

            }
            else if (x == 6)
            {
                skinEngine1.SkinFile = "mp10mulberry.ssk";
                Properties.Settings.Default.themes = "mp10mulberry.ssk";
                Properties.Settings.Default.Save();
                


            }


        }
    }
}