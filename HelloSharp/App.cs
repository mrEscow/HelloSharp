using SFML.Graphics;
using SFML.System;
using SFML.Window;

class App
{
    //private CircleShape circle;
    private RenderWindow window;

    public void Run()
    {
        var mode = new VideoMode(800, 600);
        window = new RenderWindow(mode, "SFML works!");
        window.KeyPressed += Window_KeyPressed;

        // Создание экземпляров классов Menu и Game
        Menu menu = new Menu(window);
        Game game = new Game(window);

        // Флаг для определения текущего состояния
        bool isInMenu = true;


        // Start the game loop
        while (window.IsOpen)
        {
            // Process events
            window.DispatchEvents();

            if (isInMenu)
            {
                // Обработка обновления меню
                if (!menu.Update())
                {
                    menu.Draw();
                }
                else
                {
                    isInMenu = false;
                }
            }
            else
            {
                // Обновление и рендеринг игрового процесса
                game.Run();
            }
        }
    }

    private void Window_KeyPressed(object sender, KeyEventArgs e)
    {
        var window = (Window)sender;
        if (e.Code == Keyboard.Key.Escape)
        {
            window.Close();
        }
    }

}
