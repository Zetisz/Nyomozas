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
        internal Person Szemely { get => szemely; set => szemely = value; }

        public override string ToString()
        {
            return $"{szemely}, vallomas: {vallomas} ({datum})";
        }
    }
}
