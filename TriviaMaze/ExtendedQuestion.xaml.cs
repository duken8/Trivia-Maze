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
using System.Windows.Shapes;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for ExtendedQuestion.xaml
    /// </summary>
    public partial class ExtendedQuestion : Window
    {
        private string correctAnswer;
        public bool result { get; private set; }
        private bool submitClicked = false;

        public ExtendedQuestion()
        {
            InitializeComponent();
        }

        public ExtendedQuestion(string prompt, string keyword)
        {
            Console.Out.WriteLine("Prompt is: " + prompt);
            InitializeComponent();
            result = false;
            PromptLabel.Text = prompt;
            correctAnswer = keyword;
        }
        

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if(InputBox.Text.ToUpper().Contains(correctAnswer.ToUpper()))
            {
                MessageBox.Show("Correct! You can now advance!", "Well Done!", MessageBoxButton.OK);
                result = true;
                submitClicked = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect! You have now locked that gate!", "Unlucky!", MessageBoxButton.OK);
                result = false;
                submitClicked = true;
                this.Close();
            }
        }

        private void On_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (submitClicked == false)
            {
                e.Cancel = true;
            }
        }
    }
}
