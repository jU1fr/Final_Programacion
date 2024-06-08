using Parcial_Final;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Parcial_Final
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarDatos();
        }

        private void CargarDatos()
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT a.Carnet, a.Nombre, a.Telefono, a.Grado, a.UsuarioID, a.Estado, u.Nombre as UsuarioNombre FROM Alumnos a LEFT JOIN Usuarios u ON a.UsuarioID = u.UsuarioID", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                AlumnosDataGrid.ItemsSource = dt.DefaultView;
                ConexionBD.CloseConnection(conn);
            }
        }


        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            AgregarAlumno agregarAlumno = new AgregarAlumno();
            agregarAlumno.ShowDialog();
            CargarDatos();
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (AlumnosDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)AlumnosDataGrid.SelectedItem;

                // Verificar si "UsuarioID" está presente en la vista de datos
                if (row.Row.Table.Columns.Contains("UsuarioID"))
                {
                    // Ahora puedes continuar con el código
                    string carnet = row["Carnet"].ToString();
                    string nombre = row["Nombre"].ToString();
                    string telefono = row["Telefono"].ToString();
                    string grado = row["Grado"].ToString();
                    int usuarioID;

                    if (row["UsuarioID"] != DBNull.Value)
                    {
                        usuarioID = Convert.ToInt32(row["UsuarioID"]);
                    }
                    else
                    {
                        // Si UsuarioID es nulo, asignar un valor predeterminado o manejarlo según lo desees
                        usuarioID = -1; // Por ejemplo, asignar -1
                                        // O lanzar una excepción o mostrar un mensaje de error
                        MessageBox.Show("El UsuarioID es nulo. No se puede editar el alumno.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return; // Salir del método, ya que no podemos continuar sin el ID del usuario
                    }

                    EditarAlumno editarAlumno = new EditarAlumno(carnet, nombre, telefono, grado, usuarioID);
                    editarAlumno.ShowDialog();
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("El nombre de la columna 'UsuarioID' no se encuentra en la vista de datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }






        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (AlumnosDataGrid.SelectedItem != null)
            {
                DataRowView row = (DataRowView)AlumnosDataGrid.SelectedItem;
                string carnet = row["Carnet"].ToString();

                using (SqlConnection conn = ConexionBD.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Alumnos WHERE Carnet = @Carnet", conn);
                    cmd.Parameters.AddWithValue("@Carnet", carnet);
                    cmd.ExecuteNonQuery();
                    ConexionBD.CloseConnection(conn);
                }
                CargarDatos();
            }
        }


        private void RecuperarAlumnosEliminados()
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT a.Carnet, a.Nombre, a.Telefono, a.Grado, a.UsuarioID, a.Estado, u.Nombre as UsuarioNombre FROM Alumnos a LEFT JOIN Usuarios u ON a.UsuarioID = u.UsuarioID WHERE a.Estado = 0", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                AlumnosDataGrid.ItemsSource = dt.DefaultView;
                ConexionBD.CloseConnection(conn);
            }
        }



        public void EliminarAlumno(int idAlumno)
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Alumnos WHERE ID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", idAlumno);
                cmd.ExecuteNonQuery();

                // Registrar la eliminación en la tabla de registros eliminados
                RegistrarEliminacion("Alumno", idAlumno);

                ConexionBD.CloseConnection(conn);
            }
        }

        public void RecuperarAlumnoEliminado(int idRegistro)
        {
            // Recuperar el registro eliminado de la tabla de registros eliminados
            // y realizar las operaciones necesarias para restaurarlo en la tabla principal
            // (por ejemplo, insertarlo nuevamente en la tabla de alumnos)
        }

        private void RegistrarEliminacion(string tipoRegistro, int idRegistro)
        {
            // Insertar un registro en la tabla de registros eliminados
            // con el tipo de registro y el ID del registro eliminado
        }

    }
}
