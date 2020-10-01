using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ButtonAnimation
{
    public partial class Form1 : Form
    {
        private Button SomeButton;

        private Color MainColor;
        private Color MouseOverColor1;
        private Color MouseOverColor2;
        private Color MouseDownColor;

        public Form1()
        {
            InitializeComponent();

            MainColor = Color.DarkBlue;
            MouseOverColor1 = MainColor;
            MouseOverColor2 = Color.FromArgb(MouseOverColor1.R + 60, MouseOverColor1.G + 60, MouseOverColor1.B + 60);
            MouseDownColor = Color.FromArgb(MouseOverColor2.R + 40, MouseOverColor2.G + 40, MouseOverColor2.B + 40);

            SomeButton = new Button();
            SomeButton.TabIndex = 0;
            SomeButton.TabStop = false;
            SomeButton.Location = new Point(50, 50);
            SomeButton.Size = new Size(300, 200);
            SomeButton.FlatStyle = FlatStyle.Flat;
            SomeButton.BackColor = MainColor;
            SomeButton.FlatAppearance.MouseOverBackColor = MouseOverColor1;
            SomeButton.FlatAppearance.MouseDownBackColor = MouseDownColor;
            SomeButton.FlatAppearance.BorderSize = 0;
            SomeButton.MouseEnter += SomeButton_MouseEnter;
            SomeButton.MouseLeave += SomeButton_MouseLeave;

            this.Controls.Add(SomeButton);
        }

        private void SomeButton_MouseEnter(object sender, EventArgs e)
        {
            Color AnimationColor;
            Thread ButtonAnimation = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    AnimationColor = Color.FromArgb(MouseOverColor1.R + i * 6, MouseOverColor1.G + i * 6, MouseOverColor1.B + i * 6);
                    SomeButton.Invoke((MethodInvoker)(delegate { SomeButton.FlatAppearance.MouseOverBackColor = AnimationColor; }));
                    Thread.Sleep(1);
                }
                SomeButton.Invoke((MethodInvoker)(delegate { SomeButton.BackColor = SomeButton.FlatAppearance.MouseOverBackColor; }));
            });
            ButtonAnimation.IsBackground = true;
            ButtonAnimation.Start();
        }

        private void SomeButton_MouseLeave(object sender, EventArgs e)
        {
            Color AnimationColor;
            Thread ButtonAnimation = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    AnimationColor = Color.FromArgb(MouseOverColor2.R - i * 6, MouseOverColor2.G - i * 6, MouseOverColor2.B - i * 6);
                    SomeButton.Invoke((MethodInvoker)(delegate { SomeButton.BackColor = AnimationColor; }));
                    Thread.Sleep(1);
                }
                SomeButton.Invoke((MethodInvoker)(delegate { SomeButton.FlatAppearance.MouseOverBackColor = SomeButton.BackColor; }));
            });
            ButtonAnimation.IsBackground = true;
            ButtonAnimation.Start();
        }
    }
}
