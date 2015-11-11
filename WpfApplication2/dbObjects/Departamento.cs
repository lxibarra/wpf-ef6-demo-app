using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.dbObjects
{
    public class Departamento
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; }

    }
}
