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

namespace proyecto_final_csharp_wpf
{
    /// <summary>
    /// Lógica de interacción para w_menu.xaml
    /// </summary>
    public partial class w_menu : Window
    {
        public w_menu()
        {
            InitializeComponent();
        }

        private void Btn_Marca_Click(object sender, RoutedEventArgs e)
        {
            w_Marca wm = new w_Marca();
            wm.ShowDialog();
        }

        private void Btn_Proveedor_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.ShowDialog();
        }
    }
}
