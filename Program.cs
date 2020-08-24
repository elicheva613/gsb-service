using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.Sql;
using System;
using System.Timers;

namespace PPE_gestion_cloture
{
  

    class Program
    {
        private static System.Timers.Timer aTimer;
        static void Main(string[] args)
        {

            SetTimer();
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();

            Console.WriteLine("Terminating the application...");
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {

            //Connexion à la bdd et création du curseur:
            connexionbdd crs = new connexionbdd("SERVER=127.0.0.1; DATABASE=gsb_frais; UID=root; PASSWORD=");

            //On vérifie qu'on est bien entre le 1 et le 10 du mois:
            if (Gestion_Date.Entre(1, 15) == true)
            {

                //Récupération des fiches du mois précédent et maj de celles-ci:
                //Récupération du mois précédent et son année
                String moisPrecedent = Gestion_Date.GetMoisPrecedent();
                Console.WriteLine(moisPrecedent);
                String annee = Gestion_Date.getAnnee(DateTime.Today);
                //  string annee = DateTime.Today.AddMonths(-1).ToString("yyyy");
                string mois = annee + moisPrecedent;
                crs.reqUpdate("update fichefrais set idetat='CL' where mois =" + mois + " and idetat='CR'");
            }
            //Si on est après le 20 du mois:
            if (Gestion_Date.Entre(20, 31) == true)
            {
                ;
                //Récupération des fiches du mois précédent et maj de celles-ci:
                String moisPrecedent = Gestion_Date.GetMoisPrecedent();
                String annee = Gestion_Date.getAnnee(DateTime.Today);
                //string annee = DateTime.Today.AddMonths(-1).ToString("yyyy");
                string mois = annee + moisPrecedent;

                crs.reqUpdate("update fichefrais set idetat='RB' where mois = " + mois + " and idetat='VA'");

            }
        }


    }
}
