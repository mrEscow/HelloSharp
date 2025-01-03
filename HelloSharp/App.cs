using SFML.Graphics;
using SFML.System;
using SFML.Window;

class App
{
    //private CircleShape circle;
    private RenderWindow window;

    // Переменная для скорости движения
    float speedX = 200; // Пикселей в секунду
    float speedY = 200; // Пикселей в секунду
    float deltaTime = 0;       // Время между кадрами

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

        //circle = new CircleShape(100f) { FillColor = Color.Blue };

        Clock clock = new Clock();

        // Start the game loop
        while (window.IsOpen)
        {
            // Process events
            window.DispatchEvents();

            // Обновляем время
            deltaTime = clock.Restart().AsSeconds();

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
                game.Update(deltaTime);
                game.Draw();
            }
            // Двигаем круг
            //circle.Position += new Vector2f(speedX * deltaTime, speedY * deltaTime);

            // Очищаем экран
            //window.Clear(Color.White);

            //window.Draw(circle);
            // Finally, display the rendered frame on screen
            //window.Display();
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
