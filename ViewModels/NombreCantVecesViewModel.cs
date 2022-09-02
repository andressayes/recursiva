using System;
using System.Collections.Generic;
using System.Text;

namespace SpApp.ViewModels
{
    internal class NombreCantVecesViewModel
    {
        public string nombre { get; set; }

        public int  cant { get; set; }

        public NombreCantVecesViewModel(string nombre, int cant)
        {
            this.nombre = nombre;
            this.cant = cant;
        }
    }
}
