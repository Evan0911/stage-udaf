using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetUDAFAdmin
{
    /// <summary>
    /// Logique d'interaction pour ModifArticle.xaml
    /// </summary>
    public partial class ModifArticle : Window
    {
        bool isAdding = false;
        int id;

        //Récupérer ici le nom de l'auteur via l'AD
        string auteur = "Gérard Menvussa";

        //Constructeur de l'ajout
        public ModifArticle()
        {
            InitializeComponent();
            isAdding = true;

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        //Constructeur de la modification
        public ModifArticle(int Id)
        {
            InitializeComponent();

            id = Id;

            Article unArt = bdd.SearchArticle(id);
            TxtTitre.Text = unArt.titre;
            TxtContenu.Text = unArt.contenu;

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (TxtTitre.Text == "" || TxtContenu.Text == "")
            {
                MessageBox.Show("Veuillez remplir le titre et le contenu de l'article");
            }
            else
            {
                if (isAdding)
                {
                    bdd.InsertArticle(TxtTitre.Text, TxtContenu.Text, auteur);
                    DialogResult = true;
                }
                else
                {
                    bdd.UpdateArticle(id, TxtTitre.Text, TxtContenu.Text);
                    DialogResult = true;
                }
            }
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
