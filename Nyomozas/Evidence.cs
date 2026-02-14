namespace Nyomozas
{
    internal class Evidence
    {
        private string azonosito;
        private string tipus;
        private string leiras;
        private string megbizhatosag;

        public Evidence(string azonosito, string tipus, string leiras, string megbizhatosag)
        {
            this.azonosito = azonosito;
            this.tipus = tipus;
            this.leiras = leiras;
            this.megbizhatosag = megbizhatosag;
        }

        public string Azonosito { get => azonosito; set => azonosito = value; }
        public string Tipus { get => tipus; set => tipus = value; }
        public string Leiras { get => leiras; set => leiras = value; }
        public string Megbizhatosag { get => megbizhatosag; set => megbizhatosag = value; }

        public override string ToString()
        {
            return $"{azonosito}: {tipus} -- {leiras} :: {megbizhatosag}";
        }
    }
}
