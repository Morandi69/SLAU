using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace СЛАУ
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        double MatrixNorm(double [,] Matrix, int length)
        {
            double [] sum = new double[length];
            double strsumm=0;
            for (int i = 0; i < length-1; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    strsumm+= Math.Abs(Matrix[i, j]);
                }
                sum[i] = strsumm;
                strsumm = 0;
            }
            return Enumerable.Max(sum);
        }
   
        


        double[,] A1= { { 5,  2, -1},
                        {-4,  7, 3 },
                        { 2, -2, 4} };

        double[] F1 = {12,
                      24,
                      9};


        double [,] A2 = { { 10, 15, -1},
                          {1, 10, -1},
                          {-1, 1, 10} };
        double [] F2= {11,
                       10,
                       10};

        double[,] A = {  { 5, 10, 1, 1},
                          {1, 5, 1, 1},
                          {1, 1, 5, 1},
                          {1, 1, 1, 5} };
        double[] F = {12,
                      11,
                      13,
                      14};


        double[] Yakoby(double[,] A, double[]F)
        {
            bool exit = true;
            double esp = 0.001;
            int n = F.Length;
            double[,] B = new double[n, n];
            double[] D = new double[n];
            double[] x = new double[n];
            double[] xn = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    B[i, j] = -A[i, j] / A[i, i];
                }
            }
            for (int i = 0; i < n; i++)
            {
                D[i] = F[i] / A[i, i];
            }
            if (MatrixNorm(B, n) > 1)
            {
                MessageBox.Show("Норма матрицы больше 1, Матрица не сходится");
                return xn;
            }
            if (MatrixNorm(B, n) >= 0.5 && MatrixNorm(B, n) != 0 && MatrixNorm(B, n) != 1)
            {
                double e1 = Math.Abs((1 - MatrixNorm(B, n)) / (MatrixNorm(B, n))) * esp;
                esp = e1;
            }
            do
            {
                for (int i = 0; i < n; i++)
                {
                    x[i] = xn[i];
                }
                for (int i = 0; i < n; i++)
                {
                    xn[i] = 0;
                }

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }
                        xn[i] += B[i, j] * x[j];
                    }
                    xn[i] += D[i];
                }


                if (n == 2)
                {
                    if (Math.Abs(xn[0] - x[0]) < esp && Math.Abs(xn[1] - x[1]) < esp)
                    {
                        exit = false;
                    }
                }
                if (n == 3)
                {
                    if (Math.Abs(xn[0] - x[0]) < esp && Math.Abs(xn[1] - x[1]) < esp && Math.Abs(xn[2] - x[2]) < esp)
                    {
                        exit = false;
                    }
                }
                if (n == 4)
                {
                    if (Math.Abs(xn[0] - x[0]) < esp && Math.Abs(xn[1] - x[1]) < esp && Math.Abs(xn[2] - x[2]) < esp && Math.Abs(xn[3] - x[3]) < esp)
                    {
                        exit = false;
                    }
                }
            } while (exit);
            return xn;

        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] x = Yakoby(A2, F2);
            Label[] labels = { label2, label3, label4, label5 };
            for (int i = 0; i < x.Length; i++)
            {
                labels[i].Text = x[i].ToString();
            }
        }
    }
}
