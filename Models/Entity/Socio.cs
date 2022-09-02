using System;
using System.Collections.Generic;
using System.Text;

namespace SpApp.src.Models.Entity
{
    internal class Socio
    {
        public string Nombre { get; set; }
        
        public int Edad { get; set; }
        
        public string Equipo { get; set; }

        public string EstadoCivil { get; set; }

        public string Estudios { get; set; }
        public Socio(string nombre, int edad, string equipo, string estadoCivil, string estudios)
        {
            Nombre = nombre;
            Edad = edad;
            Equipo = equipo;
            EstadoCivil = estadoCivil;
            Estudios = estudios;
        }   
    }
}
