using SFML.Graphics;
using SFML.System;

enum ObjectType { Empty, Type1, Type2, Type3 };

class Board
{
    private const int Rows = 8;
    private const int Columns = 8;
    private readonly ObjectType[,] grid = new ObjectType[Rows, Columns];

    public Board()
    {
        RandomizeGrid();
    }

    private void RandomizeGrid()
    {
        var random = new Random();
        for (int i = 0; i < Rows; ++i)
        {
            for (int j = 0; j < Columns; ++j)
            {
                grid[i, j] = (ObjectType)random.Next(1, 4); // Генерируем случайные объекты
            }
        }
    }

    public void Swap(int row1, int col1, int row2, int col2)
    {
        var temp = grid[row1, col1];
        grid[row1, col1] = grid[row2, col2];
        grid[row2, col2] = temp;
    }

    public void CheckAndRemoveMatches()
    {
        // Проверка горизонтальных рядов
        for (int i = 0; i < Rows; ++i)
        {
            for (int j = 0; j <= Columns - 3; ++j)
            {
                if (grid[i, j] != ObjectType.Empty &&
                    grid[i, j] == grid[i, j + 1] && grid[i, j] == grid[i, j + 2])
                {
                    grid[i, j] = ObjectType.Empty;
                    grid[i, j + 1] = ObjectType.Empty;
                    grid[i, j + 2] = ObjectType.Empty;
                }
            }
        }

        // Проверка вертикальных рядов
        for (int j = 0; j < Columns; ++j)
        {
            for (int i = 0; i <= Rows - 3; ++i)
            {
                if (grid[i, j] != ObjectType.Empty &&
                    grid[i, j] == grid[i + 1, j] && grid[i, j] == grid[i + 2, j])
                {
                    grid[i, j] = ObjectType.Empty;
                    grid[i + 1, j] = ObjectType.Empty;
                    grid[i + 2, j] = ObjectType.Empty;
                }
            }
        }

        DropObjects();
    }

    private void DropObjects()
    {
        for (int j = 0; j < Columns; ++j)
        {
            for (int i = Rows - 1; i >= 0; --i)
            {
                if (grid[i, j] == ObjectType.Empty)
                {
                    for (int k = i; k >= 1; --k)
                    {
                        grid[k, j] = grid[k - 1, j];
                    }
                    grid[0, j] = (ObjectType)new Random().Next(1, 4);
                }
            }
        }
    }

    public void Draw(RenderWindow window, Texture type1Tex, Texture type2Tex, Texture type3Tex)
    {
        int cellSize = Math.Min((int)window.Size.X / Columns, (int)window.Size.Y / Rows) - 16;
        int offsetX = ((int)window.Size.X - cellSize * Columns) / 2;
        int offsetY = ((int)window.Size.Y - cellSize * Rows) / 2;

        for (int i = 0; i < Rows; ++i)
        {
            for (int j = 0; j < Columns; ++j)
            {
                switch (grid[i, j])
                {
                    case ObjectType.Type1:
                        DrawCell(window, type1Tex, i, j, cellSize, offsetX, offsetY);
                        break;
                    case ObjectType.Type2:
                        DrawCell(window, type2Tex, i, j, cellSize, offsetX, offsetY);
                        break;
                    case ObjectType.Type3:
                        DrawCell(window, type3Tex, i, j, cellSize, offsetX, offsetY);
                        break;
                }
            }
        }
    }

    private void DrawCell(RenderWindow window, Texture tex, int row, int col, int size, int offsetX, int offsetY)
    {
        Sprite sprite = new Sprite(tex);
        sprite.Scale = new Vector2f(size / (float)tex.Size.X, size / (float)tex.Size.Y);
        sprite.Position = new Vector2f(offsetX + col * size, offsetY + row * size);
        window.Draw(sprite);
    }
}