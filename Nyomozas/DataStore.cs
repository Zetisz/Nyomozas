namespace Nyomozas
{
    internal class DataStore
    {
        private List<User> felhasznalok;
        private List<Case> ugyek;
        private List<Person> szemelyek;
        private List<Evidence> bizonyitekok;
        private List<Suspect> gyanusitottak;
        private List<Witness> tanuk;

        public DataStore()
        {
            felhasznalok = [];
            ugyek = [];
            szemelyek = [];
            bizonyitekok = [];
            gyanusitottak = [];
            tanuk = [];
        }

        public List<User> Felhasznalok
        {
            get { return felhasznalok; }
        }

        public List<Case> Ugyek
        {
            get { return ugyek; }
        }

        public List<Person> Szemelyek
        {
            get { return szemelyek; }
        }

        public List<Evidence> Bizonyitekok
        {
            get { return bizonyitekok; }
        }

        public List<Suspect> Gyanusitottak
        {
            get { return gyanusitottak; }
        }

        public List<Witness> Tanuk
        {
            get { return tanuk; }
        }
    }
}
