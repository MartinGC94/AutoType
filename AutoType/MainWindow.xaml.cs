using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace AutoType
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool shouldType = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TypeAll_Click(object sender, RoutedEventArgs e)
        {
            BeginTyping(textBox.Text);
        }

        private void TypeSelection_Click(object sender, RoutedEventArgs e)
        {
            BeginTyping(textBox.SelectedText);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            shouldType = false;
        }

        private void BeginTyping(string text)
        {
            shouldType = true;
            _ = ThreadPool.QueueUserWorkItem(TypeText, text);
        }

        private void TypeText(object textToType)
        {
            IEnumerator<string> inputEnumerator = ((string)textToType).GetKeyStrokes().GetEnumerator();
            Thread.Sleep(1000);
            while (shouldType && inputEnumerator.MoveNext())
            {
                SendKeys.SendWait(inputEnumerator.Current);
                Thread.Sleep(40);
            }

            inputEnumerator.Dispose();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.F5:
                    TypeAll_Click(null, null);
                    break;
                case System.Windows.Input.Key.F8:
                    TypeSelection_Click(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}