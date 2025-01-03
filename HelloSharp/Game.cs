using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Game
{
    private RenderWindow _window;
    private Sprite _playerSprite;

    public Game(RenderWindow window)
    {
        _window = window;
        InitializeGame();
    }

    private void InitializeGame()
    {
        // Загружаем текстуру игрока
        Texture playerTexture = new Texture("Resources/Images/Player_1.png");
        _playerSprite = new Sprite(playerTexture);
        _playerSprite.Position = new Vector2f(_window.Size.X / 2, _window.Size.Y / 2);
    }

    public void Update(float deltaTime)
    {
        // Логика обновления игрового процесса
        ProcessInput(deltaTime);
        UpdatePlayer(deltaTime);
    }

    private void ProcessInput(float deltaTime)
    {
        // Обрабатываем ввод пользователя
        if (Keyboard.IsKeyPressed(Keyboard.Key.W))
        {
            MoveUp(deltaTime);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.S))
        {
            MoveDown(deltaTime);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.A))
        {
            MoveLeft(deltaTime);
        }
        if (Keyboard.IsKeyPressed(Keyboard.Key.D))
        {
            MoveRight(deltaTime);
        }
    }

    private void MoveUp(float deltaTime)
    {
        _playerSprite.Position += new Vector2f(0, -200 * deltaTime);
    }

    private void MoveDown(float deltaTime)
    {
        _playerSprite.Position += new Vector2f(0, 200 * deltaTime);
    }

    private void MoveLeft(float deltaTime)
    {
        _playerSprite.Position += new Vector2f(-200 * deltaTime, 0);
    }

    private void MoveRight(float deltaTime)
    {
        _playerSprite.Position += new Vector2f(200 * deltaTime, 0);
    }

    private void UpdatePlayer(float deltaTime)
    {
        // Обновление позиции игрока
    }

    public void Draw()
    {
        _window.Clear(Color.Cyan);
        _window.Draw(_playerSprite);
        _window.Display();
    }
}