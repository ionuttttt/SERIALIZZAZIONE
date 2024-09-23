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

        private List<Utenti> listUtenti;
        private string file = "./file.txt";
        public Form1()
        {
            InitializeComponent();
            listUtenti = new List<Utenti>();
            deserializza();
        }

        private void deserializza()
        {
            string line;
            StreamReader sr = new StreamReader(file);
            while ((line = sr.ReadLine()) != null)
            {
                string[] parti = line.Split(';');
                if (parti.Length == 2)
                {
                    string nome = parti[0];
                    string password = parti[1];

                    Utenti nuovoUtente = new Utenti(nome, password);
                    listUtenti.Add(nuovoUtente);
                }
            }
            sr.Close();

        }

        private void serializza()
        {
            StreamWriter sw = new StreamWriter(file);

            foreach (Utenti utente in listUtenti)
            {
                sw.WriteLine($"{utente.Nome};{utente.Password}");
            }
            sw.Close();
        }


        class Utenti
        {
            public string Nome { get; set; }
            public string Password { get; set; }

            public Utenti(string nome, string password)
            {
                Nome = nome;
                Password = password;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string _nome = textBox1.Text;
            string _password = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();

            if (_nome == "" || _password == "")
            {
                MessageBox.Show("Compila tutti i campi prima di continuare");
            }

            else
            {
                Utenti utente = new Utenti(_nome, _password);
                listUtenti.Add(utente);

                listBox1.Items.Add(utente.Nome + "; " + utente.Password);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serializza();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Utenti utente in listUtenti)
            {
                listBox1.Items.Add($"{utente.Nome}; {utente.Password}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string cancellaNome = textBox1.Text;
            string cancellaPassword = textBox2.Text;
            textBox1.Clear();
            textBox2.Clear();

            if (cancellaNome == "" || cancellaPassword == "")
            {
                MessageBox.Show("Compila tutti i campi prima di continuare");
            }

            else
            {

                bool trovato = false;

                for (int i = 0; i < listUtenti.Count; i++)
                {
                    if (listUtenti[i].Nome == cancellaNome && listUtenti[i].Password == cancellaPassword)
                    {
                        listUtenti.RemoveAt(i);

                        trovato = true;
                        break;
                    }
                }


                if (trovato)
                {
                    MessageBox.Show("Utente cancellato con successo!");
                    serializza();
                }

                else
                {
                    MessageBox.Show("Utente non trovato.");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string vecchioNome= textBox1.Text;
            string vecchiaPassword= textBox2.Text;
            string nuovoNome= textBox4.Text;
            string nuovaPassword= textBox3.Text;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            if (vecchioNome == "" || vecchiaPassword == "" || nuovoNome == "" || nuovaPassword == "")
            {
                MessageBox.Show("Compila tutti i campi prima di continuare");
            }

            else
            {

                bool trovato = false;

                for (int i = 0; i < listUtenti.Count; i++)
                {
                    if (listUtenti[i].Nome == vecchioNome && listUtenti[i].Password == vecchiaPassword)
                    {
                        listUtenti[i].Nome = nuovoNome;
                        listUtenti[i].Password = nuovaPassword;

                        trovato = true;
                        break;
                    }
                }

                if (trovato)
                {
                    MessageBox.Show("Utente modificato con successo!");
                    serializza();
                }

                else
                {
                    MessageBox.Show("Utente non trovato.");
                }
            }
        }
    }
}
