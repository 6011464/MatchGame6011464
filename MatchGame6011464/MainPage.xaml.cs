namespace MatchGame6011464
{
    public partial class MainPage : ContentPage
    {
        int matchesFound = 0;
        Button lastButtonClicked;
        bool findingMatch = false;
        DateTime startTime;
        bool gameEnded = false;

        public MainPage()
        {
            InitializeComponent();
            SetUpGame(); // Configurar el juego al inicializar la página
            startTime = DateTime.Now;

            // Iniciar temporizador para mostrar el tiempo transcurrido
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!gameEnded)
                {
                    var elapsedTime = DateTime.Now - startTime;
                    lblTimer.Text = $"Time: {elapsedTime:mm\\:ss}";
                    return true; // Continuar el temporizador
                }
                return false; // Detener el temporizador cuando el juego termine
            });
        }

        // Método para configurar las formas aleatorias en los botones
        private void SetUpGame()
        {
            // Lista de formas disponibles
            List<string> shapes = new List<string>()
    {
        "🔷", "✨", "❤", "🎶", "🤳", "▲", "🎁", "□",
        "🔷", "✨", "❤", "🎶", "🤳", "▲", "🎁", "□",
    };

            Random random = new Random();
            // Asignar formas aleatorias a los botones en el Grid
            foreach (var child in Grid1.Children)
            {
                if (child is Button button)
                {
                    int index = random.Next(shapes.Count);
                    string nextShape = shapes[index];
                    button.Text = nextShape;
                    shapes.RemoveAt(index);
                    button.IsVisible = true; // Asegurar que los botones sean visibles
                }
            }
        }

        // Evento de clic en un botón
        private async void Button_Clicked(object sender, EventArgs e)
        {
            // Verificar si el juego ha terminado y reiniciar si es necesario
            if (gameEnded)
            {
                SetUpGame(); // Reiniciar el juego
                startTime = DateTime.Now;
                lblPrincipal.Text = "Match Game Indel 6011464";
                matchesFound = 0;
                gameEnded = false;
            }

            // Verificar si ya se está buscando una coincidencia y el juego no ha terminado
            if (!findingMatch && !gameEnded)
            {
                if (sender is Button button)
                {
                    button.IsVisible = false; // Ocultar el botón seleccionado
                    lastButtonClicked = button; // Guardar el último botón seleccionado
                    findingMatch = true; // Indicar que se está buscando una coincidencia
                }
            }
            else if (!gameEnded)
            {
                if (sender is Button button)
                {
                    if (button.Text == lastButtonClicked.Text) // Si hay una coincidencia
                    {
                        button.IsVisible = false; // Ocultar el botón seleccionado
                        matchesFound++; // Incrementar el número de coincidencias encontradas
                        if (matchesFound == 8) // Si se encuentran todas las coincidencias
                        {
                            var elapsedTime = DateTime.Now - startTime;
                            await DisplayAlert("Congratulations!", $"You've won in {elapsedTime:mm\\:ss}!", "OK");
                            gameEnded = true; // Indicar que el juego ha terminado
                        }
                    }
                    else // Si no hay coincidencia
                    {
                        await System.Threading.Tasks.Task.Delay(1000); // Esperar un segundo
                        lastButtonClicked.IsVisible = true; // Mostrar el último botón seleccionado
                    }
                    findingMatch = false; // Restablecer el estado de búsqueda
                }
            }
        }

        // Método para reiniciar el juego cuando se presiona el botón de reinicio
        private void RestartButton_Clicked(object sender, EventArgs e)
        {
            gameEnded = false; // Reiniciar el estado del juego
            matchesFound = 0; // Reiniciar el contador de coincidencias
            SetUpGame(); // Reiniciar el juego
            startTime = DateTime.Now; // Reiniciar el tiempo transcurrido
            lblPrincipal.Text = "Match Game Indel 6011464"; // Restablecer el título
        }
    }
}