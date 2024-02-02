using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp18
{
    public partial class Form1 : Form
    {
        MyStack<char> char_stack;
        MyStack<char> CHECK_ON_SKOB;
        Myqueue<string> result;
        MyStack<int> int_stack;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            if (CHECK(textBox1.Text))
            {
                TO_OPZ(textBox1.Text);
                COUNT_OPZ();
                richTextBox1.Text += "Ответ: " + int_stack.Pop() + "\r\n";
            }
            else
            {
                MessageBox.Show(
        "Скобки не совпадают.",
        "Ошибка",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            char_stack = new MyStack<char>(1000);
            CHECK_ON_SKOB = new MyStack<char>(1000);
            result = new Myqueue<string>(1000);
            int_stack = new MyStack<int>(1000);
            
        }
        private bool CHECK(string INPUT_STRING)
        {
            CHECK_ON_SKOB.Clear();
            bool bool1 = true;
            for (int i = 0; i < INPUT_STRING.Length; i++)
            {
                if (INPUT_STRING[i] == '(' || INPUT_STRING[i] == '{' || INPUT_STRING[i] == '[')
                    CHECK_ON_SKOB.Push(INPUT_STRING[i]);
                else if (INPUT_STRING[i] == ')' || INPUT_STRING[i] == '}' || INPUT_STRING[i] == ']')
                {
                    if (CHECK_ON_SKOB.isEmpty())
                    {
                        bool1 = false;
                    }
                    if (!CHECK_ON_SKOB.isEmpty())
                    {
                        switch (INPUT_STRING[i])
                        {
                            case ')':
                                if (CHECK_ON_SKOB.Top() == '(')
                                    CHECK_ON_SKOB.Pop();
                                else
                                {
                                    bool1 = false;
                                }
                                break;
                            case ']':
                                if (CHECK_ON_SKOB.Top() == '[')
                                    CHECK_ON_SKOB.Pop();
                                else
                                {
                                    bool1 = false;
                                }
                                break;
                            case '}':
                                if (CHECK_ON_SKOB.Top() == '{')
                                    CHECK_ON_SKOB.Pop();
                                else
                                {
                                    bool1 = false;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (CHECK_ON_SKOB.isEmpty() && bool1)
                bool1 = true;
            else
                bool1 = false;
            while (!CHECK_ON_SKOB.isEmpty())
            {
                CHECK_ON_SKOB.Pop();
            }
            if (bool1)
                return true;
            return false;
        }
        private int Priority(char Value)
        {
            switch (Value)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                case '-':
                    return 2;
                case '*':
                case '/':
                    return 3;
                case '^':
                    return 4;
                default:
                    return -1;

            }
        }
        private void TO_OPZ(string BASE_STRING)
        {
            char_stack.Clear();
            result.Clear();
            string some_res = "";
            for (int i = 0; i < BASE_STRING.Length; i++)
            {
                switch (BASE_STRING[i])
                {
                    case '(':
                        char_stack.Push(BASE_STRING[i]);
                        break;
                    case ')':
                        {
                            while (char_stack.Top() != '(')
                            {
                                result.Push(Convert.ToString(char_stack.Pop()));
                            }
                            char_stack.Pop();
                            break;
                        }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        if (i + 1 < BASE_STRING.Length && Char.IsDigit(BASE_STRING[i + 1]))
                        {
                            while (i + 1 < BASE_STRING.Length && Char.IsDigit(BASE_STRING[i + 1]))
                            {
                                some_res += BASE_STRING[i];
                                i++;
                            }
                            some_res += BASE_STRING[i];
                        }
                        else
                            some_res += BASE_STRING[i];
                        result.Push(some_res);
                        some_res = "";
                        break;
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                    case '^':
                        if (char_stack.isEmpty())
                        {
                            char_stack.Push(BASE_STRING[i]);
                        }
                        else
                        {
                            while (!char_stack.isEmpty() && Priority(BASE_STRING[i]) <= Priority(char_stack.Top()))
                            {
                                result.Push(Convert.ToString(char_stack.Pop()));
                            }
                            char_stack.Push(BASE_STRING[i]);
                        }
                        break;
                    default:
                        break;
                }
            }
            while (!char_stack.isEmpty())
            {
                result.Push(Convert.ToString(char_stack.Pop()));
            }
        }
        private void COUNT_OPZ()
        {
            richTextBox1.Text += "ОПЗ: ";
            string BASE_CHAR;
            int res1, s1, s2;
            int_stack.Clear();
            while(!result.isEmpty())
            {
                BASE_CHAR = result.Pop();
                richTextBox1.Text += BASE_CHAR + " ";
                if (int.TryParse(BASE_CHAR, out res1))
                {
                    int_stack.Push(res1);
                }
                else
                {
                    switch (Convert.ToChar(BASE_CHAR))
                    {
                        case '+':
                            int_stack.Push(int_stack.Pop() + int_stack.Pop());
                            break;
                        case '-':
                            s1 = int_stack.Pop();
                            s2 = int_stack.Pop();
                            int_stack.Push(s2 - s1);
                            break;
                        case '*':
                            int_stack.Push(int_stack.Pop() * int_stack.Pop());
                            break;
                        case '/':
                            s1 = int_stack.Pop();
                            s2 = int_stack.Pop();
                            int_stack.Push(s2/ s1);
                            break;
                        case '^':
                            s1 = int_stack.Pop();
                            s2 = int_stack.Pop();
                            int_stack.Push(Convert.ToInt32(Math.Pow(s2, s1)));
                            break;
                        default:
                            break;
                    }
                }
            }
            richTextBox1.Text += "\r\n";
        }
    }
    }

