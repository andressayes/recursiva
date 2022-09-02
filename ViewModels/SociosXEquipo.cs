using SpApp.src.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpApp.ViewModels
{
    internal class SociosXEquipo
    {
        public string Equipo  { get; set; }
        public List<int> Socios { get; set; }

        public SociosXEquipo(string equipo )
        {
            Equipo = equipo;
            Socios = new List<int>();
        }   
    }
}
