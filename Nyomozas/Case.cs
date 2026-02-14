namespace Nyomozas
{
    internal class Case
    {
        private int ugyazonosito;
        private string cim;
        private string leiras;
        private string allapot;
        private List<Person> szemelyek;
        private List<Evidence> bizonyitekok;

        public Case(int ugyazonosito, string cim, string leiras, string allapot)
        {
            this.ugyazonosito = ugyazonosito;
            this.cim = cim;
            this.leiras = leiras;
            this.allapot = allapot;
            szemelyek = [];
            bizonyitekok = [];
        }

        public int Ugyazonosito { get => ugyazonosito; set => ugyazonosito = value; }
        public string Cim { get => cim; set => cim = value; }
        public string Leiras { get => leiras; set => leiras = value; }
        public string Allapot { get => allapot; set => allapot = value; }

        internal List<Person> Szemelyek { get => szemelyek; }
        internal List<Evidence> Bizonyitekok { get => bizonyitekok; }


        public override string ToString()
        {
            return $"{ugyazonosito}: {cim} -- {leiras} :: {allapot}";
        }
    }
}
