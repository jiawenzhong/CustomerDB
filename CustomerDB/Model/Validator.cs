using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace CustomerDB.Model
{
    public class Validator
    {
        private static string title = "Entry Error";

        public static string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public static bool IsPresent(string textBox)
        {
            if (textBox == null)
            {
                MessageBox.Show("Incomplete.", Title);
                //textBox.Focus();
                return false;
            }

            return true;
        }

        public static bool isDecimal(string textBox)
        {
            decimal number = 0m;
            if (Decimal.TryParse(textBox, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox + " must be a decimal value.", Title);
                //textBox.Focus();
                return false;
            }
        }

        public static bool IsInt32(string textBox)
        {
            int number = 0;
            if (Int32.TryParse(textBox, out number))
            {
                return true;
            }
            else
            {
                MessageBox.Show(textBox + " must be an integer.", Title);
                //textBox.Focus();
                return false;
            }
        }

        public static bool IsWithinRange(string textBox, decimal min, decimal max)
        {
            //decimal number = Convert.ToDecimal(textBox.Text);

            int number = textBox.Length;

            if (number < min || number > max)
            {
                MessageBox.Show(textBox + " must be between " + min
                + " and " + max + ".", Title);
                //textBox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsValidEmail(string textBox)
        {
            if (textBox.IndexOf("@") == -1 ||
            textBox.IndexOf(".") == -1)
            {
                MessageBox.Show(textBox + " must be a valid email address.",
                Title);
                //textBox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
