using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) != System.Windows.Forms.DialogResult.Cancel)
            {
                label1.Text = openFileDialog1.FileName;
                textBox1.Text = Regex.Replace(openFileDialog1.SafeFileName, @"^[a-zA-Z0-9]", "_");              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) != System.Windows.Forms.DialogResult.Cancel)
            {
                Stream inStream = openFileDialog1.OpenFile();
                Stream outStream = saveFileDialog1.OpenFile();

                StreamWriter writer = new StreamWriter(outStream);

                int bite;

                writer.WriteLine("#region //" + textBox1.Text);
                writer.Write("byte[] " + textBox1.Text + " = new byte[] { ");

                if ((bite = inStream.ReadByte()) != -1)
                {
                    writer.Write(bite);

                    while ((bite = inStream.ReadByte()) != -1)
                        writer.Write(", " + bite);
                }

                writer.WriteLine("};");
                writer.Write("#endregion //" + textBox1.Text);

                writer.Flush();
                writer.Close();
            }

            byte[] b = new byte[] { 1, 2, 3 };
        }
    }
}
