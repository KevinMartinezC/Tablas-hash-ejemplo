using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Tablas_hash_ejemplo
{
    internal class Estudiante
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Carrera { get; set; }
        public double Promedio { get; set; }
        public DateTime FechaRegistro { get; set; }

        public Estudiante(int id, string nombre, string carrera, double promedio)
        {
            ID = id;
            Nombre = nombre;
            Carrera = carrera;
            Promedio = promedio;
            FechaRegistro = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{ID} - {Nombre} ({Carrera}) - Promedio: {Promedio:F2}";
        }
    }
}
