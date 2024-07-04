using System.Windows;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Controls;
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

        private async void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            await DeleteNote();
        }

        private async void ReadNoteButton_Click(object sender, RoutedEventArgs e)
        {
            var notes = await ReadNote();
            RenderNotes(notes);
            
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

        private async Task DeleteNote()
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"{url}/delete");
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Notas eliminadas correctamente");
                }
                else
                {
                    MessageBox.Show($"Error al eliminar las notas: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<List<String>> ReadNote()
        {
            try
            {
                ///var notes = await client.GetStringAsync($"{url}/read");
                ///return notes;
                return await client.GetFromJsonAsync<List<String>>($"{url}/read");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<string>();
            }
        }

        private void RenderNotes(List<String> notes)
        {
            LabelContainer.Children.Clear();
            foreach (var item in notes)
            {
                Label newLabel = new Label { Content = item, Margin = new Thickness(1) };
                LabelContainer.Children.Add(newLabel);

            }

        }
        
    }
}