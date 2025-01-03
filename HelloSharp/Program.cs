using SFML.Graphics;
using SFML.System;
using SFML.Window;

Console.WriteLine("Press ESC key to close window");
var window = new SimpleWindow();
window.Run();
Console.WriteLine("All done");


class SimpleWindow
{
    private CircleShape circle;
    private RenderWindow window;

    // Переменная для скорости движения
    float speedX = 200; // Пикселей в секунду
    float speedY = 200; // Пикселей в секунду
    float dt = 0;       // Время между кадрами

    public void Run()
    {
        var mode = new VideoMode(800, 600);
        window = new RenderWindow(mode, "SFML works!");
        window.KeyPressed += Window_KeyPressed;
        
        circle = new CircleShape(100f) { FillColor = Color.Blue };

        Clock clock = new Clock();

        // Start the game loop
        while (window.IsOpen)
        {
            // Process events
            window.DispatchEvents();

            // Обновляем время
            dt = clock.Restart().AsSeconds();

            // Двигаем круг
            circle.Position += new Vector2f(speedX * dt, speedY * dt);

            Кукуруку();

            // Очищаем экран
            window.Clear(Color.White);

            window.Draw(circle);
            // Finally, display the rendered frame on screen
            window.Display();
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

    private void Кукуруку()
    {
        // Проверяем границы экрана
        if (circle.Position.X > window.Size.X - circle.Radius * 2 || circle.Position.X < 0)
        {
            speedX *= -1; // Меняем направление
        }

        // Проверяем границы экрана
        if (circle.Position.Y > window.Size.Y - circle.Radius * 2 || circle.Position.Y < 0)
        {
            speedY *= -1; // Меняем направление
        }

    }
}