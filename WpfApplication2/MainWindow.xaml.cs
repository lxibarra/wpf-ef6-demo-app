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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication2.dbObjects;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
               
        string ErrorNombreEmpleado = "La captura del nombre de empleado es invalida solo se aceptan letras y espacios";
        string ErrorIdEmpleado = "El id de empleado debe ser numerico";
        string ErrorNombreDepartamento = "El nombre de departamento es invalido solo se aceptan letras y espacios";
        string ErrorIdDepartamento = "El id de departamento debe ser numerico";

        string ErrorIdNoEncontrado = "El id buscado no existe en la bd";
        
        
        /// <summary>
        /// Alta de empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Utilerias.esTexto(txtnombre1.Text))
            {
                demoEF db = new demoEF();
                Empleado emp = new Empleado();
                emp.Nombre = txtnombre1.Text;
                emp.DepartamentoId = (int)cbDepartamentos.SelectedValue;
                db.Empleados.Add(emp);
                db.SaveChanges();

                Utilerias.LimpiarTextBoxes(txtnombre1);
                MostrarTodosLosEmpleados();
            }
            else
            {
                MessageBox.Show(ErrorNombreEmpleado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Actualizacion de empleados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Utilerias.esNumero(txtid.Text))
            {
                if (Utilerias.esTexto(txtnombre1.Text))
                {
                    int id = int.Parse(txtid.Text);
                    demoEF db = new demoEF();
                    Empleado emp = db.Empleados.SingleOrDefault(p => p.Id == id);
                    if (emp != null)
                    {
                        emp.Nombre = txtnombre1.Text;
                        emp.DepartamentoId = (int)cbDepartamentos.SelectedValue;
                        db.SaveChanges();
                        MostrarTodosLosEmpleados();
                    }
                    else
                    {
                        MessageBox.Show(ErrorIdNoEncontrado, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(ErrorNombreEmpleado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(ErrorIdEmpleado, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Alta de departamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Utilerias.esTexto(txtdep.Text))
            {
                demoEF db = new demoEF();
                Departamento dep = new Departamento();
                dep.Nombre = txtdep.Text;
                db.Departamentos.Add(dep);
                db.SaveChanges();

                RecargaCombo();

                Utilerias.LimpiarTextBoxes(txtdep);
            }
            else
            {
                MessageBox.Show(ErrorNombreDepartamento, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Actualizacion de departamentos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Utilerias.esNumero(txtdepId.Text))
            {
                if (Utilerias.esTexto(txtdep.Text))
                {
                    int id = int.Parse(txtdepId.Text);
                    demoEF db = new demoEF();
                    Departamento dep = db.Departamentos.SingleOrDefault(d => d.Id == id);
                    if (dep != null)
                    {
                        dep.Nombre = txtdep.Text;
                        db.SaveChanges();
                        Utilerias.LimpiarTextBoxes(txtdep, txtdepId);
                        RecargaCombo();
                        MostrarTodosLosEmpleados();
                    }
                    else
                    {
                        MessageBox.Show(ErrorIdNoEncontrado, "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(ErrorNombreDepartamento, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(ErrorIdDepartamento, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RecargaCombo();
            MostrarTodosLosEmpleados();
        }

        private void RecargaCombo()
        {
            demoEF db = new demoEF();
            cbDepartamentos.ItemsSource = db.Departamentos.ToList();
            cbDepartamentos.DisplayMemberPath = "Nombre";
            cbDepartamentos.SelectedValuePath = "Id";
            //hacemos default la primera opcion
            cbDepartamentos.SelectedIndex = 0;
          
        }

        private void MostrarTodosLosEmpleados()
        {
            demoEF db = new demoEF();

            var registros = from emps in db.Empleados
                            join deps in db.Departamentos
                            on emps.DepartamentoId equals deps.Id
                            select new
                            {
                                emps.Id,
                                emps.Nombre,
                                Departamento=deps.Nombre
                            };
            dbgrid1.ItemsSource = registros.ToList();
        }

       

        

     
    }
}
