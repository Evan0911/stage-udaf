using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetUDAF
{
    class Raccourci
    {
        public int id { get; set; }
        public string path { get; set; }
        public string imgPath { get; set; }
        public string name { get; set; }
        public string metier { get; set; }
        public int colonne { get; set; }
        public int ligne { get; set; }


        public Raccourci(int Id, string Name, string Path, string ImgPath, string Metier, int Colonne, int Ligne)
        {
            id = Id;
            path = Path;
            imgPath = ImgPath;
            name = Name;
            metier = Metier;
            colonne = Colonne;
            ligne = Ligne;
        }
    }
}
