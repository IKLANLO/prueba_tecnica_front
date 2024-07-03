using System.Windows;
using System.Text;
using System.Net.Http;
///using System.Threading.Tasks;

/* using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes; */

namespace prueba_tecnica
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly HttpClient client = new HttpClient();
        private const string url = "http://localhost:8081/";
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string text = TextInput.Text;
            if (!string.IsNullOrEmpty(text))
            {
                await SaveNote(text); ///falta crear el método para llamar al endpoint de guardado
            }
        }
    }
}