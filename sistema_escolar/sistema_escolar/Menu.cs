using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sistema_escolar
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            CargarDatos(); // Llama al método para cargar datos en el DataGridView al cargar el formulario
        }

        private void CargarDatos()
        {
            string connectionString = "server=localhost;port=3306;database=sistema_escolar;uid=daniel;password=123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    connection.Open();
                    MessageBox.Show("Conexión exitosa");

                    // Consulta SELECT para cargar datos en el DataGridView
                    string query = "SELECT * FROM alumnos";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Procesa los datos obtenidos y agrega filas al DataGridView
                            int columna1 = reader.GetInt32("id_alumno");
                            String columna2 = reader.GetString("apellido_1");
                            String columna3 = reader.GetString("apellido_2");
                            String columna4 = reader.GetString("nombre_alumno");
                            String columna5 = reader.GetString("correo_alumno");
                            dataGridView1.Rows.Add(columna1, columna2, columna3, columna4, columna5);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertarDatos(); // Llama al método para insertar datos en la base de datos al hacer clic en el botón
        }

        private void InsertarDatos()
        {
            string connectionString = "server=localhost;port=3306;database=sistema_escolar;uid=daniel;password=123;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexión exitosa");

                    // Consulta INSERT para insertar datos en la base de datos
                    string insertQuery = "INSERT INTO alumnos (id_alumno, apellido_1, apellido_2, nombre_alumno, correo_alumno) VALUES (@valor1, @valor2, @valor3, @valor4, @valor5)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);

                    // Parámetros para el INSERT obtenidos de los TextBox
                    insertCommand.Parameters.AddWithValue("@valor1", textBox1.Text);
                    insertCommand.Parameters.AddWithValue("@valor2", textBox2.Text);
                    insertCommand.Parameters.AddWithValue("@valor3", textBox3.Text);
                    insertCommand.Parameters.AddWithValue("@valor4", textBox4.Text);
                    insertCommand.Parameters.AddWithValue("@valor5", textBox5.Text);

                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    MessageBox.Show("Filas afectadas por el INSERT: " + rowsAffected);

                    CargarDatos(); // Vuelve a cargar los datos en el DataGridView después de la inserción
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EjecutarDelete(); // Llama al método para ejecutar la eliminación al hacer clic en el botón
        }

        private void EjecutarDelete()
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int idToDelete = Convert.ToInt32(textBox1.Text);

                string connectionString = "server=localhost;port=3306;database=sistema_escolar;uid=daniel;password=123;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        MessageBox.Show("Conexión exitosa");

                        // Consulta DELETE para eliminar el registro basado en el ID proporcionado en textBox1
                        string deleteQuery = "DELETE FROM alumnos WHERE id_alumno = @idToDelete";
                        MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@idToDelete", idToDelete);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        MessageBox.Show("Filas afectadas por la eliminación: " + rowsAffected);

                        CargarDatos(); // Vuelve a cargar los datos en el DataGridView después de la eliminación
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID de alumno válido en textBox1 para eliminar el registro.");
            }
        }






        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Llena los TextBox con los datos seleccionados en el DataGridView
            textBox1.Text = dataGridView1[0, e.RowIndex].Value.ToString();
            textBox2.Text = dataGridView1[1, e.RowIndex].Value.ToString();
            textBox3.Text = dataGridView1[2, e.RowIndex].Value.ToString();
            textBox4.Text = dataGridView1[3, e.RowIndex].Value.ToString();
            textBox5.Text = dataGridView1[4, e.RowIndex].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                int idToUpdate = Convert.ToInt32(textBox1.Text);

                string connectionString = "server=localhost;port=3306;database=sistema_escolar;uid=daniel;password=123;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        MessageBox.Show("Conexión exitosa");

                        // Consulta UPDATE para actualizar el registro basado en el ID proporcionado en textBox1
                        string updateQuery = "UPDATE alumnos SET apellido_1 = @apellido1, apellido_2 = @apellido2, nombre_alumno = @nombre, correo_alumno = @correo WHERE id_alumno = @idToUpdate";
                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@apellido1", textBox2.Text);
                        updateCommand.Parameters.AddWithValue("@apellido2", textBox3.Text);
                        updateCommand.Parameters.AddWithValue("@nombre", textBox4.Text);
                        updateCommand.Parameters.AddWithValue("@correo", textBox5.Text);
                        updateCommand.Parameters.AddWithValue("@idToUpdate", idToUpdate);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        MessageBox.Show("Filas afectadas por la actualización: " + rowsAffected);

                        CargarDatos(); // Vuelve a cargar los datos en el DataGridView después de la actualización
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un ID de alumno válido en textBox1 para actualizar el registro.");
            }
        }
    }
}