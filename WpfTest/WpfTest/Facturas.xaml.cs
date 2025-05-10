using System.Collections.ObjectModel;
using System.Windows;
using WpfTest.Models.Dtos;
using WpfTest.Models.Entities;
using WpfTest.Services;

namespace WpfTest
{
    /// <summary>
    /// Lógica de interacción para Facturas.xaml
    /// </summary>
    public partial class Facturas : Window
    {
        private readonly FacturaService _facturaService = new FacturaService();
        private ObservableCollection<Factura> Facturass { get; } = new ObservableCollection<Factura>();
        private readonly PersonaService _personaService = new PersonaService();
        private ObservableCollection<Persona> Personas { get; } = new ObservableCollection<Persona>();

        public Facturas()
        {
            InitializeComponent();
        }

        private async Task LoadFacturasAsync()
        {
            var facturas = await _facturaService.GetFacturasAsync();
            dgFacturas.ItemsSource = facturas;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFacturasAsync();
            await LlenarCombo();
        }

        private async void txtBuscarFactura_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var search = txtBuscarFactura.Text;
            if (!string.IsNullOrEmpty(search))
            {
                try
                {
                    var facturas = await _facturaService.GetByFacturaAsync(int.Parse(search));
                    dgFacturas.ItemsSource = facturas;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar: {ex.Message}");
                }
            }
            else
            {
                await LoadFacturasAsync();
            }
        }

        private async void btnAgregarFactura_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FacturaDto factura = new FacturaDto();
                //factura.Fecha = dpFecha.SelectedDate?.ToString("yyyy-MM-dd");
                factura.Fecha = DateOnly.FromDateTime(dpFecha.SelectedDate.Value);
                factura.Monto = decimal.Parse(txtMonto.Text);
                factura.PersonaId = (int)cbIdPersona.SelectedValue;

                bool success = await _facturaService.CreateFacturaAsync(factura);
                if (success)
                {
                    MessageBox.Show("La persona a sido registrada correctamente", "Success");
                    dpFecha.SelectedDate = null;
                    txtMonto.Text = string.Empty;
                    cbIdPersona.SelectedValue = null;
                    await LoadFacturasAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("La persona no se pudo guardar, Error: " + ex.Message, "Error");
            }
        }

        public async Task LlenarCombo()
        {
            var personas = await _personaService.GetPersonasAsync();
            cbIdPersona.ItemsSource = personas.Select(p => new Persona
            {
                IdPersona = p.IdPersona,
                Nombre = $"{p.Nombre} {p.ApellidoPaterno} {p.ApellidoMaterno}"
            }).ToList();
            cbIdPersona.DisplayMemberPath = "Nombre";
            cbIdPersona.SelectedValuePath = "IdPersona";
        }
    }
}
