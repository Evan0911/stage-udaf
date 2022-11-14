using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetUDAF
{
    class bdd
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;

        public static void Initialize()
        {
            server = "127.0.0.1";
            database = "udaf";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        #region Co&Déco
        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                Console.WriteLine("Erreur connexion BDD");
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

        #region Article
        public static List<Article> SelectArticle()
        {
            //Select statement
            string query = "SELECT * FROM Article order by id desc";

            //Create a list to store the result
            List<Article> dbArticle = new List<Article>();

            //Ouverture connection
            if (bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Article leArticle = new Article(Convert.ToInt32(dataReader["id"]), dataReader["auteur"].ToString(), dataReader["titre"].ToString(), dataReader["contenu"].ToString(), Convert.ToDateTime(dataReader["date_creation"]));
                    dbArticle.Add(leArticle);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbArticle;
            }
            else
            {
                return dbArticle;
            }
        }
        public static Article SearchArticle(int numA)
        {
            //Select statement
            string query = "SELECT * FROM Article WHERE id = " + numA;

            //Create a list to store the result
            List<Article> dbArticle = new List<Article>();

            if (bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Article leArt = new Article(Convert.ToInt32(dataReaderS["id"]), dataReaderS["auteur"].ToString(), dataReaderS["titre"].ToString(), dataReaderS["contenu"].ToString(), Convert.ToDateTime(dataReaderS["date_creation"]));
                    dbArticle.Add(leArt);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbArticle[0];
            }
            else
            {
                return new Article(1, "", "", "", new DateTime());
            }
        }
        public static Article GetLastArticle()
        {
            //Select statement
            string query = "SELECT * FROM article where id = (select max(id) from article)";

            //Create a list to store the result
            List<Article> dbArticle = new List<Article>();

            if (bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Article leArt = new Article(Convert.ToInt32(dataReader["id"]), dataReader["auteur"].ToString(), dataReader["titre"].ToString(), dataReader["contenu"].ToString(), Convert.ToDateTime(dataReader["date_creation"]));
                    dbArticle.Add(leArt);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbArticle[0];
            }
            else
            {
                return new Article(1, "", "", "", new DateTime());
            }
        }
        #endregion

        #region Raccourci
        public static List<Raccourci> SelectRaccourci()
        {
            //Select statement
            string query = "SELECT * FROM raccourci inner join metier on idMetier=metier.id";

            //Create a list to store the result
            List<Raccourci> dbRaccourci = new List<Raccourci>();

            //Ouverture connection
            if (bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Raccourci leRaccourci = new Raccourci(Convert.ToInt16(dataReader["id"]), Convert.ToString(dataReader["nom"]), Convert.ToString(dataReader["chemin"]), Convert.ToString(dataReader["image"]), Convert.ToString(dataReader["libelle"]), Convert.ToInt32(dataReader["colonne"]), Convert.ToInt32(dataReader["ligne"]));
                    dbRaccourci.Add(leRaccourci);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbRaccourci;
            }
            else
            {
                return dbRaccourci;
            }
        }
        public static Raccourci SearchRaccourci(int id)
        {
            //Select statement
            string query = "SELECT * FROM raccourci inner join metier on idMetier=metier.id WHERE raccourci.id = " + id;

            //Create a list to store the result
            List<Raccourci> dbRaccourci = new List<Raccourci>();

            bdd.OpenConnection();

            //Creation Command MySQL
            MySqlCommand cmdS = new MySqlCommand(query, connection);
            //Création d'un DataReader et execution de la commande
            MySqlDataReader dataReaderS = cmdS.ExecuteReader();

            //Lecture des données et stockage dans la collection
            while (dataReaderS.Read())
            {
                Raccourci leRaccourci = new Raccourci(Convert.ToInt16(dataReaderS["id"]), Convert.ToString(dataReaderS["nom"]), Convert.ToString(dataReaderS["chemin"]), Convert.ToString(dataReaderS["image"]), Convert.ToString(dataReaderS["libelle"]), Convert.ToInt32(dataReaderS["colonne"]), Convert.ToInt32(dataReaderS["ligne"]));
                dbRaccourci.Add(leRaccourci);
            }

            //fermeture du Data Reader
            dataReaderS.Close();

            bdd.CloseConnection();

            //retour de la collection pour être affichée
            return dbRaccourci[0];
        }
        #endregion
    }
}
