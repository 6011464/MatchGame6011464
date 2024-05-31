namespace MatchGame6011464
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
     

        public MainPage()
        {
            InitializeComponent();
            SetUpGame();
        }



        // Método para configurar el juego
        private void SetUpGame()
        {
            // Lista de emojis de animales
            List<string> animalEmoji = new List<string>()
            {
                "🤔", "🤔",
                "⨊", "⨊",
                "🌹", "🌹",
                "🎁", "🎁",
                "⩙", "⩙",
                "✌", "✌",
                "👌", "👌",
                "⨞", "⨞",
            };

            Random random = new Random(); // Generador aleatorio
            foreach (Button view in Grid1.Children) // Iteractuar a través de los botones en el grid
            {
                int index = random.Next(animalEmoji.Count); // Obtener un índice
                string nextEmoji = animalEmoji[index]; // próximo emoji

                view.Text = nextEmoji; // Establecer el texto del botón
                animalEmoji.RemoveAt(index); // Eliminar el emoji usado de la lista
            }
        }

        Button ultimoButtonCliked; // Último botón clicado
        bool encontrandoMatch = false; //  seguir el estado del juego

        // Evento para manejar el clic en los botones
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button; // Obtener el botón que ha sido selccionado

            if (encontrandoMatch == false) // Si no tiene coincidencia
            {
                button.IsVisible = false; // Ocultar el botón selccionado
                ultimoButtonCliked = button; // Establecer el botón selccioando como el último
                encontrandoMatch = true; // Indicar que se está buscando
            }
            else if (button.Text == ultimoButtonCliked.Text) // Si se encuentra una coincidencia
            {
                button.IsVisible = false; // Ocultar el botón seleccionado
                encontrandoMatch = false; // Restablecer el estado del juego
            }
            else // Si no  encuentra una coincidencia
            {
                ultimoButtonCliked.IsVisible = true; // Mostrar el último botón seleccionado
                encontrandoMatch = false; // Restablecer el estado del juego
            }
        }
    }
}