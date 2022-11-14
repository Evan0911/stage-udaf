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
    /// Logique d'interaction pour AddMetier.xaml
    /// </summary>
    public partial class AddMetier : Window
    {
        public AddMetier()
        {
            InitializeComponent();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            if(txt.Text != "")
            {
                DialogResult = true;
            }
        }
    }
}
