using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculate_kon
{
    public partial class Кулькулятор : Form
    {
        bool znak = false;
        public Кулькулятор()
        {
            InitializeComponent();
        }
        private void Number_click(string s)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = s;
            }
            else
            {
                textBox1.Text = textBox1.Text + s;
            }

            znak = false;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Number_click("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Number_click("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Number_click("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Number_click("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Number_click("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Number_click("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Number_click("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Number_click("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Number_click("9");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            Number_click("0");
        }
        private void Clear()
        {
            textBox1.Text = "0";
            znak = false;
        }

        private void buttonUdalit_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text;
            int lenght = textBox1.Text.Length - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            if (lenght == 0)
            {
                textBox1.Text = "0";
            }
            else
            {
                for (int i = 0; i < lenght; i++)
                {
                    textBox1.Text = textBox1.Text + text[i];
                    if (Char.IsDigit(text[lenght - 1]))
                        znak = true;
                    else
                        znak = false;
                }
            }
            znak = false;
        }

        private void Pow_Root(string s)
        {
            textBox1.Text = textBox1.Text + s;
            znak = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Znak_click(string s)
        {
            if (znak == false)
            {
                textBox1.Text = textBox1.Text + s;
                znak = true;
            }
            else
            {
                int lenght = textBox1.Text.Length - 1;
                string text = textBox1.Text;
                textBox1.Clear();

                for (int i = 0; i < lenght; i++)
                {
                    textBox1.Text = textBox1.Text + text[i];
                }

                textBox1.Text = textBox1.Text + s;
            }
        }

        private void buttonZapyataya_Click(object sender, EventArgs e)
        {
            Znak_click(",");
        }

        private void buttonPlas_Click(object sender, EventArgs e)
        {
            Znak_click("+");
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            Znak_click("-");
        }

        private void buttonUmnojit_Click(object sender, EventArgs e)
        {
            Znak_click("*");
        }

        private void buttonPodelit_Click(object sender, EventArgs e)
        {
            Znak_click("/");
        }

        private void buttonKvadrat_Click(object sender, EventArgs e)
        {

        }

       
        private void buttonKvadratY_Click(object sender, EventArgs e)
        {
            Znak_click("^");
        }

        private void buttonCorenX_Click(object sender, EventArgs e)
        {
            Znak_click("√");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Pow_Root("(");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Pow_Root(")");
        }
        private String Calculate(String input)
        {
            String output = GetExpression(input);
            String result = Counting(output);
            return result;
        }

        static private string GetExpression(string input)
        {
            string output = string.Empty;
            Stack<char> operStack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (IsDelimeter(input[i]))
                {
                    continue;
                }

                if (Char.IsDigit(input[i]))
                {
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i];
                        i++;

                        if (i == input.Length) break;
                    }

                    output += " ";
                    i--;
                }

                if (IsOperator(input[i]))
                {
                    if (input[i] == '(')
                        operStack.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else
                    {
                        if (operStack.Count > 0)
                            if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
                                output += operStack.Pop().ToString() + " ";

                        operStack.Push(char.Parse(input[i].ToString()));
                    }
                }
            }

            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output;
        }

        static private string Counting(string input)
        {
            try
            {
                double result = 0;
                Stack<double> temp = new Stack<double>();

                for (int i = 0; i < input.Length; i++)
                {

                    if (Char.IsDigit(input[i]))
                    {
                        string a = string.Empty;

                        while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                        {
                            a += input[i];
                            i++;
                            if (i == input.Length) break;
                        }
                        temp.Push(double.Parse(a));
                        i--;
                    }
                    else if (IsOperator(input[i]))
                    {
                        double a = temp.Pop();
                        double b = temp.Pop();

                        switch (input[i])
                        {
                            case '+': result = b + a; break;
                            case '-': result = b - a; break;
                            case '*': result = b * a; break;
                            case '/': result = b / a; break;
                            case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                            case '√': result = double.Parse(Math.Pow(double.Parse(a.ToString()), 1 / double.Parse(b.ToString())).ToString()); break;
                        }
                        temp.Push(result);
                    }
                }
                return temp.Peek().ToString();
            }
            catch
            {
                return "Ошибка ввода";
            }
        }

        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }

        static private bool IsOperator(char с)
        {
            if (("+-/*^()√".IndexOf(с) != -1))
                return true;
            return false;
        }

        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                case '√': return 6;
                default: return 7;
            }
        }

        private void buttonRavno2_Click(object sender, EventArgs e)
        {
            textBox1.Text = this.Calculate(textBox1.Text);
        }

        private void buttonRavno1_Click(object sender, EventArgs e)
        {
            textBox1.Text = this.Calculate(textBox1.Text);
        }
    }
}
