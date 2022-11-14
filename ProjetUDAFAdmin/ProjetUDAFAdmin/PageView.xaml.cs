using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace ProjetUDAFAdmin
{
    /// <summary>
    /// Logique d'interaction pour PageView.xaml
    /// </summary>
    public partial class PageView : Window
    {
        public PageView()
        {
            InitializeComponent();
            bdd.Initialize();

            InitializeCbo();

            InitializeInterface();

            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
        }
        private void ModifRaccourci(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((MenuItem)sender).Tag);
            ModifShortcut modif = new ModifShortcut(id);
            modif.Show();
            modif.Owner = this;
        }

        public void UpdateRaccourci(int id, string path, string name, string imgPath, int metier, int row, int column)
        {
            bdd.UpdateRaccourci(id, name, path, imgPath, metier, row, column);
            InitializeCbo();
        }

        private void DeleteRaccourci(object sender, EventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce raccourci ?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                bdd.DeleteRaccourci(Convert.ToInt32(((MenuItem)sender).Tag));
                InitializeInterface();
            }
        }

        private void cboMetier_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMetier.Items.Count != 0)
            {
                InitializeInterface();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ModifShortcut add = new ModifShortcut();
            add.Show();
            add.Owner = this;
        }

        public void AddRaccourci (string path, string name, string imgPath, int metier, int row, int column)
        {
            bdd.InsertRaccourci(name, path, imgPath, metier, row, column);
            InitializeCbo();
        }

        public void VoirArticle(object sender, RoutedEventArgs e)
        {
            ShowArticle sa = new ShowArticle(Convert.ToInt32(((Button)sender).Tag));
            sa.Show();
        }

        public void ToutArticle(object sender, RoutedEventArgs e)
        {
            ListArticle ta = new ListArticle();
            ta.Show();
        }

        public void InitializeCbo()
        {
            //Reset cbo
            cboMetier.Items.Clear();
            foreach (string unM in bdd.SelectMetier())
            {
                if (unM != "general")
                {
                    cboMetier.Items.Add(unM);
                }
            }
            cboMetier.SelectedIndex = 0;
        }

        private void BtnOpenApp_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(((Button)sender).Tag);
            Process.Start(bdd.SearchRaccourci(id).path);
        }

        public void InitializeInterface()
        {
            GrPlace.Children.Clear();

            //Ajout titre dernier article
            Article unArt = bdd.GetLastArticle();
            Viewbox vb = new Viewbox();
            TextBlock txtB = new TextBlock();

            txtB.Text = unArt.titre;
            txtB.TextWrapping = TextWrapping.Wrap;
            vb.Child = txtB;

            Grid.SetColumn(vb, 5);
            Grid.SetRow(vb, 6);
            Grid.SetColumnSpan(vb, 5);
            GrPlace.Children.Add(vb);

            //Ajout contenu dernier article
            Viewbox vbC = new Viewbox();
            TextBlock txtBC = new TextBlock();
            string contenu = unArt.contenu;
            char[] listeContenu = contenu.ToCharArray(); 
            for(int i = 0; i < 100 && listeContenu.Length > i; i++)
            {
                txtBC.Text += listeContenu[i];
                if (i%50 == 0 && i!= 0)
                {
                    txtBC.Text += "\n";
                }
            }
            txtBC.TextWrapping = TextWrapping.Wrap;
            txtBC.Margin = new Thickness(3);
            vbC.Child = txtBC;

            Grid.SetColumn(vbC, 5);
            Grid.SetRow(vbC, 7);
            Grid.SetColumnSpan(vbC, 5);
            Grid.SetRowSpan(vbC, 2);
            GrPlace.Children.Add(vbC);

            //Boutons articles
            Viewbox vbVoirArticle = new Viewbox();
            TextBlock txtVoirArticle = new TextBlock();
            Button btnVoirArticle = new Button();
            txtVoirArticle.Text = "Voir l'article en entier";
            txtVoirArticle.TextWrapping = TextWrapping.Wrap;
            vbVoirArticle.Child = txtVoirArticle;
            btnVoirArticle.Click += VoirArticle;
            btnVoirArticle.Content = vbVoirArticle;
            btnVoirArticle.Tag = bdd.GetLastArticle().id;
            Grid.SetColumn(btnVoirArticle, 5);
            Grid.SetRow(btnVoirArticle, 9);
            Grid.SetColumnSpan(btnVoirArticle, 2);
            GrPlace.Children.Add(btnVoirArticle);

            Viewbox vbToutArticle = new Viewbox();
            TextBlock txtToutArticle = new TextBlock();
            Button btnToutArticle = new Button();
            txtToutArticle.Text = "Voir tous les articles";
            txtToutArticle.TextWrapping = TextWrapping.Wrap;
            vbToutArticle.Child = txtToutArticle;
            btnToutArticle.Click += ToutArticle;
            btnToutArticle.Content = vbToutArticle;
            Grid.SetColumn(btnToutArticle, 8);
            Grid.SetRow(btnToutArticle, 9);
            Grid.SetColumnSpan(btnToutArticle, 2);
            GrPlace.Children.Add(btnToutArticle);

            //Différents titres
            Viewbox vbGeneral = new Viewbox();
            TextBlock txtGeneral = new TextBlock();
            txtGeneral.Text = "Raccourcis généraux";
            txtGeneral.TextWrapping = TextWrapping.Wrap;
            txtGeneral.TextDecorations = TextDecorations.Underline;
            vbGeneral.Child = txtGeneral;
            Grid.SetColumnSpan(vbGeneral, 5);
            Grid.SetColumn(vbGeneral, 0);
            Grid.SetRow(vbGeneral, 0);
            GrPlace.Children.Add(vbGeneral);

            Viewbox vbMetier = new Viewbox();
            TextBlock txtMetier = new TextBlock();
            txtMetier.Text = "Raccourcis personnels";
            txtMetier.TextWrapping = TextWrapping.Wrap;
            txtMetier.TextDecorations = TextDecorations.Underline;
            vbMetier.Child = txtMetier;
            Grid.SetColumnSpan(vbMetier, 5);
            Grid.SetColumn(vbMetier, 0);
            Grid.SetRow(vbMetier, 5);
            GrPlace.Children.Add(vbMetier);

            Viewbox vbPerso = new Viewbox();
            TextBlock txtPerso = new TextBlock();
            txtPerso.Text = "Raccourcis métiers";
            txtPerso.TextWrapping = TextWrapping.Wrap;
            txtPerso.TextDecorations = TextDecorations.Underline;
            vbPerso.Child = txtPerso;
            Grid.SetColumnSpan(vbPerso, 5);
            Grid.SetColumn(vbPerso, 5);
            Grid.SetRow(vbPerso, 0);
            GrPlace.Children.Add(vbPerso);

            Viewbox vbArticle = new Viewbox();
            TextBlock txtArticle = new TextBlock();
            txtArticle.Text = "Actualités de l'entreprise";
            txtArticle.TextWrapping = TextWrapping.Wrap;
            txtArticle.TextDecorations = TextDecorations.Underline;
            vbArticle.Child = txtArticle;
            Grid.SetColumnSpan(vbArticle, 5);
            Grid.SetColumn(vbArticle, 5);
            Grid.SetRow(vbArticle, 5);
            GrPlace.Children.Add(vbArticle);

            //set borders
            Border bor1 = new Border();
            bor1.BorderBrush = Brushes.Black;
            bor1.BorderThickness = new Thickness(1);
            Grid.SetColumn(bor1, 0);
            Grid.SetRow(bor1, 0);
            Grid.SetColumnSpan(bor1, 5);
            Grid.SetRowSpan(bor1, 5);
            GrPlace.Children.Add(bor1);

            Border bor2 = new Border();
            bor2.BorderBrush = Brushes.Black;
            bor2.BorderThickness = new Thickness(1);
            Grid.SetColumnSpan(bor2, 5);
            Grid.SetRowSpan(bor2, 5);
            Grid.SetColumn(bor2, 5);
            Grid.SetRow(bor2, 0);
            GrPlace.Children.Add(bor2);

            Border bor3 = new Border();
            bor3.BorderBrush = Brushes.Black;
            bor3.BorderThickness = new Thickness(1);
            Grid.SetColumnSpan(bor3, 5);
            Grid.SetRowSpan(bor3, 5);
            Grid.SetColumn(bor3, 0);
            Grid.SetRow(bor3, 5);
            GrPlace.Children.Add(bor3);

            Border bor4 = new Border();
            bor4.BorderBrush = Brushes.Black;
            bor4.BorderThickness = new Thickness(1);
            Grid.SetColumnSpan(bor4, 5);
            Grid.SetRowSpan(bor4, 5);
            Grid.SetColumn(bor4, 5);
            Grid.SetRow(bor4, 5);
            GrPlace.Children.Add(bor4);


            foreach (Raccourci unRac in bdd.SelectRaccourci())
            {
                if (unRac.metier == "general")
                {
                    ContextMenu ctxMenu = new ContextMenu();
                    MenuItem modif = new MenuItem();
                    MenuItem suppr = new MenuItem();
                    modif.Header = "Modifier";
                    suppr.Header = "Supprimer";
                    modif.Click += ModifRaccourci;
                    suppr.Click += DeleteRaccourci;
                    ctxMenu.Items.Add(modif);
                    ctxMenu.Items.Add(suppr);

                    Button button = new Button();
                    button.Tag = unRac.id;
                    button.Click += BtnOpenApp_Click;
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.VerticalAlignment = VerticalAlignment.Stretch;
                    button.ContextMenu = ctxMenu;

                    foreach (MenuItem unItem in ctxMenu.Items)
                    {
                        unItem.Tag = unRac.id;
                    }

                    if (unRac.imgPath != "")
                    {
                        Image imgObject = new Image();
                        BitmapImage imgSource = new BitmapImage();
                        imgSource.BeginInit();
                        imgSource.UriSource = new Uri(unRac.imgPath);
                        imgSource.EndInit();
                        imgObject.Source = imgSource;
                        imgObject.Width = 100;
                        imgObject.Height = 100;
                        button.Content = imgObject;
                    }
                    else
                    {
                        Viewbox unVb = new Viewbox();
                        TextBlock unTxt = new TextBlock();
                        unTxt.TextWrapping = TextWrapping.Wrap;
                        unTxt.Text = unRac.name;
                        unVb.Child = unTxt;
                        button.Content = unVb;
                    }

                    Grid.SetRow(button, unRac.ligne-1);
                    Grid.SetColumn(button, unRac.colonne-1);

                    GrPlace.Children.Add(button);
                }
                else if (unRac.metier == cboMetier.SelectedItem.ToString())
                {
                    ContextMenu ctxMenu = new ContextMenu();
                    MenuItem modif = new MenuItem();
                    MenuItem suppr = new MenuItem();
                    modif.Header = "Modifier";
                    suppr.Header = "Supprimer";
                    modif.Click += ModifRaccourci;
                    suppr.Click += DeleteRaccourci;
                    ctxMenu.Items.Add(modif);
                    ctxMenu.Items.Add(suppr);

                    Button button = new Button();
                    button.Tag = unRac.id;
                    button.Click += BtnOpenApp_Click;
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.VerticalAlignment = VerticalAlignment.Stretch;
                    button.ContextMenu = ctxMenu;

                    foreach (MenuItem unItem in ctxMenu.Items)
                    {
                        unItem.Tag = unRac.id;
                    }

                    if (unRac.imgPath != "")
                    {
                        Image imgObject = new Image();
                        BitmapImage imgSource = new BitmapImage();
                        imgSource.BeginInit();
                        imgSource.UriSource = new Uri(unRac.imgPath);
                        imgSource.EndInit();
                        imgObject.Source = imgSource;
                        button.Content = imgObject;
                    }
                    else
                    {
                        Viewbox unVb = new Viewbox();
                        TextBlock unTxt = new TextBlock();
                        unTxt.TextWrapping = TextWrapping.Wrap;
                        unTxt.Text = unRac.name;
                        unVb.Child = unTxt;
                        button.Content = unVb;
                    }

                    Grid.SetRow(button, unRac.ligne-1);
                    Grid.SetColumn(button, unRac.colonne+4);

                    GrPlace.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
