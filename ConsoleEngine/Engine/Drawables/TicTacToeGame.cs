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
        int BoardSize = 10;

        public void Play(int boardSize)
        {
            BoardSize = boardSize;
            Play();
        }

        public void Play()
        {
            bool isRunning = true;
            NewGame();

            while (isRunning)
            {
                int selectedMove = GetMoveIndex(Console.ReadKey(true).Key);
                if (Board.Move(CurrentPlayer, selectedMove))  EndTurn(); 
                else if (selectedMove == -2) NewGame();
                else if (selectedMove == -3) isRunning = false;
            }

            Board.Hide();
        }


        private void NewGame()
        {
            Board.Hide();
            Board = new TicTacToeBoard(BoardSize);
            CurrentPlayer = Player1;
            Board.Show();
        }


        private void EndTurn()
        {
            if (CurrentPlayer == Player1)
                CurrentPlayer = Player2;
            else
                CurrentPlayer = Player1;
        }


        private int GetMoveIndex(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad7:
                    return 0;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad8:
                    return 1;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad9:
                    return 2;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return 3;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    return 4;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    return 5;
                case ConsoleKey.D7:
                case ConsoleKey.NumPad1:
                    return 6;
                case ConsoleKey.D8:
                case ConsoleKey.NumPad2:
                    return 7;
                case ConsoleKey.D9:
                case ConsoleKey.NumPad3:
                    return 8;
                case ConsoleKey.Backspace:
                    return -2;
                case ConsoleKey.End:
                    return -3;
            }
            return -1;
        }


        class TicTacToeBoard
        {
            Glyph BoardBackground = new Glyph(' ');
            Glyph BoardLineFill = new Glyph('*');
            Display BoardDisplay;
            TicTacToeCell[] BoardCells;
            Sprite[] BoardLines = new Sprite[4];
            int[] RowIndexes = new int[3];
            int[] ColumnIndexes = new int[3];

            public TicTacToeBoard()
            {
                BoardDisplay = new Display(5, 5, BoardBackground);
                Init();
            }


            public TicTacToeBoard(int size){
                if(size > 80) size = 80;
                if(size < 5) size = 5;
                BoardDisplay = new Display(size, size, BoardBackground);
                Init();
            }


            private void Init(){

                CalulateRowIndexes();
                CalulateColumnIndexes();
                CreateBoardLines();
                CreateBoardCells();
                PlaceLinesOnBoard();
                PlaceCellsOnBoard();

            }


            private int CalculateBoardXOrigin(){
                return 40 - BoardDisplay.Width / 2;
            }


            private void CalulateRowIndexes(){
                int boardHeight = BoardDisplay.Height;
                int edgeOffset = boardHeight / 6;
                RowIndexes[0] = edgeOffset;
                RowIndexes[1] = boardHeight / 2;
                RowIndexes[2] = boardHeight - edgeOffset - 1;
            }


            private void CalulateColumnIndexes(){
                int boardWidth = BoardDisplay.Width;
                int edgeOffset = boardWidth / 6;
                ColumnIndexes[0] = edgeOffset;
                ColumnIndexes[1] = boardWidth / 2;
                ColumnIndexes[2] = boardWidth - edgeOffset - 1;
            }


            private void CreateBoardLines(){
                int width = BoardDisplay.Width;
                int height = BoardDisplay.Height;
                int columnOffset = width / 3;
                int rowOffset = height / 3;
                char lineChar = BoardLineFill.Symbol;

                BoardLines[0] = new Sprite (columnOffset, 0, height, 1, lineChar);
                BoardLines[1] = new Sprite (width - columnOffset - 1, 0, height, 1, lineChar);
                BoardLines[2] = new Sprite (0, rowOffset, 1, width, lineChar);
                BoardLines[3] = new Sprite (0, height - rowOffset - 1, 1, width, lineChar);
            }


            private void CreateBoardCells()
            {
                BoardCells = new TicTacToeCell[9];
                for (int i = 0; i < 9; i++) BoardCells[i] = new TicTacToeCell();
            }


            private void PlaceLinesOnBoard()
            {
                foreach(Sprite line in BoardLines) BoardDisplay.Sprites.Add(line);
            }


            private void PlaceCellsOnBoard(){
                int cellIndex = 0;
                for(int row = 0; row < 3; row++)
                {
                    for(int col = 0; col < 3; col++){
                        int colIndex = ColumnIndexes[col];
                        int rowIndex = RowIndexes[row];
                        BoardCells[cellIndex].CellSprite.Origin.X = colIndex;
                        BoardCells[cellIndex].CellSprite.Origin.Y = rowIndex;
                        cellIndex++;
                    }
                }

                foreach (TicTacToeCell cell in BoardCells)
                    BoardDisplay.Sprites.Add(cell.CellSprite);
            }


            public bool Move(TicTacToePlayer player, int cellID)
            {
                if (!IsValidMove(cellID)) return false;
                BoardCells[cellID].Owner = player;
                return true;
            }


            private bool IsValidMove(int cellID){
                if (cellID < 0 || cellID > 8) return false;
                if (BoardCells[cellID].Owner != null) return false;
                return true;
            }


            public void Hide()
            {
                BoardDisplay.Hide();
            }


            public void Show()
            {
                BoardDisplay.Show();
            }
        }



        class TicTacToeCell
        {
            private Glyph EmptyCellChar = new Glyph(' ');
            private TicTacToePlayer _owner = null;

            public Sprite CellSprite = new Sprite();
            public TicTacToePlayer Owner
            {
                get => _owner;
                set
                {
                    _owner = value;
                    CellSprite.Fill(value.PlayerAvatar);
                }
            }
            public Point Location
            {
                get => CellSprite.Origin;
                set
                {
                    CellSprite.Origin = value;
                }
            }

            public TicTacToeCell(){
                CellSprite.Origin = new Point(0,0);
                Init();
            }


            public TicTacToeCell(Point location)
            {
                CellSprite.Origin = location;
                Init();
            }


            public TicTacToeCell(Point location, int width, int height)
            {
                CellSprite.Origin = location;
                Area cellArea = new Area(width, height);
                Init();
                CellSprite.DisplayArea = cellArea;
            }


            public TicTacToeCell(int row, int col)
            {
                CellSprite.Origin = new Point(col, row);
                Init();
            }


            private void Init()
            {
                int adjustedRow = (int)CellSprite.Origin.Y * 2;
                int adjustedCol = (int)CellSprite.Origin.X * 2;
                Point displayLocation = new Point(adjustedCol, adjustedRow);
                Area displayArea = new Area(1, 1);
                CellSprite = new Sprite(displayLocation, displayArea, EmptyCellChar);
            }
        }



        class TicTacToePlayer
        {
            public Glyph PlayerAvatar;

            public TicTacToePlayer(char avatar)
            {
                PlayerAvatar = new Glyph(avatar);
            }
        }
    }
}
