﻿using Parcial_Final;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Parcial_Final
{
    public partial class AgregarAlumno : Window
    {
        public AgregarAlumno()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT UsuarioID, Nombre FROM Usuarios", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                UsuarioComboBox.ItemsSource = dt.DefaultView;
                ConexionBD.CloseConnection(conn);
            }
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            string carnet = CarnetTextBox.Text;
            string nombre = NombreTextBox.Text;
            string telefono = TelefonoTextBox.Text;
            string grado = GradoTextBox.Text;
            int usuarioID = (int)UsuarioComboBox.SelectedValue;

            using (SqlConnection conn = ConexionBD.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Alumnos (Carnet, Nombre, Telefono, Grado, UsuarioID) VALUES (@Carnet, @Nombre, @Telefono, @Grado, @UsuarioID)", conn);
                cmd.Parameters.AddWithValue("@Carnet", carnet);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Telefono", telefono);
                cmd.Parameters.AddWithValue("@Grado", grado);
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);
                cmd.ExecuteNonQuery();
                ConexionBD.CloseConnection(conn);
            }
            this.Close();
        }
    }
}
