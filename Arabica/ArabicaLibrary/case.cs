using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    public class Case
    {
        private int x, y;
        private Parcelle parcelle;
        private IA proprietaire;

        public Case(int x, int y, Parcelle parcelle)
        {
            this.x = x;
            this.y = y;
            this.parcelle = parcelle;
            proprietaire = null;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public Parcelle Parcelle { get => parcelle; set => parcelle = value; }
        public IA Proprietaire { get => proprietaire; set => proprietaire = value; }

        public void planter(IA proprietaire)
        {
            this.proprietaire = proprietaire;
        }
    }
}
