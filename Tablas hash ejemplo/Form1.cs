using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tablas_hash_ejemplo
{
    public partial class Form1 : Form
    {
        // Dictionary para almacenar estudiantes (Clave: ID, Valor: objeto Estudiante)
        // Esta es la estructura principal que demuestra el uso de Dictionary<K,V>
        private Dictionary<int, Estudiante> registroEstudiantes;

        public Form1()
        {
            InitializeComponent();
            // Inicializar el Dictionary vacío
            registroEstudiantes = new Dictionary<int, Estudiante>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigurarDataGridView();
            CargarDatosEjemplo(); // Datos iniciales para demostración
        }

        // Configurar las columnas del DataGridView
        private void ConfigurarDataGridView()
        {
            dgvEstudiantes.Columns.Clear();
            dgvEstudiantes.Columns.Add("ID", "ID");
            dgvEstudiantes.Columns.Add("Nombre", "Nombre");
            dgvEstudiantes.Columns.Add("Carrera", "Carrera");
            dgvEstudiantes.Columns.Add("Promedio", "Promedio");
            dgvEstudiantes.Columns.Add("FechaRegistro", "Fecha de Registro");

            // Ajustar ancho de columnas
            dgvEstudiantes.Columns["ID"].Width = 80;
            dgvEstudiantes.Columns["Promedio"].Width = 100;
            dgvEstudiantes.Columns["FechaRegistro"].Width = 150;

            // Estilo del header
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEstudiantes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvEstudiantes.EnableHeadersVisualStyles = false;

            // Estilo de filas alternadas
            dgvEstudiantes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(189, 195, 199);
        }

        // Cargar datos de ejemplo
        private void CargarDatosEjemplo()
        {
            AgregarEstudianteInterno(1001, "Kevin Martínez", "Ingeniería en Sistemas", 10);
            AgregarEstudianteInterno(1002, "Marlon García", "Licenciatura en Informática", 9.2);
            AgregarEstudianteInterno(1003, "Ana Rodríguez", "Ingeniería en Computación", 8.8);
            AgregarEstudianteInterno(1004, "Carlos López", "Ingeniería en Sistemas", 7.9);
            ActualizarDataGridView();
        }

        // Método auxiliar para agregar sin validaciones (para ejemplos)
        private void AgregarEstudianteInterno(int id, string nombre, string carrera, double promedio)
        {
            if (!registroEstudiantes.ContainsKey(id))
            {
                Estudiante estudiante = new Estudiante(id, nombre, carrera, promedio);
                registroEstudiantes.Add(id, estudiante);
            }
        }

        // Actualizar el DataGridView con los datos del Dictionary
        private void ActualizarDataGridView()
        {
            dgvEstudiantes.Rows.Clear();

            // Iterar sobre el Dictionary usando KeyValuePair
            foreach (KeyValuePair<int, Estudiante> par in registroEstudiantes)
            {
                Estudiante est = par.Value;
                dgvEstudiantes.Rows.Add(
                    est.ID,
                    est.Nombre,
                    est.Carrera,
                    est.Promedio.ToString("F2"),
                    est.FechaRegistro.ToString("dd/MM/yyyy HH:mm")
                );
            }

            ActualizarContador();
        }

        // Actualizar el contador de estudiantes
        private void ActualizarContador()
        {
            lblTotal.Text = $"Total de estudiantes: {registroEstudiantes.Count}";
        }

        // ==================== BOTÓN AGREGAR ====================
        // Demuestra: Add(), ContainsKey() - Operaciones O(1)
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Por favor ingrese un ID", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                int id = int.Parse(txtID.Text);

                // Ventaja del Dictionary: verificar existencia en O(1)
                if (registroEstudiantes.ContainsKey(id))
                {
                    MessageBox.Show($"Ya existe un estudiante con el ID {id}",
                        "ID Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCarrera.Text) ||
                    string.IsNullOrWhiteSpace(txtPromedio.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double promedio = double.Parse(txtPromedio.Text);

                if (promedio < 0 || promedio > 10)
                {
                    MessageBox.Show("El promedio debe estar entre 0 y 10", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear y agregar estudiante al Dictionary - Operación O(1)
                Estudiante nuevoEstudiante = new Estudiante(id, txtNombre.Text,
                    txtCarrera.Text, promedio);
                registroEstudiantes.Add(id, nuevoEstudiante);

                ActualizarDataGridView();
                LimpiarCampos();

                MessageBox.Show("✓ Estudiante agregado exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese valores numéricos válidos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== BOTÓN BUSCAR ====================
        // Demuestra: TryGetValue() - Búsqueda segura en O(1)
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Por favor ingrese un ID para buscar", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                // TryGetValue: método seguro de búsqueda en O(1)
                // Retorna true si encuentra la clave y asigna el valor a la variable
                if (registroEstudiantes.TryGetValue(id, out Estudiante estudiante))
                {
                    // Llenar los campos con los datos encontrados
                    txtNombre.Text = estudiante.Nombre;
                    txtCarrera.Text = estudiante.Carrera;
                    txtPromedio.Text = estudiante.Promedio.ToString("F2");

                    // Resaltar la fila en el DataGridView
                    foreach (DataGridViewRow row in dgvEstudiantes.Rows)
                    {
                        if (row.Cells["ID"].Value.ToString() == id.ToString())
                        {
                            row.Selected = true;
                            dgvEstudiantes.FirstDisplayedScrollingRowIndex = row.Index;
                            break;
                        }
                    }

                    MessageBox.Show($"✓ Estudiante encontrado:\n\n{estudiante}",
                        "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"❌ No se encontró ningún estudiante con ID {id}",
                        "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese un ID numérico válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== BOTÓN ACTUALIZAR ====================
        // Demuestra: Acceso directo [key] - Modificación en O(1)
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Por favor ingrese el ID del estudiante a actualizar",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                // Verificar existencia en O(1)
                if (!registroEstudiantes.ContainsKey(id))
                {
                    MessageBox.Show($"No existe un estudiante con ID {id}",
                        "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCarrera.Text) ||
                    string.IsNullOrWhiteSpace(txtPromedio.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double promedio = double.Parse(txtPromedio.Text);

                if (promedio < 0 || promedio > 10)
                {
                    MessageBox.Show("El promedio debe estar entre 0 y 10", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Actualizar datos usando acceso directo - Operación O(1)
                registroEstudiantes[id].Nombre = txtNombre.Text;
                registroEstudiantes[id].Carrera = txtCarrera.Text;
                registroEstudiantes[id].Promedio = promedio;

                ActualizarDataGridView();
                LimpiarCampos();

                MessageBox.Show("✓ Datos actualizados exitosamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese valores válidos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== BOTÓN ELIMINAR ====================
        // Demuestra: Remove() - Eliminación en O(1)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Por favor ingrese el ID del estudiante a eliminar",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                if (!registroEstudiantes.ContainsKey(id))
                {
                    MessageBox.Show($"No existe un estudiante con ID {id}",
                        "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirmar eliminación
                DialogResult resultado = MessageBox.Show(
                    $"¿Está seguro de eliminar al estudiante {registroEstudiantes[id].Nombre}?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    // Eliminar del Dictionary - Operación O(1)
                    registroEstudiantes.Remove(id);
                    ActualizarDataGridView();
                    LimpiarCampos();

                    MessageBox.Show("✓ Estudiante eliminado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese un ID numérico válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== BOTÓN LIMPIAR ====================
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // Limpiar los campos del formulario
        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtCarrera.Clear();
            txtPromedio.Clear();
            txtBuscar.Clear();
            txtID.Focus();
        }

        // ==================== BOTÓN ESTADÍSTICAS ====================
        // Demuestra: Uso de LINQ con Dictionary.Values
        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            if (registroEstudiantes.Count == 0)
            {
                MessageBox.Show("No hay estudiantes registrados", "Sin datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Usar LINQ con Dictionary.Values para cálculos
            double promedioGeneral = registroEstudiantes.Values.Average(est => est.Promedio);
            double promedioMasAlto = registroEstudiantes.Values.Max(est => est.Promedio);
            double promedioMasBajo = registroEstudiantes.Values.Min(est => est.Promedio);

            // Contar estudiantes por carrera usando otro Dictionary
            Dictionary<string, int> contadorCarreras = new Dictionary<string, int>();
            foreach (var estudiante in registroEstudiantes.Values)
            {
                if (contadorCarreras.ContainsKey(estudiante.Carrera))
                {
                    contadorCarreras[estudiante.Carrera]++;
                }
                else
                {
                    contadorCarreras[estudiante.Carrera] = 1;
                }
            }

            // Construir mensaje de estadísticas
            string mensaje = "📊 ESTADÍSTICAS GENERALES\n\n";
            mensaje += $"Total de estudiantes: {registroEstudiantes.Count}\n";
            mensaje += $"Promedio general: {promedioGeneral:F2}\n";
            mensaje += $"Promedio más alto: {promedioMasAlto:F2}\n";
            mensaje += $"Promedio más bajo: {promedioMasBajo:F2}\n\n";
            mensaje += "📚 ESTUDIANTES POR CARRERA:\n";

            foreach (var par in contadorCarreras)
            {
                mensaje += $"• {par.Key}: {par.Value} estudiante(s)\n";
            }

            MessageBox.Show(mensaje, "Estadísticas",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento al hacer clic en una celda del DataGridView
        private void dgvEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEstudiantes.Rows[e.RowIndex];
                txtID.Text = row.Cells["ID"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtCarrera.Text = row.Cells["Carrera"].Value.ToString();
                txtPromedio.Text = row.Cells["Promedio"].Value.ToString();
            }
        }

        // ==================== BÚSQUEDA EN TIEMPO REAL ====================
        // Demuestra: Filtrado usando LINQ sobre Dictionary
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text.ToLower();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                ActualizarDataGridView();
                return;
            }

            dgvEstudiantes.Rows.Clear();

            // Filtrar usando LINQ sobre Dictionary.Values
            var resultados = registroEstudiantes.Values
                .Where(est => est.ID.ToString().Contains(busqueda) ||
                              est.Nombre.ToLower().Contains(busqueda))
                .ToList();

            foreach (var est in resultados)
            {
                dgvEstudiantes.Rows.Add(
                    est.ID,
                    est.Nombre,
                    est.Carrera,
                    est.Promedio.ToString("F2"),
                    est.FechaRegistro.ToString("dd/MM/yyyy HH:mm")
                );
            }

            lblTotal.Text = $"Resultados encontrados: {resultados.Count}";
        }
    }
}
