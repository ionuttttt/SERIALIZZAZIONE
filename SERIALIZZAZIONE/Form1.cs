using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Drawing.Drawing2D;
using System.Collections;

namespace SERIALIZZAZIONE
{
    public partial class Form1 : Form
    {
        // Lista che contiene tutti gli utenti caricati o aggiunti
        private List<Utenti> listUtenti;
        private string file = "./file.txt";

        public Form1()
        {
            InitializeComponent();
            listUtenti = new List<Utenti>();
            deserializza(); // Carica gli utenti salvati dal file di testo
        }

        // Metodo per deserializzare gli utenti dal file di testo 
        private void deserializza()
        {
            string line;
            StreamReader sr = new StreamReader(file); // Apre il file per lettura
            // Legge ogni riga del file fino alla fine
            while ((line = sr.ReadLine()) != null)
            {
                // Divide la riga in nome e password, separati da un punto e virgola
                string[] parti = line.Split(';');
                if (parti.Length == 2)
                {
                    string nome = parti[0];
                    string password = parti[1];

                    // Crea un nuovo oggetto Utenti e lo aggiunge alla lista
                    Utenti nuovoUtente = new Utenti(nome, password);
                    listUtenti.Add(nuovoUtente);
                }
            }
            sr.Close(); // Chiude lo stream di lettura
        }

        // Metodo per serializzare gli utenti nel file di testo 
        private void serializza()
        {
            StreamWriter sw = new StreamWriter(file); // Apre il file per scrittura

            // Scrive ogni utente nella lista nel formato "nome;password"
            foreach (Utenti utente in listUtenti)
            {
                sw.WriteLine($"{utente.Nome};{utente.Password}");
            }
            sw.Close(); // Chiude lo stream di scrittura
        }

        // Classe Utenti
        class Utenti
        {
            public string Nome { get; set; }
            public string Password { get; set; }

            // Costruttore della classe Utenti 
            public Utenti(string nome, string password)
            {
                Nome = nome;
                Password = password;
            }
        }

        // Aggiunta nuovo utente
        private void button1_Click(object sender, EventArgs e)
        {
            // Legge i valori dai TextBox per il nome e la password
            string _nome = textBox1.Text;
            string _password = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();

            // Verifica se i campi sono vuoti
            if (_nome == "" || _password == "")
            {
                MessageBox.Show("Compila tutti i campi prima di continuare");
            }
            else
            {
                // Crea un nuovo utente e lo aggiunge alla lista e alla ListBox
                Utenti utente = new Utenti(_nome, _password);
                listUtenti.Add(utente);

                listBox1.Items.Add(utente.Nome + "; " + utente.Password);
            }
        }

        // Serializzazione
        private void button2_Click(object sender, EventArgs e)
        {
            serializza(); // Salva tutti gli utenti su file
        }

        // Leggi
        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // Pulisce la ListBox
            // Aggiunge tutti gli utenti dalla lista alla ListBox
            foreach (Utenti utente in listUtenti)
            {
                listBox1.Items.Add($"{utente.Nome}; {utente.Password}");
            }
        }

        // Cancella utente
        private void button4_Click(object sender, EventArgs e)
        {
            // Legge i valori dai TextBox per nome e password da cancellare
            string cancellaNome = textBox1.Text;
            string cancellaPassword = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();

            // Verifica se i campi sono vuoti
            if (cancellaNome == "" || cancellaPassword == "")
            {
                MessageBox.Show("Compila tutti i campi prima di continuare");
            }
            else
            {
                bool trovato = false;

                // Cerca nella lista l'utente da cancellare
                for (int i = 0; i < listUtenti.Count; i++)
                {
                    if (listUtenti[i].Nome == cancellaNome && listUtenti[i].Password == cancellaPassword)
                    {
                        listUtenti.RemoveAt(i); // Rimuove l'utente dalla lista
                        trovato = true;
                        break;
                    }
                }

                if (trovato)
                {
                    MessageBox.Show("Utente cancellato con successo!");
                    serializza(); // Salva le modifiche 
                }
                else
                {
                    MessageBox.Show("Utente non trovato.");
                }
            }
        }

        // Modifica utente
        private void button5_Click(object sender, EventArgs e)
        {
            // Legge i valori dai TextBox 
            string vecchioNome = textBox1.Text;
            string vecchiaPassword = textBox2.Text;
            string nuovoNome = textBox4.Text;
            string nuovaPassword = textBox3.Text;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            // Verifica se i campi sono vuoti
            if (vecchioNome == "" || vecchiaPassword == "" || nuovoNome == "" || nuovaPassword == "")
            {
                MessageBox.Show("Compila tutti i campi prima di continuare");
            }
            else
            {
                bool trovato = false;

                // Cerca l'utente da modificare nella lista
                for (int i = 0; i < listUtenti.Count; i++)
                {
                    if (listUtenti[i].Nome == vecchioNome && listUtenti[i].Password == vecchiaPassword)
                    {
                        // Modifica il nome e la password dell'utente
                        listUtenti[i].Nome = nuovoNome;
                        listUtenti[i].Password = nuovaPassword;

                        trovato = true;
                        break;
                    }
                }

                if (trovato)
                {
                    MessageBox.Show("Utente modificato con successo!");
                    serializza(); // Salva le modifiche 
                }
                else
                {
                    MessageBox.Show("Utente non trovato.");
                }
            }
        }
    }
}
