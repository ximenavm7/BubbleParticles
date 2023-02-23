using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Pelotas.Properties;
using System.Reflection.Emit;

namespace Pelotas
{
    public partial class Particulas : Form
    {
        static List<Burbujas> balls;
        static Bitmap bmp = Resources.diver;
        static Graphics g;
        static Random rand = new Random();
        static float deltaTime;

        public Particulas()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (PCT_CANVAS.Width == 0)
                return;
            balls       = new List<Burbujas>();
            bmp         = new Bitmap(PCT_CANVAS.Width, PCT_CANVAS.Height);
            g           = Graphics.FromImage(bmp);
            deltaTime   = 1;
            PCT_CANVAS.Image = bmp;
            

            for (int b = 0; b < 50; b++)
            {
                balls.Add(new Burbujas(rand, PCT_CANVAS.Size, b));
            }
        }

        private void Pelotas_Load(object sender, EventArgs e)
        {
            Init();
            this.BackgroundImage = Resources.diver;
        }

        private void Pelotas_SizeChanged(object sender, EventArgs e)
        {
            Init();
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            g.DrawImage(this.BackgroundImage, 0, 0, this.Width, this.Height);
            Parallel.For(0, balls.Count, b =>//ACTUALIZAMOS EN PARALELO
            {
                Burbujas P;
                balls[b].Update(deltaTime, balls, rand, PCT_CANVAS.Size);
                P = balls[b];               
            });

            Burbujas p;

            for (int b = 0; b < balls.Count; b++)//PINTAMOS EN SECUENCIA
            {
                p = balls[b];

                // Dibujar la burbuja del tamaño de la pelota
                g.DrawImage(Resources.bubble, p.x - p.radio, p.y - p.radio, 2 * p.radio, 2 * p.radio);
            }


            PCT_CANVAS.Invalidate();
            deltaTime += .1f;
        }
    }
}
