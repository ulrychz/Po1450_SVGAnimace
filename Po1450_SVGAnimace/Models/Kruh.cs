using Po1450_SVGAnimace.Globals;
using System.Drawing;

namespace Po1450_SVGAnimace.Models
{
    public class Kruh : Obrazec
    {
        public Kruh(TypObrazce typObrazce, Color barva, int pozX, int pozY, int polomer) : 
            base(typObrazce, barva, pozX, pozY)
        {
            Polomer = polomer;
        }
        public int Polomer { get; set; }
    }
}
