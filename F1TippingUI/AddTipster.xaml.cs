using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using F1Tipping;

namespace F1TippingUI
{
    /// <summary>
    /// Interaction logic for AddTipster.xaml
    /// </summary>
    public partial class AddTipster : Window
    {
        public AddTipster()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Tipster tipster = new Tipster()
            {
                FirstName = this.firstName.Text,
                Surname = this.Surname.Text,
                emailAddress = this.Email.Text,
                InitialScore = Int32.Parse(this.InitialScore.Text)
            };

            (Application.Current as App).Comp.Tipsters.Add(tipster);

            this.Close();
        }
    }
}
