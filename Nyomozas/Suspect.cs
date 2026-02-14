namespace Nyomozas
{
    internal class Suspect
    {
        private Person szemely;
        private string szint;
        private string statusz;

        public Suspect(Person szemely, string szint, string statusz)
        {
            this.szemely = szemely;
            this.szint = szint;
            this.statusz = statusz;
        }

        public string Szint { get => szint; set => szint = value; }
        public string Statusz { get => statusz; set => statusz = value; }
        internal Person Szemely { get => szemely; set => szemely = value; }

        public override string ToString()
        {
            return $"{szemely}\n\t{szint} -- {statusz}";
        }
    }
}
