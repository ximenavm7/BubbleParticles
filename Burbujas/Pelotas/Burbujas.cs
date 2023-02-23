using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Runtime.Remoting.Channels;

namespace Pelotas
{
    public class Burbujas
    {
        int index;
        Size space;
        public Color c;
        // Variables de posición
        public float x;
        public float y;

        // Variables de velocidad
        private float vx;
        private float vy;

        // Variable de radio
        public float radio;


        // Constructor
        public Burbujas(Random rand,Size size, int index)
        {

            this.radio  = rand.Next(15, 20);
            this.x      = rand.Next((int)radio, size.Width - (int)radio);
            this.y      = size.Height - radio;         
            c           = Color.FromArgb(rand.Next(200, 255), rand.Next(250, 255), rand.Next(235, 255), rand.Next(240, 255));

            // Velocidades iniciales
            this.vx = rand.Next(1, 5);
            this.vy = rand.Next(-6,-1);

            this.index = index;
            space = size;
        }

        // Método para actualizar la posición de la burbuja en función de su velocidad
        public void Update(float deltaTime, List<Burbujas> balls, Random rand, Size size)
        { 
            if ((y - radio) <= 0)
            {
                vy = rand.Next(-6, -1);
                y = size.Height - radio;
                x = rand.Next((int)radio, size.Width - (int)radio);
            }

            if ((x - radio) <= 0 || (x + radio) >= space.Width)
            {
                if (x - radio <= 0)
                    x = radio + 3;
                else
                    x = space.Width - radio-3;
                    
                //vx *= -.01f;
                vy *= .5f;
            }
            
            if ((y - radio) <= 0 || (y + radio) >= space.Height)
            {
                if (y - radio<=  0)
                    y = radio + 3;
                else
                    y = space.Height - radio-3;

                
                //vx *=  .01f;
                vy *= -.5f;
            }

            //this.x += this.vx + .55f; // * deltaTime;
            this.y -= this.vy + 4; // * deltaTime;
        }
    }

}
