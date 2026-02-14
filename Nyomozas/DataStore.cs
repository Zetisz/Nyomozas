using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyomozas
{
    internal class DataStore
    {
        private List<User> felhasznalok;
        private List<Case> ugyek;
        private List<Person> szemelyek;
        private List<Evidence> bizonyitekok;

        public DataStore()
        {
            felhasznalok = [];
            ugyek = [];
            szemelyek = [];
            bizonyitekok = [];
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
    }
}
