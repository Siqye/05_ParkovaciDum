using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace _05_ParkovaciDum
{
    class Parkoviste
    {

        static public int DefaultniPocetMist = 30;

        static public int DefaultniPocetPovrhu = 2;

        private int m_pocetPovrhu;
        private int m_pocetMistNaPovrh;
        private bool[,] m_obsazenostMist;

        private List<Vozidlo> m_zaparkovanaVozidla;

        private void initializaceMist(int mista, int patra)
        {
            for (int m = 0; m < mista;m++) {
                for (int p = 0; p < patra; p++) {
                    m_obsazenostMist[m, p] = false;
                }
            }
        }

        public Parkoviste(int pocetPorvrhu, int pocetMistNaPovrh) {
            m_pocetMistNaPovrh = pocetMistNaPovrh;
            m_pocetPovrhu = pocetPorvrhu;
            m_obsazenostMist = new bool[pocetMistNaPovrh, pocetPorvrhu];
            initializaceMist(pocetPorvrhu, pocetMistNaPovrh);
        }

        public Parkoviste() {
            m_pocetMistNaPovrh = DefaultniPocetMist;
            m_pocetPovrhu = DefaultniPocetPovrhu;
            m_obsazenostMist = new bool[DefaultniPocetMist, DefaultniPocetPovrhu];
            initializaceMist(DefaultniPocetMist, DefaultniPocetPovrhu);
        }

        public void zaparkuj(Vozidlo vozidlo) {
            if (vozidlo.SPZ == null)
                return;

            for (int m = 0; m < m_pocetMistNaPovrh; m++)
            {
                for (int p = 0; p < m_pocetPovrhu; p++)
                {
                    if (m_obsazenostMist[m, p])
                        continue;
                    else
                    {
                        vozidlo.JeZaparkovano = true;
                        vozidlo.ParkovaciMisto = m;
                        vozidlo.ParkovaciPatro = p;
                        m_zaparkovanaVozidla.Add(vozidlo);
                        m_obsazenostMist[m, p] = true;
                    }
                }
            }
        }

        public void zaparkuj(Vozidlo vozidlo, int patro) {
            if (vozidlo.SPZ == null)
                return;

            if (patro > m_pocetPovrhu) return;
            for (int m = 0;m < m_pocetMistNaPovrh;m++)
            {
                if (m_obsazenostMist[m, patro])
                    continue;
                else
                {
                    vozidlo.JeZaparkovano = true;
                    vozidlo.ParkovaciMisto = m;
                    vozidlo.ParkovaciPatro = patro;
                    m_zaparkovanaVozidla.Add(vozidlo);
                    m_obsazenostMist[m, patro] = true;
                }
            }
        }

        public void zaparkuj(Vozidlo vozidlo, int patro, int misto)
        {
            if (vozidlo.SPZ == null)
                return;

            if (m_obsazenostMist[patro, misto])
            {
                Console.WriteLine
                    ($"Nepodarilo se zapakovat auto na misto {misto} na patre {patro}, je obsazeno");
                return;
            }
            m_obsazenostMist[patro, misto] = true;
        }

        public void odjed(Vozidlo vozidlo) {
            if (vozidlo.SPZ == null) 
                return;
            if (!vozidlo.JeZaparkovano)
            {
                Console.WriteLine("Voizdlo neni zaparkovano");
                return;
            }
            if (m_obsazenostMist[vozidlo.ParkovaciMisto, vozidlo.ParkovaciPatro])
            {
                m_obsazenostMist[vozidlo.ParkovaciMisto, vozidlo.ParkovaciPatro] = false;
                vozidlo.JeZaparkovano = false;
                vozidlo.ParkovaciPatro = -1;
                vozidlo.ParkovaciMisto = -1;
            }
        }

        private Vozidlo vyhledVozdila(int misto, int patro)
        {
            foreach (Vozidlo v in m_zaparkovanaVozidla)
            {
                if (v.ParkovaciMisto == misto || v.ParkovaciPatro == patro) return v;
            }
            return null;
        }

        public void odjed(int misto, int patro)
        {
            if (misto > m_pocetMistNaPovrh && misto < 0) 
                return;
            if (patro > m_pocetPovrhu && patro < 0)
                return;

            if (!m_obsazenostMist[misto, patro])
            {
                Vozidlo auto = vyhledVozdila(misto, patro);
                m_zaparkovanaVozidla.Remove(auto);
                auto.JeZaparkovano = false;
                auto.ParkovaciMisto = -1;
                auto.ParkovaciPatro = -1;
            } else
            {
                Console.WriteLine("na tom miste neni zaparkovano zadne vozidlo");
            }
        }
    }
}
