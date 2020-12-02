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
    /// Lógica de interacción para w_Marca.xaml
    /// </summary>
    public partial class w_Marca : Window
    {
        ComercialEntities db;
        public w_Marca()
        {
            InitializeComponent();
            db = new ComercialEntities();
        }

        private void CargarDatosGrilla()
        {
            try
            {                
                dg_marca.ItemsSource = db.Marca.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
        }

        private void Btn_crear_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if (txt_descripcion.Text != "")
                {
                    Marca m = new Marca();
                    m.descripcionMarca = txt_descripcion.Text;
                    db.Marca.Add(m);
                    db.SaveChanges();
                    MessageBox.Show("Marca creada");
                    txt_descripcion.Text = "";
                    CargarDatosGrilla();
                }
                else
                {
                    MessageBox.Show("La descripción no puede quedar en blanco");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_descripcion.Text = "";
                db.Dispose();
                db = new ComercialEntities();
                CargarDatosGrilla();
            }
        }


        private void Dg_marca_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dg_marca.SelectedItem != null)
                {
                    Marca marca = (Marca)dg_marca.SelectedItem;
                    txt_descripcion.Text = marca.descripcionMarca;                   
                }
            }
            catch (Exception ex)
            {
                txt_descripcion.Text = "";
                db.Dispose();
                db = new ComercialEntities();
                CargarDatosGrilla();
            }

        }

        private void Btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (dg_marca.SelectedItem != null)
                {
                    if (txt_descripcion.Text != "")
                    {
                        Marca m = (Marca)dg_marca.SelectedItem;


                        m.descripcionMarca = txt_descripcion.Text;
                        db.Entry(m).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Marca modificada");
                        txt_descripcion.Text = "";
                        CargarDatosGrilla();
                    }
                    else
                    {
                        MessageBox.Show("La descripción no puede quedar en blanco");
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una Marca de la grilla para eliminar!");
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_descripcion.Text = "";
                db.Dispose();
                db = new ComercialEntities();
                CargarDatosGrilla();
            }
        }

        private void Btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg_marca.SelectedItem != null)
                {
                    Marca m = (Marca)dg_marca.SelectedItem;


                    db.Marca.Remove(m);
                    db.SaveChanges();
                    MessageBox.Show("Marca eliminada");
                    txt_descripcion.Text = "";
                    CargarDatosGrilla();
                }
                else
                    MessageBox.Show("Debe seleccionar una Marca de la grilla para eliminar!");
            }catch(Exception ex)
            {
                MessageBox.Show("Se ha producido el siguiente error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txt_descripcion.Text = "";
                db.Dispose();
                db = new ComercialEntities();
                CargarDatosGrilla();
            }
        }
    }
}
