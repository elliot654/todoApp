using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace todo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("saveData/todolist.txt"))
            {
                File.Create("saveData/todolist.txt");
            }
            string[] lines = File.ReadAllLines("saveData/todolist.txt");
            
                TextBox[] textBox = new TextBox[lines.Length];
                CheckBox[] checkBox = new CheckBox[lines.Length];
                int checkBoxX, checkBoxY, textboxX, textboxY;

                checkBoxX = 20;
                checkBoxY = 20;
                textboxX = 50;
                textboxY = 20;

                for (int i = 0; i < lines.Length; i++)
                {
                    textBox[i] = new TextBox();
                    textBox[i].Name = i.ToString();
                    textBox[i].Text = lines[i];
                    textBox[i].Location = new Point(textboxX, textboxY);

                    checkBox[i] = new CheckBox();
                    checkBox[i].Name = i.ToString();
                    checkBox[i].Location = new Point(checkBoxX, checkBoxY);

                    checkBoxY += 25;
                    textboxY += 25;
                }

                for (int i = 0; i < lines.Length; i++)
                {
                    this.Controls.Add(textBox[i]);
                    this.Controls.Add(checkBox[i]);
                }

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            string[] lines = File.ReadAllLines("saveData/todolist.txt");
           
            foreach (Control c in this.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = (CheckBox)c;
                    if (chk.Checked)
                    {
                        int i = int.Parse(chk.Name);
                        var foos = new List<string>(lines);
                        foos.RemoveAt(i);
                        lines = foos.ToArray();
                    }
                }
            }
            File.WriteAllLines("saveData/todolist.txt", lines);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines("saveData/todolist.txt");
            var list = new List<string>(lines);
            list.Add(txtNew.Text);
            File.WriteAllLines("saveData/todolist.txt",list);
            Form1_Load(null, EventArgs.Empty);
        }
    }
}
