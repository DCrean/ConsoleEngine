using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEngine.Engine.Drawables
{
    class TicTacToeGame
    {
        TicTacToeBoard Board = new TicTacToeBoard();
        TicTacToePlayer Player1 = new TicTacToePlayer('X');
        TicTacToePlayer Player2 = new TicTacToePlayer('O');
        TicTacToePlayer CurrentPlayer;


        public void Play()
        {
            bool isRunning = true;
            CurrentPlayer = Player1;

            while (isRunning)
            {
                ConsoleKey move = Console.ReadKey(true).Key;
                int selectedMove = GetMoveIndex(move);
                if (Board.Move(CurrentPlayer, selectedMove)) { EndTurn(); }
            }

            Board.Hide();
        }


        private void EndTurn()
        {
            if(CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
            } else
            {
                CurrentPlayer = Player1;
            }
        }


        private int GetMoveIndex(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.NumPad7:
                    return 0;
                case ConsoleKey.NumPad8:
                    return 1;
                case ConsoleKey.NumPad9:
                    return 2;
                case ConsoleKey.NumPad4:
                    return 3;
                case ConsoleKey.NumPad5:
                    return 4;
                case ConsoleKey.NumPad6:
                    return 5;
                case ConsoleKey.NumPad1:
                    return 6;
                case ConsoleKey.NumPad2:
                    return 7;
                case ConsoleKey.NumPad3:
                    return 8;
            }
            return -1;
        }
    }

    internal class TicTacToeBoard
    {
        Glyph BoardBackground = new Glyph('*');
        Display BoardDisplay;
        TicTacToeCell[] BoardCells;

        public TicTacToeBoard()
        {
            BoardDisplay = new Display(5, 10, BoardBackground);
            BoardCells = createBoardCells();
            foreach (TicTacToeCell cell in BoardCells)
            {
                BoardDisplay.Sprites.Add(cell.CellSprite);
            }
            BoardDisplay.Show();
        }

        private TicTacToeCell[] createBoardCells()
        {
            TicTacToeCell[] cells = new TicTacToeCell[9];
            int row = 0, col = 0;
            for (int i = 0; i < 9; i++)
            {
                if (col == 3)
                {
                    row++;
                    col = 0;
                }

                cells[i] = new TicTacToeCell(row, col);
                col++;
            }
            return cells;
        }

        public bool Move(TicTacToePlayer player, int cellID)
        {
            if (BoardCells[cellID].Owner != null) { return false; }
            BoardCells[cellID].Owner = player;
            return true;
        }


        public void Hide()
        {
            BoardDisplay.Hide();
        }
    }

    internal class TicTacToeCell
    {
        private Point CellLocation;
        private Glyph EmptyCellChar = new Glyph(' ');
        private TicTacToePlayer _owner = null;

        public Sprite CellSprite;
        public TicTacToePlayer Owner 
        {
            get => _owner;
            set
            {
                _owner = value;
                CellSprite.Fill(value.PlayerAvatar);
            }
        }


        public TicTacToeCell(Point location)
        {
            CellLocation = location;
            init();
        }


        public TicTacToeCell(int row, int col)
        {
            CellLocation = new Point(col, row);
            init();
        }


        private void init()
        {
            int adjustedRow = (int) CellLocation.Y * 2;
            int adjustedCol = (int) CellLocation.X * 2;
            Point displayLocation = new Point(adjustedCol, adjustedRow);
            Area displayArea = new Area(1, 1);
            CellSprite = new Sprite(displayLocation, displayArea, EmptyCellChar);
        }
    }

    internal class TicTacToePlayer
    {
        public Glyph PlayerAvatar;

        public TicTacToePlayer(char avatar)
        {
            PlayerAvatar = new Glyph(avatar);
        }
    }
}
