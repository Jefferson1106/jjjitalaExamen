namespace jjjitalaExamen.Viesw;

public partial class vLogin : ContentPage
{
    public vLogin()
    {
        InitializeComponent();
    }

    private async void btnIniciarSesion_Clicked(object? sender, EventArgs e)
    {
        string[,] usuarios =
        {
            { "recepcion", "gym2026" },
            { "entrenador", "fit_01" },
            { "uisrael", "2026" },
            { "gerente", "admin123" }
        };

        for (int i = 0; i < usuarios.GetLength(0); i++)
        {
            if (txtUsuario.Text?.Trim() == usuarios[i, 0] && txtContrasena.Text == usuarios[i, 1])
            {
                txtContrasena.Text = string.Empty;
                await Navigation.PushAsync(new vRegistro(usuarios[i, 0]));
                return;
            }
        }
        await DisplayAlertAsync("Login", "Usuario o contraseña incorrectos.", "Aceptar");
    }

    private async void btnAcercaDe_Clicked(object? sender, EventArgs e)
    {
        await DisplayAlertAsync("Acerca de", "desarrolado por : Jefferson jitala", "Aceptar");
    }
}

