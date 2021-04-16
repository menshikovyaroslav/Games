using MinesV2.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesV2
{
    public partial class Form1 : Form
    {
        Dictionary<Button, Element> _elements = new Dictionary<Button, Element>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var panel = new Panel();
            panel.Dock = DockStyle.Fill;

            for (int i = 0; i < Options.ElementCountX; i++)
            {
                for (int j = 0; j < Options.ElementCountY; j++)
                {
                    var btn = new Button() { Width = Options.ElementWidth, Height = Options.ElementHeight };
                    btn.Top = Options.ElementHeight * j;
                    btn.Left = Options.ElementWidth * i;
                    btn.Click += Btn_Click;

                    var el = new Element() { X = i, Y = j };
                    _elements[btn] = el;

                    //if (el.IsBomb) btn.Text = "b";
                    //else btn.Text = "-";

                    panel.Controls.Add(btn);
                }
            }

            this.Controls.Add(panel);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var el = _elements[btn];

            Calculate(el);

            el.IsShow = true;
        }

        private void Calculate(Element el)
        {
            el.IsShow = true;
            var btn = _elements.Single(e => e.Value.X == el.X && e.Value.Y == el.Y).Key;

            if (el.IsBomb)
            {
                MessageBox.Show("Bomb !");
            }
            else
            {
                var count = 0;
                foreach (var currEl in _elements.Values)
                {
                    if (Math.Abs(currEl.X - el.X) <= 1 && Math.Abs(currEl.Y - el.Y) <= 1 && currEl != el && currEl.IsBomb) count++;
                }
                btn.Text = count.ToString();

                if (count == 0)
                {
                    foreach (var currEl in _elements.Values)
                    {
                        if (Math.Abs(currEl.X - el.X) <= 1 && Math.Abs(currEl.Y - el.Y) <= 1 && !currEl.IsShow)
                        {
                            Calculate(currEl);
                        }
                    }
                }
            }
        }
    }
}
