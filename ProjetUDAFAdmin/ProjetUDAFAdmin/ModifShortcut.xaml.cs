using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Xml;
using Microsoft.Win32;
using ProjetUDAFAdmin;

namespace ProjetUDAFAdmin
{
    /// <summary>
    /// Logique d'interaction pour ModifShortcut.xaml
    /// </summary>
    public partial class ModifShortcut : Window
    {
        int id;
        int row;
        int column;
        Raccourci rac;
        bool isAdding = false;

        public ModifShortcut()
        {
            InitializeComponent();

            foreach (string unM in bdd.SelectMetier())
            {
                CboMetier.Items.Add(unM);
            }

            for (int i = 1; i <= 5; i++)
            {
                if (i == 1)
                {
                    CboColumn.Items.Add(i);
                }
                else
                {
                    CboRow.Items.Add(i);
                    CboColumn.Items.Add(i);
                }
            }

            isAdding = true;
        }

        public ModifShortcut(int Id)
        {
            InitializeComponent();
            rac = bdd.SearchRaccourci(Id);
            TxtFilePath.Text = rac.path;
            TxtFileName.Text = rac.name;
            id = rac.id;
            TxtImgPath.Text = rac.imgPath;
            row = rac.ligne;
            column = rac.colonne;

            foreach (string unM in bdd.SelectMetier())
            {
                CboMetier.Items.Add(unM);
                if (unM == rac.metier)
                {
                    CboMetier.SelectedIndex = CboMetier.Items.Count - 1;
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                if (i == 1)
                {
                    CboColumn.Items.Add(i);
                }
                else
                {
                    CboRow.Items.Add(i);
                    CboColumn.Items.Add(i);
                }
                if (i == row)
                {
                    CboRow.SelectedIndex = i - 2;
                }
                if (i == column)
                {
                    CboColumn.SelectedIndex = i - 1;
                }
            }
        }

        private void BtnBowseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                TxtFilePath.Text = dlg.FileName;

                System.IO.StreamReader sr = new
                System.IO.StreamReader(dlg.FileName);
                string[] s = (dlg.FileName.ToString()).Split('\\');
                int count = s.Length;
                TxtFileName.Text = s[count - 1];

                sr.Close();
            }
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (TxtFileName.Text == "" || TxtFilePath.Text == "")
            {
                MessageBox.Show("Veuillez remplir au minimum le nom et le chemin d'accès du raccourci.");
            }
            else if((!File.Exists(TxtFilePath.Text)) || (!File.Exists(TxtImgPath.Text) && TxtImgPath.Text != ""))
            {
                MessageBox.Show("Le chemin d'accès au raccourci et/ou à l'image n'existe pas");
            }
            else
            {
                if (!bdd.CheckRaccourci(id, CboMetier.SelectedItem.ToString(), Convert.ToInt32(CboRow.SelectedItem), Convert.ToInt32(CboColumn.SelectedItem)))
                {
                    if (isAdding)
                    {
                        ((PageView)this.Owner).AddRaccourci(TxtFilePath.Text, TxtFileName.Text, TxtImgPath.Text, bdd.SearchMetier(CboMetier.SelectedItem.ToString()), Convert.ToInt32(CboRow.SelectedItem),
                            Convert.ToInt32(CboColumn.SelectedItem));
                        this.Owner.Focus();
                        this.Close();
                    }
                    else
                    {
                        ((PageView)this.Owner).UpdateRaccourci(id, TxtFilePath.Text, TxtFileName.Text, TxtImgPath.Text, bdd.SearchMetier(CboMetier.SelectedItem.ToString()),
                            Convert.ToInt32(CboRow.SelectedItem), Convert.ToInt32(CboColumn.SelectedItem));
                        this.Owner.Focus();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Un raccourci existe déjà à cet emplacement");
                }
            }
        }

        private void BtnBrowseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.RestoreDirectory = true;
            dlg.Filter = "Image Files(*.BMP;*.PNG;*.JPG;*.GIF)|*.BMP;*.PNG;*.JPG;*.GIF";

            if (dlg.ShowDialog() == true)
            {
                TxtImgPath.Text = dlg.FileName;
            }
        }

        private void BtnClearImg_Click(object sender, RoutedEventArgs e)
        {
            TxtImgPath.Text = "";
        }

        private void AddMetier_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddMetier();
            if (dialog.ShowDialog() == true)
            {
                CboMetier.Items.Add(dialog.txt.Text);
                bdd.InsertMetier(dialog.txt.Text);
            }
        }

        private void BtnRetour_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
