using System.Drawing;

namespace Po1450_SVGAnimace.Models
{
    public abstract class Obrazec
    {
        public Obrazec(Globals.TypObrazce typObrazce, Color barva, int pozX, int pozY) 
        { 
            TypObrazce = typObrazce;
            Barva = barva;
            PozX = pozX;
            PozY = pozY;
        }
        public Globals.TypObrazce TypObrazce { get; set; }
        public Color Barva { get; set; }
        public int PozX { get; set; }
        public int PozY { get; set;}

        public void PosunObjekt(int krok, int svgWidth, int svgHeight, int vychoziPozice)
        {
            PozX += krok;
            PozY += krok;

            if (PozX >= svgWidth + Math.Abs(vychoziPozice)) 
            {
                PozX = vychoziPozice;
            }
            if (PozY >= svgHeight + Math.Abs(vychoziPozice))
            {
                PozY = vychoziPozice;
            }
        }
    }
}
