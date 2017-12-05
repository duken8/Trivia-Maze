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
    /// Interaction logic for QuestionForm.xaml
    /// </summary>
    public partial class SimpleQuestion : Window
    {
        Boolean result = false;        
        public SimpleQuestion()
        {
            
        }

        //Constructor for true false questions
        public SimpleQuestion(String [] data, Boolean answer)
        {
            
        }

        public SimpleQuestion(String [] data)
        {

        }
    }
}
