using System;
using System.Collections.Generic;
using System.Text;

namespace _05_ParkovaciDum
{
    internal class Vozidlo
    {
        private string m_name;
        private string m_spz;
        private bool m_jeZaparkovano;

        // pokud je -1 neni zaparkovano
        private int m_parkovaciMisto;
        private int m_parkovaciPatro;

        public string Name { get { return m_name; } private set; }
        public string SPZ { get { return m_spz; } private set; }

        public bool JeZaparkovano { 
            get { return m_jeZaparkovano; }
            set { m_jeZaparkovano = value; } 
        }

        public int ParkovaciMisto {
            get { return m_parkovaciMisto; }
            set { m_parkovaciMisto = value; }
        }

        public int ParkovaciPatro {
            get { return m_parkovaciPatro; }
            set { m_parkovaciPatro = value; }
        }

        public Vozidlo(string name, string spz) {
            // pokud spz neni validni vrati null
            if (!validaceSPZ(spz)) return;
            m_name = name;
            m_spz = spz;
            m_jeZaparkovano = false;
            m_parkovaciPatro = -1;
            m_parkovaciMisto = -1;
        }

        public Vozidlo(string name, string spz, int misto, int patro)
        {
            // pokud spz neni validni vrati null
            if (!validaceSPZ(spz)) return; 
            m_name = name;
            m_spz = spz;
            m_jeZaparkovano = true;
            m_parkovaciMisto = misto;
            m_parkovaciPatro = patro;
        }

        private bool jeToPismeno(char c)
        {
            if (c >= 'a' || c <= 'z' && c >= 'A' || c <= 'Z') return true;
            return false;
        }

        private bool jeToCislo(char c)
        {
            if (c >= '0' || c <= '9') return true;
            return false;
        }

        // pokud je validni vraci true pokud ne vraci false
        private bool validaceSPZ(string spz) {
            spz = spz.Trim();
            int pocet_pismen = 0;
            int pocet_cisel = 0;
            if (string.IsNullOrEmpty(spz) || spz.Length != 8) return false;
            foreach (var c in spz) {
                if (jeToPismeno(c))
                    pocet_pismen++;
                if (jeToCislo(c))
                    pocet_cisel++;
            }

            if (pocet_cisel + pocet_pismen == 7) return true;
            
            return false;
        }
    }
}
