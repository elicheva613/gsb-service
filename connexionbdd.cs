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
    class connexionbdd
    {

        // propriétés
        private bool finCurseur = true; // fin du curseur atteinte
        private MySqlConnection connection; // chaine de connexion
        private MySqlCommand command; // envoi de la requête à la base de données
        private MySqlDataReader reader; // gestion du curseur

        // constructeur
        public connexionbdd(string chaineConnection)
        {
            this.connection = new MySqlConnection(chaineConnection);
            this.connection.Open();
        }

        // execution d'une requete select
        public void reqSelect(string chaineRequete)
        {
            this.command = new MySqlCommand(chaineRequete, this.connection);
            this.reader = this.command.ExecuteReader();
            this.finCurseur = false;
            this.suivant();
        }

        // execution d'une requete update
        public void reqUpdate(string chaineRequete)
        {
            this.command = new MySqlCommand(chaineRequete, this.connection);
            this.command.ExecuteNonQuery();
            this.finCurseur = true;
        }

        // récupération d'un champ
        public Object champ(string nomChamp)
        {
            return this.reader[nomChamp];
        }

        // passage à la ligne suivante du curseur
        public void suivant()
        {
            if (!this.finCurseur)
            {
                this.finCurseur = !this.reader.Read();
            }
        }

        // test de la fin du curseur
        public Boolean fin()
        {
            return this.finCurseur;
        }

        // fermeture de la connexion
        public void close()
        {
            this.connection.Close();

        }
    }
}
  


  