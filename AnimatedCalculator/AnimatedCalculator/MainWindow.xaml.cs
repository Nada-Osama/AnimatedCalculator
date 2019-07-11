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
        char operationSymbol;
        String operation;
        /// <summary>
        /// <para isOperationDone> returns false when secondNumber isn't entered yet </para>
        /// <para operationBtnClicked> to prevent consecutive operationBtn clicking without secondNumber entry </para>
        /// </summary>
        Boolean isInitialInput = true, isOperationDone = false, operationBtnClicked = false,
            isPlusMinus_Or_Percent_BtnsClicked = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void NumberBtns_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string numberKind = ((MaterialDesignThemes.Wpf.PackIcon)(btn.Content)).Kind.ToString();
            int numberKindSize = numberKind.Length;
            if (resultTxt.Text == "0" || isInitialInput)
            {
                resultTxt.Text = String.Empty;
                isInitialInput = false;
            }
            resultTxt.Text += (numberKind[numberKindSize - 1]).ToString();

            //to prevent display resultTxt text in calculationTxt when first entry of number
            if (isInitialInput)
                calculationsTxt.Text += resultTxt.Text; 

            operationBtnClicked = false;
        }

        private void Do_Operation()
        {
            switch (operation)
            {
                case "Plus":
                    {
                        result = firstNumber + secondNumber;
                    }
                    break;
                case "Minus":
                    {
                        result = firstNumber - secondNumber;
                    }
                    break;
                case "Multiplication":
                    {
                        result = firstNumber * secondNumber;
                    }
                    break;
                case "Division":
                    try
                    {
                        result = firstNumber / secondNumber;
                    }
                    catch
                    {
                        MessageBox.Show("Can't divide by zero!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
            }
        }

        public void OperationBtns_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string operationKind = ((MaterialDesignThemes.Wpf.PackIcon)(btn.Content)).Kind.ToString();
            isInitialInput = true;
            if (operationBtnClicked)
            {
                operation = operationKind;
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
                operation = operationKind;
                if (!isPlusMinus_Or_Percent_BtnsClicked)
                    calculationsTxt.Text += resultTxt.Text;
                resultTxt.Text = result.ToString();

                OperationSymbolPrint();
                calculationsTxt.Text += operationSymbol.ToString();
            }
            else
            {
                firstNumber = decimal.Parse(resultTxt.Text);
                operation = operationKind;

                //to display number beside operation sign when first input
                if (isInitialInput)
                    calculationsTxt.Text += resultTxt.Text;

                //to print opteration sign in calculationTxt when clicked
                OperationSymbolPrint();
                calculationsTxt.Text += operationSymbol.ToString();

                isOperationDone = true;
            }
            operationBtnClicked = true;
        }

        private void OperationSymbolPrint()
        {
            switch (operation)
            {
                case "Plus":
                    operationSymbol = '+';
                    break;
                case "Minus":
                    operationSymbol = '-';
                    break;
                case "Multiplication":
                    operationSymbol = '*';
                    break;
                case "Division":
                     operationSymbol = '÷';
                    break;
            }
        }

        public void EqualBtn_Click(object sender, RoutedEventArgs e)
        {
            if (secondNumber != null)
                firstNumber = result;
            secondNumber = decimal.Parse(resultTxt.Text);
            Do_Operation();
            if (result != null)
            {
                /// <summary>
                /// <para last> gets last number before equalClick to display it in historyTxt </para>
                /// </summary>
                string last = resultTxt.Text;
                resultTxt.Text = result.ToString();
                historyTxt.Text = calculationsTxt.Text + last + " = " + resultTxt.Text;
            }
            //to start from the beginning
            firstNumber = null;
            secondNumber = null;
            operation = null;
            isInitialInput = true;
            operationBtnClicked = false;
            isOperationDone = false;
            calculationsTxt.Text = String.Empty;
        }

        public void DecBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (isInitialInput)
            {
                isInitialInput = false;
                resultTxt.Text = "0.";
                return;
            }
            if (resultTxt.Text.Contains("."))
                return;
            isInitialInput = false;
            resultTxt.Text += btn.Content;
        }

        public void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            resultTxt.Text = "0";
            calculationsTxt.Text = String.Empty;
            firstNumber = null;
            secondNumber = null;
            operation = null;
            isInitialInput = true;
            operationBtnClicked = false;
            isOperationDone = false;
        }

        public void PlusMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            decimal negateNumber = decimal.Parse(resultTxt.Text) * -1;
            resultTxt.Text = negateNumber.ToString();
            //calculationsTxt.Text += resultTxt.Text;
            isInitialInput = false;
            isPlusMinus_Or_Percent_BtnsClicked = true;
        }

        public void PercentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isInitialInput || !isOperationDone)
            {
                resultTxt.Text = "0";
                calculationsTxt.Text = "0";
            }
            //calculationsTxt.Text = String.Empty;
            decimal PercentedNumber = decimal.Parse(resultTxt.Text) / 100;
            resultTxt.Text = PercentedNumber.ToString();
            calculationsTxt.Text += resultTxt.Text; 
            isInitialInput = false;
            isPlusMinus_Or_Percent_BtnsClicked = true;
        }
    }
}
