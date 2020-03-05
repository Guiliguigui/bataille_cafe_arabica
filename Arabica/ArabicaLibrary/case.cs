using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    public class Case
    {
        private byte x, y;
        private char parcelle;
        private bool plantee;
        private IA proprietaire;

        public Case(byte x, byte y, char parcelle)
        {
            this.x = x;
            this.y = y;
            this.parcelle = parcelle;
            plantee = false;
            proprietaire = null;
        }

        public byte X { get => x; set => x = value; }
        public byte Y { get => y; set => y = value; }
        public char Parcelle { get => parcelle; set => parcelle = value; }
        public bool Plantee { get => plantee; set => plantee = value; }
        public IA Proprietaire { get => proprietaire; set => proprietaire = value; }

        public void planter(IA proprietaire)
        {
            plantee = true;
            this.proprietaire = proprietaire;
        }
    }
}
