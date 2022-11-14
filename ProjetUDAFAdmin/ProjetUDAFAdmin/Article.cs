using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetUDAFAdmin
{
    class Article
    {
        public int id { get; set; }
        public string auteur { get; set; }
        public string titre { get; set; }
        public string contenu { get; set; }
        public DateTime dateCrea { get; set; }

        public Article(int Id, string Auteur, string Titre, string Contenu, DateTime DateCrea)
        {
            id = Id;
            auteur = Auteur;
            titre = Titre;
            contenu = Contenu;
            dateCrea = DateCrea;
        }
    }
}
