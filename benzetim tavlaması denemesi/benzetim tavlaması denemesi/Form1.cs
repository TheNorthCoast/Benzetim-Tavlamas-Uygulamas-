using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace benzetim_tavlaması_denemesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[,] veri = new double[,]
            {
                { 0, 0.75, 1.3, 1.5, 1.7, 1.4, 1.6, 2.9, 2.3 },
                { 0.75, 0, 1, 0.75, 0.7, 0.65, 0.9, 2.1, 2.1 },
                { 1.3, 1, 0, 0.3, 0.55, 0.9, 1.1, 2.3, 2.1 },
                { 1.5, 0.75, 0.3, 0, 0.25, 0.7, 0.95, 2.1, 1.9 },
                { 1.7, 0.7, 0.55, 0.25, 0, 0.7, 0.8, 2.1, 1.9 },
                { 1.4, 0.65, 0.9, 0.7, 0.7, 0, 0.22, 1.6, 1.3 },
                { 1.6, 0.9, 1.1, 0.95, 0.8, 0.22, 0, 1.4, 1.3 },
                { 2.9, 2.1, 2.3, 2.1, 2.1, 1.6, 1.4, 0, 1 },
                { 2.3, 2.1, 2.1, 1.9, 1.9, 1.3, 1.3, 1, 0 }
            };

            string[] parametre = textBox1.Text.Split(',');
            if (parametre.Length != 3)
            {
                MessageBox.Show("Aralarında virgül olacak şekilde sıcaklık, soğuma derecesi ve iterasyon sayısını giriniz.");
                return;
            }

            if (!double.TryParse(parametre[0], out double t) ||
                !double.TryParse(parametre[1], out double r) ||
                !int.TryParse(parametre[2], out int maxiterasyon))
            {
                MessageBox.Show("Geçersiz sayı. Sayıları tekrar yazınız.");
                return;
            }

            StringBuilder result = new StringBuilder();
                Random rand = new Random();
            int[] baslangicrotasi = new int[] { 1, 7, 5, 2, 6, 4, 3, 8, 9 };
            double fitness = 0;
                for (int rota = 0; rota < 5; rota++)
                {
                    
                    for (int i = 0; i < baslangicrotasi.Length - 1; i++)
                    {
                        fitness += veri[baslangicrotasi[i] - 1, baslangicrotasi[i + 1] - 1];
                    }
                    fitness += veri[baslangicrotasi[baslangicrotasi.Length - 1] - 1, baslangicrotasi[0] - 1];
                double yenisicaklik = t;  
                double normalsicaklik = t; 

                for (int iterasyon = 0; iterasyon < maxiterasyon; iterasyon++)
            {

                    int[] yenirota = (int[])baslangicrotasi.Clone();

                    int daire;
                    do
                    {
                        daire = rand.Next(1, yenirota.Length - 2);
                    } while (daire == 0 || daire >= yenirota.Length - 2);

                    (yenirota[daire], yenirota[daire + 1]) = (yenirota[daire + 1], yenirota[daire]);

                    double yeniFitness = 0;
                    for (int i = 0; i < yenirota.Length - 1; i++)
                    {
                        yeniFitness += veri[yenirota[i] - 1, yenirota[i + 1] - 1];
                    }
                    yeniFitness += veri[yenirota[yenirota.Length - 1] - 1, yenirota[0] - 1];

                    if (yeniFitness < fitness)
                    {
                        baslangicrotasi = yenirota;
                        fitness = yeniFitness;
                    }
                    else if (rand.NextDouble() < Math.Exp((fitness - yeniFitness) / normalsicaklik))
                    {
                        baslangicrotasi = yenirota;
                        fitness = yeniFitness;
                    }


                    for (int i = 0; i < yenirota.Length - 1; i++)
                {
                    yeniFitness += veri[yenirota[i] - 1, yenirota[i + 1] - 1];
                }
                yeniFitness += veri[yenirota[yenirota.Length - 1] - 1, yenirota[0] - 1];

                if (yeniFitness < fitness)
                {
                    baslangicrotasi = yenirota;
                    fitness = yeniFitness;
                }
                    else if (rand.NextDouble() < Math.Exp((fitness - yeniFitness) / normalsicaklik))
                    {
                        baslangicrotasi = yenirota;
                        fitness = yeniFitness;
                    }

                    normalsicaklik *= r;
                   
                }

            string Rota = string.Join(" -> ", baslangicrotasi.Select(x => x.ToString()).ToArray());

                result.AppendLine($"Run {rota + 1}:");
                result.AppendLine($"En Iyi Rota: {Rota}");
                result.AppendLine($"Fitness: {fitness}");
                result.AppendLine($"Sicaklik Uzunluğu: {yenisicaklik}");
                result.AppendLine($"Baslangic Sicakligi: {r}");
                result.AppendLine($"Max Iterasyon: {maxiterasyon}");
                result.AppendLine();
            }

            MessageBox.Show(result.ToString());
        }
    }
}
