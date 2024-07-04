using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Schema;


namespace facturacion
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source =.; initial catalogo = facturacion; " + "Integrated Security = true");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            verdatos();
        }

        private void verdatos()
        {

            try
            {
                conexion.Open();
                string consulta = ("Select item, cantidad, descripcion, PU, PU*cantida as total from factura");
                SqlCommand Comando = new SqlCommand(consulta, conexion);
                SqlDataAdapter adapter = new SqlDataAdapter(Comando);
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Facturacion");
                dataGridView2.DataSource = dataSet.Tables["factura"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conexion.Close(); }





        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows) 
                {
                    if (!row.IsNewRow && !string.IsNullOrEmpty(row.Cells[0].Value.ToString())) 
                    {
                      int item = int.Parse(row.Cells[0].Value.ToString());
                      int cantidad = int.Parse(row.Cells[1].Value.ToString());
                      string descripcion = (row.Cells[2].Value.ToString());
                      double PU = double.Parse(row.Cells[3].Value.ToString());

                        string consulta = "insert into factura (item, cantidad, descripcion, pu) Values (@item, @cantidad, @descripcion, @pu";
                      SqlCommand comando = new SqlCommand(consulta, conexion);

                      comando.Parameters.AddWithValue("@item", item);
                      comando.Parameters.AddWithValue("@cantidad", cantidad);
                      comando.Parameters.AddWithValue("@descripcion", descripcion);
                      comando.Parameters.AddWithValue("@PU", PU);
                    }
                    
                }
                MessageBox.Show("Guardado con éxito");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
            finally 
            {
              conexion.Close();
              verdatos();
            }
        }
    }
}
