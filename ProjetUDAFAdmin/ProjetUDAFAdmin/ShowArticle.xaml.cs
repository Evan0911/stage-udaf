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
    /// Logique d'interaction pour ShowArticle.xaml
    /// </summary>
    public partial class ShowArticle : Window
    {
        public ShowArticle(int id)
        {
            InitializeComponent();

            bdd.Initialize();

            Article unArt = bdd.SearchArticle(id);

            TxtTitre.Text = unArt.titre;
            TxtAuteur.Text = unArt.auteur;
            TxtDate.Text = unArt.dateCrea.ToString();

            string contenu = unArt.contenu;
            char[] listeContenu = contenu.ToCharArray();
            for (int i = 0; listeContenu.Length > i; i++)
            {
                TxtContenu.Text += listeContenu[i];
                if (i % 100 == 0 && i != 0)
                {
                    TxtContenu.Text += "\n";
                }
            }
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
