using System.Windows;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Controls;

namespace prueba_tecnica
{
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
                TextInput.Text = string.Empty;
            }   
        }

        private async void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            await DeleteNote();
            LabelContainer.Children.Clear();
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
                Label newLabel = new Label { 
                    Content = new TextBlock
                    {
                        Text = item,
                        MaxWidth = 600,
                        VerticalAlignment = VerticalAlignment.Top,
                        TextWrapping = TextWrapping.Wrap,

                    }, 
                    Margin = new Thickness(1) 
                };
                LabelContainer.Children.Add(newLabel);

            }

        }
        
    }
}