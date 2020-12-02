using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proyecto_final_csharp_wpf
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public async void ObtenerProveedores()
        {
            try
            {
                string result1 = await ProveedorAPI.ObtenerProveedores();
                //string result1 = "[{\"idProveedor\":4,\"razonSocial\":\"Empresa A\",\"telefono\":\"021-369847\",\"direccion\":\"Calle A\",\"RUC\":\"123456789 -9\"},{\"idProveedor\":5, \"razonSocial\":\"Empresa B\",\"telefono\":\"068 -2688769\",\"direccion\":\"Calle B\",\"RUC\":\"45678912 -9\"},{\"idProveedor\":6,\"razonSocial\":\"Proveedor 1\",\"telefono\":\"0971 -396547\",\"direccion\":\"Direccion\",\"RUC\":\"454545456\"}]";

                JArray JarrayProveedores = JArray.Parse(result1);



                dg_proveedores.ItemsSource = JarrayProveedores;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerProveedores();
        }

        private void Dg_proveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_proveedores.SelectedItem != null)
                {
                    dynamic proveedordynamic = dg_proveedores.SelectedItem;
                    txt_ruc.Text = proveedordynamic.RUC;
                    txt_razon.Text = proveedordynamic.razonSocial;
                    txt_telefono.Text = proveedordynamic.telefono;
                    txt_direccion.Text = proveedordynamic.direccion;
                }
            }
            catch (Exception ex)
            {
                limpiar();
            }
            
        }

        private async void Btn_crear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validarCampos() == true)
                {
                    dynamic proveedordynamic = new ExpandoObject();
                    proveedordynamic.RUC = txt_ruc.Text;
                    proveedordynamic.razonSocial = txt_razon.Text;
                    proveedordynamic.telefono = txt_telefono.Text;
                    proveedordynamic.direccion = txt_direccion.Text;

                    var proveedoragregado = await ProveedorAPI.AgregarProveedor(proveedordynamic);
                    MessageBox.Show(proveedoragregado);
                    limpiar();
                    ObtenerProveedores();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }

        private async void Btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_proveedores.SelectedItem != null)
                {
                    if (validarCampos() == true)
                    {
                        dynamic proveedordynamic = dg_proveedores.SelectedItem;

                        proveedordynamic.RUC = txt_ruc.Text;
                        proveedordynamic.razonSocial = txt_razon.Text;
                        proveedordynamic.telefono = txt_telefono.Text;
                        proveedordynamic.direccion = txt_direccion.Text;

                        var proveedormodif = await ProveedorAPI.ModificarProveedor(proveedordynamic);
                        MessageBox.Show(proveedormodif);
                        limpiar();
                        ObtenerProveedores();
                    }
                }
                else
                    MessageBox.Show("Debe seleccionar primeramente el proveedor a modificar ");
            }catch(Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private async void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_proveedores.SelectedItem != null)
                {
                    dynamic proveedordynamic = dg_proveedores.SelectedItem;
                    var proveedoragregado = await ProveedorAPI.EliminarProveedor(proveedordynamic);
                    MessageBox.Show(proveedoragregado);
                    limpiar();
                    ObtenerProveedores();
                }
                else
                    MessageBox.Show("Debe seleccionar primeramente el proveedor a eliminar ");
            }catch(Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void limpiar()
        {
            txt_ruc.Clear();
            txt_razon.Clear();
            txt_telefono.Clear();
            txt_direccion.Clear();
        }

        public bool validarCampos()
        {
            if (txt_ruc.Text == "")
            {
                MessageBox.Show("El campo RUC no puede quedar en blanco");
                return false;
            }
            if (txt_razon.Text == "")
            {
                MessageBox.Show("El campo Razón Social no puede quedar en blanco");
                return false;
            }
            if (txt_telefono.Text == "")
            {
                MessageBox.Show("El campo Teléfono no puede quedar en blanco");
                return false;
            }
            if (txt_direccion.Text == "")
            {
                MessageBox.Show("El campo Dirección no puede quedar en blanco");
                return false;
            }
            return true;
        }
    }
    
}
