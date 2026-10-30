using System.Globalization;
using System.Text;

namespace jjjitalaExamen.Viesw;

public partial class vResumen : ContentPage
{
    public vResumen(string usuario, string nombre, string apellido, int edad,
        DateTime fechaInicio, string plan, bool casillero, decimal montoInicial, decimal pagoMensual)
    {
        InitializeComponent();
        lblUsuarioConectado.Text = "Usuario conectado: " + usuario;
        lblNombre.Text = nombre;
        lblApellido.Text = apellido;
        lblEdad.Text = edad.ToString();
        lblFechaInicio.Text = fechaInicio.ToString("dd/MM/yyyy");
        lblPlan.Text = plan;
        if (casillero)
            lblCasillero.Text = "Sí";
        else
            lblCasillero.Text = "No";

        decimal pagoTotal = montoInicial + pagoMensual * 5;
        lblMontoInicial.Text = "$" + montoInicial.ToString("0.00", CultureInfo.InvariantCulture);
        lblPagoMensual.Text = "$" + pagoMensual.ToString("0.00", CultureInfo.InvariantCulture);
        lblPagoTotal.Text = "$" + pagoTotal.ToString("0.00", CultureInfo.InvariantCulture);
    }

    private async void btnCerrarSesion_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }

    private async void btnExportar_Clicked(object? sender, EventArgs e)
    {
        btnExportar.IsEnabled = false;
        try
        {
            string nombreArchivo = "examen_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".txt";
            string ruta = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);
            string salto = Environment.NewLine;
            string texto = "RESUMEN DE SOCIO" + salto + lblUsuarioConectado.Text + salto
                + "Nombre: " + lblNombre.Text + salto
                + "Apellido: " + lblApellido.Text + salto
                + "Edad: " + lblEdad.Text + salto
                + "Fecha de inicio: " + lblFechaInicio.Text + salto
                + "Plan: " + lblPlan.Text + salto
                + "Casillero: " + lblCasillero.Text + salto
                + "Monto inicial: " + lblMontoInicial.Text + salto
                + "Pago mensual: " + lblPagoMensual.Text + salto
                + "Pago total: " + lblPagoTotal.Text;

            await File.WriteAllTextAsync(ruta, texto, Encoding.UTF8);
            await DisplayAlertAsync("Exportación completada", "Archivo de texto guardado en:\n" + ruta, "Aceptar");
            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Exportar resumen del socio",
                File = new ShareFile(ruta, "text/plain")
            });
        }
        catch (Exception error)
        {
            await DisplayAlertAsync("Exportar", "No se pudo completar la exportación: " + error.Message, "Aceptar");
        }
        finally
        {
            btnExportar.IsEnabled = true;
        }
    }
}

