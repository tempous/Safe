﻿using System;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsSafe
{
    public partial class Form1 : Form
    {
        int N = 4, step = 0;
        int size = 30;
        int left = 0, top = 0, offset = 5;
        Button[,] buttons = null;
        Random random = new();

        public Form1()
        {
            InitializeComponent();
            SetControlSize();
            SetHandles();

            if (Check(buttons))
            {
                MessageBox.Show("Safe opened successfully!\nGame over!");
                panel.Enabled = false;
            }
        }

        private void SetHandles()
        {
            foreach (var button in panel.Controls.OfType<Button>())
                button.Click -= Button_Click;
            panel.Controls.Clear();

            top = 0;
            buttons = new Button[N, N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (j == 0) left = 0;

                    var button = new Button()
                    {
                        Text = random.Next(0, 2) == 0 ? "-" : "|",
                        Width = size,
                        Height = size,
                        Left = left,
                        Top = top,
                        Tag = $"{i + 1} {j + 1}".ToString()
                    };

                    button.Click += Button_Click;
                    buttons[i, j] = button;
                    panel.Controls.Add(button);

                    left += size + offset;
                }

                top += size + offset;
            }
        }

        private void tbSize_KeyDown(object sender, KeyEventArgs e)
        {
            var textbox = (TextBox)sender;

            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(textbox.Text, out int N))
                {
                    this.N = N;
                    panel.Enabled = true;

                    SetControlSize();
                    SetHandles();
                }
                else
                    MessageBox.Show("Enter integer size!");
            }
        }

        private void SetControlSize()
        {
            panel.Width = panel.Height = (N * size) + (N - 1) * offset;
            Width = panel.Width + 35;
            Height = panel.Height + 75;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var coords = button.Tag.ToString().Split().Select(int.Parse).ToArray();
            var x = coords[0] - 1;
            var y = coords[1] - 1;

            buttons[x, y].Text = buttons[x, y].Text == "-" ? "|" : "-";

            lbStep.Text = $"Step {++step}";

            for (int i = 0; i < N; i++)
            {
                buttons[x, i].Text = buttons[x, i].Text == "-" ? "|" : "-";
                buttons[i, y].Text = buttons[i, y].Text == "-" ? "|" : "-";
            }

            if (Check(buttons))
            {
                MessageBox.Show("Safe opened successfully!\nGame over!");
                panel.Enabled = false;
            }
        }

        private static bool Check(Button[,] buttons, int horCount = 0, int verCount = 0)
        {
            var count = buttons.Length;

            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(0); j++)
                {
                    if (buttons[i, j].Text == "-")
                        horCount++;
                    else
                        verCount++;
                }
            }

            return horCount == count || verCount == count;
        }
    }
}
