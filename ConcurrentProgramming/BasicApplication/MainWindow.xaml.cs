using System.Windows;

namespace BasicApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World!");
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}