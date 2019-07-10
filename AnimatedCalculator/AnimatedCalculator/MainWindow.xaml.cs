using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnimatedCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal? firstNumber = null, secondNumber = null, result = null;
        char operation;
        /// <summary>
        /// <para isOperationDone> returns false when secondNumber isn't entered yet </para>
        /// <para operationBtnClicked> to prevent consecutive operationBtn clicking without secondNumber entry </para>
        /// </summary>
        Boolean isInitialInput = true, isOperationDone = false, operationBtnClicked = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void NumberBtns_Click(object sender, RoutedEventArgs e)
        {
            string btn = (sender as System.Windows.Controls.Button).Content.ToString();
            if (isInitialInput || resultTxt.Text == "0")
            {
                resultTxt=null;
                isInitialInput = false;
            }
            resultTxt.Text = btn;
            operationBtnClicked = false;
        }

        private void Do_Operation()
        {
            switch (operation)
            {
                case '+':
                    result = firstNumber + secondNumber;
                    break;
                case '-':
                    result = firstNumber - secondNumber;
                    break;
                case '*':
                    result = firstNumber * secondNumber;
                    break;
                case '/':
                    try
                    {
                        result = firstNumber / secondNumber;
                    }
                    catch
                    {
                        MessageBox.Show("Can't divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case '%':
                    secondNumber /= 100;
                    break;
                case '±':
                    secondNumber *= (-1);
                    break;
            }
        }

        public void OperationBtns_Click(object sender, RoutedEventArgs e)
        {
            string btn = (sender as System.Windows.Controls.Button).Content.ToString();
            isInitialInput = true;
            if (operationBtnClicked)
            {
                operation = char.Parse(btn);
                return;
            }
            if (isOperationDone)
            {
                if (secondNumber != null)
                {
                    firstNumber = result;
                }
                secondNumber = decimal.Parse(resultTxt.Text);
                Do_Operation();
                operation = char.Parse(btn);
                resultTxt.Text = result.ToString();
            }
            else
            {
                firstNumber = decimal.Parse(resultTxt.Text);
                operation = char.Parse(btn);
                isOperationDone = true;
            }
            operationBtnClicked = true;
        }

        public void EqualBtn_Click(object sender, RoutedEventArgs e)
        {
            if (secondNumber != null)
                firstNumber = result;
            secondNumber = decimal.Parse(resultTxt.Text);
            Do_Operation();
            if (result != null)
                resultTxt.Text = result.ToString();
            //to start from the beginning
            firstNumber = null;
            secondNumber = null;
            operation = '\0';
            isInitialInput = true;
            operationBtnClicked = false;
            isOperationDone = false;
        }

        public void DecBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            resultTxt.Text = "0";
            calculationsTxt.Text = null;
            firstNumber = null;
            secondNumber = null;
            operation = '\0';
            isInitialInput = true;
            operationBtnClicked = false;
            isOperationDone = false;
        }


    }
}
