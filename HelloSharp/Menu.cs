using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Menu
{
    private RenderWindow _window;
    private Font _font;
    private Text _startGameText;
    private Text _exitText;

    public Menu(RenderWindow window)
    {
        _window = window;
        InitializeMenu();
    }

    private void InitializeMenu()
    {
        // Загрузка шрифта
        _font = new Font("Resources/Fonts/JungleAdventurer.ttf"); // Укажите путь к вашему шрифту

        // Создание текста для кнопки "Start Game"
        _startGameText = new Text("Start Game", _font, 30);
        _startGameText.Position = new Vector2f(_window.Size.X / 2 - _startGameText.GetLocalBounds().Width / 2,
                                               _window.Size.Y / 2 - 50);

        // Создание текста для кнопки "Exit"
        _exitText = new Text("Exit", _font, 30);
        _exitText.Position = new Vector2f(_window.Size.X / 2 - _exitText.GetLocalBounds().Width / 2,
                                          _window.Size.Y / 2 + 20);
    }

    public void Draw()
    {
        _window.Clear(Color.Black);
        _window.Draw(_startGameText);
        _window.Draw(_exitText);
        _window.Display();
    }

    public bool Update()
    {
        Vector2i mousePosition = Mouse.GetPosition(_window);

        // Проверка состояния кнопки "Start Game"
        if (_startGameText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
        {
            _startGameText.FillColor = Color.Green;
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                return true; // Переход к игровому экрану
            }
        }
        else
        {
            _startGameText.FillColor = Color.White;
        }

        // Проверка состояния кнопки "Exit"
        if (_exitText.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
        {
            _exitText.FillColor = Color.Red;
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                _window.Close(); // Выход из программы
            }
        }
        else
        {
            _exitText.FillColor = Color.White;
        }

        return false; // Остаемся в меню
    }
}
