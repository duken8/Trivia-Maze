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
    /// Interaction logic for BasicQuestion.xaml
    /// </summary>
    public partial class BasicQuestion : Window
    {
        String correctAnswer;
        public Boolean result = false;
        public BasicQuestion()
        {
            InitializeComponent();
        }

        //Constructor for true false questions
        public BasicQuestion(String prompt, Boolean answer)
        {
            Answer3.Visibility = Visibility.Hidden;
            Answer4.Visibility = Visibility.Hidden;
            Answer1.Content = "True";
            Answer2.Content = "False";
            PromptLabel.Content = prompt;
            correctAnswer = answer.ToString();
        }

        //Constructor for multiple choice questions
        public BasicQuestion(String[] data)//Prompt,RealAnswer,Wrong1,Wrong2,Wrong3
        {
            PromptLabel.Content = data[0];
            correctAnswer = data[1];
            ConfigureRadioButtons(data);
        }

        private void ConfigureRadioButtons(String[] data)
        {
            Random rnd = new Random();
            int rand = rnd.Next(0, 4);
            switch(rand)
            {
                case 0:
                    Answer1.Content = data[0];
                    Answer2.Content = data[1];
                    Answer3.Content = data[2];
                    Answer4.Content = data[3];
                    break;
                case 1:
                    Answer1.Content = data[1];
                    Answer2.Content = data[3];
                    Answer3.Content = data[2];
                    Answer4.Content = data[0];
                    break;
                case 2:
                    Answer1.Content = data[2];
                    Answer2.Content = data[3];
                    Answer3.Content = data[1];
                    Answer4.Content = data[0];
                    break;
                case 3:
                    Answer1.Content = data[1];
                    Answer2.Content = data[0];
                    Answer3.Content = data[3];
                    Answer4.Content = data[2];
                    break;
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if((Boolean)Answer1.IsChecked)
            {
                if(Answer1.Content.Equals(correctAnswer))
                {
                    MessageBox.Show("Correct! You can now advance!", "Well Done!", MessageBoxButton.OK);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Incorrect! You have now locked that gate!", "Unlucky!", MessageBoxButton.OK);
                    result = false;
                }
            }
            else if((Boolean)Answer2.IsChecked)
            {
                if(Answer2.Content.Equals(correctAnswer))
                {
                    MessageBox.Show("Correct! You can now advance!", "Well Done!", MessageBoxButton.OK);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Incorrect! You have now locked that gate!", "Unlucky!", MessageBoxButton.OK);
                    result = false;
                }
            }
            else if((Boolean)Answer3.IsChecked)
            {
                if(Answer3.Content.Equals(correctAnswer))
                {
                    MessageBox.Show("Correct! You can now advance!", "Well Done!", MessageBoxButton.OK);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Incorrect! You have now locked that gate!", "Unlucky!", MessageBoxButton.OK);
                    result = false;
                }
            }
            else if((Boolean)Answer4.IsChecked)
            {
                if(Answer4.Content.Equals(correctAnswer))
                {
                    MessageBox.Show("Correct! You can now advance!", "Well Done!", MessageBoxButton.OK);
                    result = true;
                }
                else
                {
                    MessageBox.Show("Incorrect! You have now locked that gate!", "Unlucky!", MessageBoxButton.OK);
                    result = false;
                }
            }
            else
            {
                MessageBox.Show("Please check an answer before hitting the Final Answer button!", "Error", MessageBoxButton.OK);
            }
        }
    }
}