using Newtonsoft.Json;
using Po1450_SVGAnimace.Models;
using System.Drawing;

namespace Po1450_SVGAnimace.Pages
{
    public partial class SVG
    {
        public SVG() 
        {
            Task.Run(NacistNastaveni);
        }
        public int SvgWidth { get; set; } = 1600;
        public int SvgHeight { get; set; } = 800;
        public int MinRozmer { get; set; } = 20;
        public int MaxRozmer { get; set; } = 100;
        public bool AnimaceBezi { get; set; } = false;
        public int AnimaceKrok { get; set; } = 1;

        private Random rnd = new Random();
        private int TypObrazcePocet => Enum.GetNames(typeof(Globals.TypObrazce)).Length;
        public List<Obrazec> SvgObrazceList { get; set; } = new List<Obrazec>();
        private void PridatObrazec(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            Globals.TypObrazce typ;
            typ = (Globals.TypObrazce)rnd.Next(TypObrazcePocet);
            Color barva = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Obrazec? obrazec = null;
            int stranaA, pozX, pozY;
            switch (typ)
            {
                case Globals.TypObrazce.ctverec:
                    stranaA = rnd.Next(MinRozmer, MaxRozmer);
                    pozX = rnd.Next(SvgWidth - stranaA);
                    pozY = rnd.Next(SvgHeight - stranaA);
                    obrazec = new Ctverec(typ, barva, pozX, pozY, stranaA);
                    break;
                case Globals.TypObrazce.obdelnik:
                    stranaA = rnd.Next(MinRozmer, MaxRozmer);
                    var stranaB = rnd.Next(MinRozmer, MaxRozmer);
                    pozX = rnd.Next(SvgWidth - stranaA);
                    pozY = rnd.Next(SvgHeight - stranaB);
                    obrazec = new Obdelnik(typ, barva, pozX, pozY, stranaA, stranaB);
                    break;
                case Globals.TypObrazce.kruh:
                    stranaA = rnd.Next(MinRozmer, MaxRozmer);
                    pozX = rnd.Next(SvgWidth - stranaA);
                    pozY = rnd.Next(SvgHeight - stranaA);
                    obrazec = new Kruh(typ, barva, pozX, pozY,polomer: stranaA);
                    break;
            }
            if (obrazec!= null)
            {
                SvgObrazceList.Add(obrazec);
            }
        }
        private void OdebratObrazec(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            if (SvgObrazceList.Any())
            {
                SvgObrazceList.RemoveAt(SvgObrazceList.Count - 1);
            }
        }

      
        private async Task UlozitNastaveni(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            string json = JsonConvert.SerializeObject(SvgObrazceList, 
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented });
            await localStorage.SetItemAsync("data", json);
        }

        private async Task NacistNastaveni()
        {
            var json = await localStorage.GetItemAsync<string>("data");
            if (json != null)
            {
                var obrazce = JsonConvert.DeserializeObject<List<Obrazec>>(json, 
                    new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto, Formatting = Formatting.Indented });
                if (obrazce != null && obrazce.Any())
                {
                    SvgObrazceList.AddRange(obrazce);
                    StateHasChanged();
                }
            }
            
        }
        private async Task SmazatNastaveni()
        {
            await localStorage.RemoveItemAsync("data");
        }

        private void AnimaceSpustit()
        {
            AnimaceBezi = true;
            Task.Run(Posun);
        }

        private async Task Posun()
        {
            do
            {
                foreach (var item in SvgObrazceList)
                {
                    item.PosunObjekt(AnimaceKrok, SvgWidth, SvgHeight, -MaxRozmer);
                }
                StateHasChanged() ;
                await Task.Delay(10);
            } while (AnimaceBezi);
        }

        private void AnimaceZastavit()
        {
            AnimaceBezi = false;
        }
    }
}
