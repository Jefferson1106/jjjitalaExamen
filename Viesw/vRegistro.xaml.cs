using System.Globalization;

namespace jjjitalaExamen.Viesw;

public partial class vRegistro : ContentPage
{
    string usuarioConectado;

    public vRegistro(string usuario)
    {
        InitializeComponent();
        usuarioConectado = usuario;
        lblUsuarioConectado.Text = "Usuario conectado: " + usuario;
        dpFechaInicio.MinimumDate = DateTime.Today;
        dpFechaInicio.Date = DateTime.Today;
    }

    private decimal ValidarYCalcular()
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            throw new ArgumentException("Ingrese nombre y txtApellido.");

        int anios;
        if (!int.TryParse(txtEdad.Text, out anios) || anios < 16)
            throw new ArgumentException("La edad debe ser un número entero de al menos 16 años.");

        if (dpFechaInicio.Date == null || dpFechaInicio.Date.Value.Date < DateTime.Today)
            throw new ArgumentException("La fecha de inicio no puede ser anterior a hoy.");

        if (pkPlan.SelectedIndex < 0)
            throw new ArgumentException("Seleccione un pkPlan.");

        string monto = txtMontoInicial.Text?.Trim().Replace(',', '.') ?? "";
        decimal montoInicial;
        if (!decimal.TryParse(monto, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out montoInicial)
            || montoInicial < 120 || montoInicial > 600)
            throw new ArgumentException("El monto inicial debe estar entre 120 y 600.");

        decimal costoAnual = 1200;
        decimal pagoMensual = (costoAnual - montoInicial) / 5 + costoAnual * 0.02m;
        if (swCasillero.IsToggled)
            pagoMensual = pagoMensual + 10;

        return pagoMensual;
    }

    private async void btnCalcular_Clicked(object? sender, EventArgs e)
    {
        txtPagoMensual.Text = string.Empty;
        try
        {
            decimal pagoMensual = ValidarYCalcular();
            txtPagoMensual.Text = "$" + pagoMensual.ToString("0.00", CultureInfo.InvariantCulture);
        }
        catch (ArgumentException error)
        {
            await DisplayAlertAsync("Validación", error.Message, "Aceptar");
        }
    }

    private async void btnVerResumen_Clicked(object? sender, EventArgs e)
    {
        txtPagoMensual.Text = string.Empty;
        try
        {
            decimal pagoMensual = ValidarYCalcular();
            txtPagoMensual.Text = "$" + pagoMensual.ToString("0.00", CultureInfo.InvariantCulture);
            int edadSocio = Convert.ToInt32(txtEdad.Text);
            decimal montoInicial = decimal.Parse(txtMontoInicial.Text.Trim().Replace(',', '.'), CultureInfo.InvariantCulture);
            DateTime fechaInicio = dpFechaInicio.Date!.Value;
            string planSeleccionado = pkPlan.SelectedItem.ToString()!;

            await Navigation.PushAsync(new vResumen(usuarioConectado, txtNombre.Text.Trim(), txtApellido.Text.Trim(),
                edadSocio, fechaInicio, planSeleccionado, swCasillero.IsToggled, montoInicial, pagoMensual));
        }
        catch (ArgumentException error)
        {
            await DisplayAlertAsync("Validación", error.Message, "Aceptar");
        }
    }

    private void txtMontoInicial_TextChanged(object? sender, TextChangedEventArgs e)
    {
        txtPagoMensual.Text = string.Empty;
    }

    private void swCasillero_Toggled(object? sender, ToggledEventArgs e)
    {
        txtPagoMensual.Text = string.Empty;
    }
}

