using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyomozas
{
    internal class TimelineEvent
    {
        private string datum;
        private string leiras;

        public TimelineEvent(string datum, string leiras)
        {
            this.datum = datum;
            this.leiras = leiras;
        }

        public string Datum { get => datum; set => datum = value; }
        public string Leiras { get => leiras; set => leiras = value; }

        public override string ToString()
        {
            return $"{this.datum} -- {this.leiras}";
        }
    }
}
