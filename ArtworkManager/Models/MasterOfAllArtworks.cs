using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtworkManager.Models
{
    public class MasterOfAllArtworks

    {


        public int Id { get; set; }
        public ICollection<Artwork> ALotOfArtworks { get; set; } = new List<Artwork>();

        public MasterOfAllArtworks(int id)
        {
            Id = id;
        }

        public void CreateAllArtworks ()
        {
            string x = "BM";
            int n = 1000;
            int jujubinha = 0;
            string number = jujubinha.ToString();
            
            for (int i = 0; i <= n; i++)
            {
                int y = i;
                string q1code = number + i;
                string code = q1code.ToString().PadLeft(6, '0');
                if (code.Length > 6)
                {
                    double codex = double.Parse(code);
                    code = codex.ToString();
                }
                string codexx = x + code;
                Artwork artwork = new Artwork(y,codexx);
                ALotOfArtworks.Add(artwork);
            }
        }

        public int ArtworkCodeId (string code)
        {
            int codey = int.Parse(code);
            return codey;
        }
    }
}
