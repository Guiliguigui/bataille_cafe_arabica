using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    /* Classe Case
     * Cette classe sert à créer une case dynamique et communicante avec la
     * parcelle lui correspondant
     */
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

        /* Planter
         * Permet de planter un grain de café dans la case et d'actualiser le 
         * propriétaire et la parcelle 
         * Input  : l'IA qui a planté la case
         * Output : void
         */
        public void Planter(IA planteur)
        {
            this.proprietaire = planteur;
            this.proprietaire.DerniereCaseJouee = this;
            this.parcelle.NouvelleCasePlantee(planteur);
        }
    }
}
