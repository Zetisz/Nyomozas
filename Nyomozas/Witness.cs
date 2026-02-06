using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyomozas
{
    internal class Witness
    {
        private Person szemely;
        private string vallomas;
        private string datum;

        public Witness(Person szemely, string vallomas, string datum)
        {
            this.szemely = szemely;
            this.vallomas = vallomas;
            this.datum = datum;
        }

        public string Vallomas { get => vallomas; set => vallomas = value; }
        public string Datum { get => datum; set => datum = value; }
        internal Person Szemely { get => szemely; set => szemely = value; }

        public override string ToString()
        {
            return $"{this.szemely}\n\t{this.vallomas} ({this.datum})";
        }
    }
}
