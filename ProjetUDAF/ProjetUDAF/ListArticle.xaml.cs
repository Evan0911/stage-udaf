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

namespace ProjetUDAF
{
    /// <summary>
    /// Logique d'interaction pour ListArticle.xaml
    /// </summary>
    public partial class ListArticle : Window
    {
        public ListArticle()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }

        public void VoirArticle(object sender, RoutedEventArgs e)
        {
            ShowArticle sa = new ShowArticle(Convert.ToInt32(((Button)sender).Tag));
            sa.Show();
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WrpSizeChanged(object sender, SizeChangedEventArgs e)
        {
            List<Article> cArticle = bdd.SelectArticle();

            WrpArticle.Children.Clear();

            foreach (Article unArt in cArticle)
            {
                Viewbox vbTxt = new Viewbox();
                TextBlock txtTitre = new TextBlock();
                txtTitre.Text = unArt.titre;
                txtTitre.TextWrapping = TextWrapping.Wrap;
                vbTxt.Height = 50;
                vbTxt.Width = WrpArticle.ActualWidth * 0.8;
                vbTxt.Child = txtTitre;
                WrpArticle.Children.Add(vbTxt);

                Button btn = new Button();
                Viewbox vbBtn = new Viewbox();
                TextBlock txtBtn = new TextBlock();
                btn.Click += VoirArticle;
                btn.Tag = unArt.id;
                btn.Width = WrpArticle.ActualWidth * 0.2;
                btn.Height = 50;
                txtBtn.Text = "Lire cet article";
                txtBtn.TextWrapping = TextWrapping.Wrap;
                vbBtn.Child = txtBtn;
                btn.Content = vbBtn;
                WrpArticle.Children.Add(btn);
            }
        }
    }
}
