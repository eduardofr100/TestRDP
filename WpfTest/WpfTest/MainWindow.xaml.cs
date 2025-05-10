using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfTest.Models.Dtos;
using WpfTest.Models.Entities;
using WpfTest.Services;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PersonaService _personaService = new PersonaService();
        private ObservableCollection<Persona> Personas { get; } = new ObservableCollection<Persona>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            var persona = (sender as System.Windows.Controls.Button)?.DataContext as Persona;
            if (persona != null)
            {
                try
                {
                    bool success = await _personaService.DeletePersonaAsync(persona.Identificacion);

                    if (success)
                    {
                        MessageBox.Show($"{persona.Nombre} eliminado correctamente");
                        await LoadPersonasAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private async void btnAgregarPersona_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == string.Empty)
            {
                MessageBox.Show("El campo nombre es requerido", "Warning");
                return;
            }
            if (txtApPaterno.Text == string.Empty)
            {
                MessageBox.Show("El campo apellido paterno es requerido", "Warning");
                return;
            }
            if (txtIdentificador.Text == string.Empty)
            {
                MessageBox.Show("El campo identificador es requerido", "Warning");
                return;
            }
            try
            {
                PersonaDto persona = new PersonaDto();
                persona.Nombre = txtNombre.Text;
                persona.ApellidoPaterno = txtApPaterno.Text;
                persona.ApellidoMaterno = txtApMaterno.Text;
                persona.Identificacion = txtIdentificador.Text;

                bool success = await _personaService.CreatePersonaAsync(persona);
                if (success) 
                {
                    MessageBox.Show("La persona a sido registrada correctamente", "Success");
                    txtNombre.Text = string.Empty;
                    txtApPaterno.Text = string.Empty;
                    txtApMaterno.Text = string.Empty;
                    txtIdentificador.Text = string.Empty;
                    await LoadPersonasAsync();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("La persona no se pudo guardar, Error: " + ex.Message, "Error");
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadPersonasAsync();
        }

        private async Task LoadPersonasAsync()
        {
            var personas = await _personaService.GetPersonasAsync();
            dgPersonas.ItemsSource = personas;
        }

        private void btnFacturas_Click(object sender, RoutedEventArgs e)
        {
            Facturas facturas = new Facturas();
            facturas.Show();
        }

        private async void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = txtBuscar.Text;
            if (!string.IsNullOrEmpty(search))
            {
                try
                {
                    var personas = await _personaService.SearchPersonasAsync(search);
                    dgPersonas.ItemsSource = personas;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar: {ex.Message}");
                }
            }
            else
            {
                // Recargar todas las personas o limpiar el DataGrid
                await LoadPersonasAsync();
            }
        }
    }
}