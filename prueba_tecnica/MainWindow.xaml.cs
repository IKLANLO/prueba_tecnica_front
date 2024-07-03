using System.Windows;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
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
        private const string url = "http://localhost:8081";
        public MainWindow()
        {
            InitializeComponent();
        }


        private async void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            string text = TextInput.Text;
            if (!string.IsNullOrEmpty(text))
            {
                await SaveNote(text);
            }
        }

        private async Task SaveNote(string text)
        {
            try
            {
                var nota = new StringContent($"{{\"Notas\": \"{text}\"}}", Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{url}/save", nota);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Nota guardada correctamente");
                }
                else
                {
                    MessageBox.Show($"Error al guardar nota: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}