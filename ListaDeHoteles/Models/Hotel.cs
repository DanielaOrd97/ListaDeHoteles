using System;
using System.Collections.Generic;
using System.Text;

namespace ListaDeHoteles.Models
{
    public class Hotel
    {

        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public byte Estrellas { get; set; }
        public byte Pisos { get; set; }
        public int NumeroHabitaciones { get; set; }
        public string Imagen { get; set; } = "";
        public string Descripcion { get; set; }


    }
}
