using System;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

class Game
{
    private RenderWindow _window;
    private View _view;
    private Board _board;
    private Texture _type1Tex, _type2Tex, _type3Tex;
    private bool _isSwapping;
    private Vector2i _selectedCell;
    private RectangleShape border;

    private int cellSize;
    private int offsetX;
    private int offsetY;

    public Game(RenderWindow window)
    {
        _window = window;
        _view = new View(new FloatRect(0, 0, window.Size.X, window.Size.Y));
        _board = new Board();

        LoadTextures();

        CreateBorder();

        _window.MouseButtonPressed += OnMouseButtonPressed;
        _window.MouseButtonReleased += OnMouseButtonReleased;
    }

    private void LoadTextures()
    {
        _type1Tex = new Texture("Resources/Images/Type1.png");
        _type2Tex = new Texture("Resources/Images/Type2.png");
        _type3Tex = new Texture("Resources/Images/Type3.png");
    }

    private void CreateBorder()
    {
        cellSize = Math.Min((int)_window.Size.X / 8, (int)_window.Size.Y / 8) - 16;
        offsetX = ((int)_window.Size.X - cellSize * 8) / 2;
        offsetY = ((int)_window.Size.Y - cellSize * 8) / 2;

        border = new RectangleShape(new Vector2f(cellSize * 8 + 10, cellSize * 8 + 10));
        border.OutlineThickness = 5;
        border.OutlineColor = Color.Black;
        border.FillColor = Color.Transparent;
        border.Position = new Vector2f(offsetX - 5, offsetY - 5);
    }

    private void ResizeView()
    {
        // Получаем текущие размеры окна
        Vector2u windowSize = _window.Size;

        // Вычисляем новые размеры игрового поля (90% от ширины и высоты экрана)
        float fieldWidth = windowSize.X * 0.9f;
        float fieldHeight = windowSize.Y * 0.9f;

        // Центрирование игрового поля
        float xOffset = (windowSize.X - fieldWidth) / 2;
        float yOffset = (windowSize.Y - fieldHeight) / 2;

        // Устанавливаем размеры и позицию вида
        _view.Reset(new FloatRect(xOffset, yOffset, fieldWidth, fieldHeight));
        _window.SetView(_view);
    }

    public void Run()
    {
        Clock clock = new Clock();
        while (_window.IsOpen)
        {
            _window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();

            Update(deltaTime);
            Draw();
        }
    }

    private void OnMouseButtonPressed(object sender, EventArgs e)
    {
        if (!_isSwapping)
        {
            Vector2i mousePos = Mouse.GetPosition(_window);
            //int cellSize = Math.Min((int)_window.Size.X / 8, (int)_window.Size.Y / 8);
            //int offsetX = ((int)_window.Size.X - cellSize * 8) / 2;
            //int offsetY = ((int)_window.Size.Y - cellSize * 8) / 2;

            int row = (mousePos.Y - offsetY) / cellSize;
            int col = (mousePos.X - offsetX) / cellSize;

            if (row >= 0 && row < 8 && col >= 0 && col < 8)
            {
                _selectedCell = new Vector2i(row, col);
                _isSwapping = true;
            }
        }
    }

    private void OnMouseButtonReleased(object sender, EventArgs e)
    {
        if (_isSwapping)
        {
            Vector2i mousePos = Mouse.GetPosition(_window);
            //int cellSize = Math.Min((int)_window.Size.X / 8, (int)_window.Size.Y / 8);
            //int offsetX = ((int)_window.Size.X - cellSize * 8) / 2;
            //int offsetY = ((int)_window.Size.Y - cellSize * 8) / 2;

            int row = (mousePos.Y - offsetY) / cellSize;
            int col = (mousePos.X - offsetX) / cellSize;

            if (row >= 0 && row < 8 && col >= 0 && col < 8)
            {
                if ((Math.Abs(row - _selectedCell.X) + Math.Abs(col - _selectedCell.Y)) == 1)
                {
                    _board.Swap(_selectedCell.X, _selectedCell.Y, row, col);
                    _board.CheckAndRemoveMatches();
                }
            }

            _isSwapping = false;
        }
    }

    private void Update(float deltaTime)
    {
        // Логика обновления игрового процесса
        _board.CheckAndRemoveMatches();
    }

    private void Draw()
    {
        _window.Clear(Color.White);

        // Рисуем рамку
        _window.Draw(border);

        _board.Draw(_window, _type1Tex, _type2Tex, _type3Tex);

        _window.Display();
    }
}