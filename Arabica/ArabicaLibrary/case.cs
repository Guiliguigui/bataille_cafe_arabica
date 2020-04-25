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

        public int X { get => x;}
        public int Y { get => y;}
        public Parcelle Parcelle { get => parcelle;}
        public IA Proprietaire { get => proprietaire;}

        public void Planter(IA planteur)
        {
            if (this.proprietaire != null)
                throw new Exception();

            this.proprietaire = planteur;
            this.parcelle.NouvelleCasePlantee(planteur);
        }
    }
}
