using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using board = Omok.OmokBoard;
namespace Omok {
    public partial class MainForm : Form {

        public MainForm() {
            InitializeComponent();
            this.BackColor = Color.Orange;

            this.ClientSize = new Size(2 * board.margin + board.lineCount * board.stoneSize,
                2 * board.margin + board.lineCount * board.stoneSize+menuStrip1.Height);
        }
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
            DrawBoard();

        }
        private void DrawBoard() {
            board.g = Board.CreateGraphics();

            for(int i = 0; i < board.lineCount + 1; i++) {
                // 세로선
                board.g.DrawLine(board.pen, new Point(board.margin + i * board.stoneSize, board.margin),
                    new Point(board.margin + i * board.stoneSize, board.margin + board.lineCount * board.stoneSize));
                // 가로선
                board.g.DrawLine(board.pen, new Point(board.margin,board.margin + i * board.stoneSize),
                    new Point(board.margin + board.lineCount * board.stoneSize, board.margin + i * board.stoneSize));
            }

            for(int x = 3; x <= 15; x += 6) {
                for(int y = 3; y <= 15; y += 6) {
                    board.g.FillEllipse(board.bBrush, board.margin + board.stoneSize * x - board.flowerSpotSize / 2,
                        board.margin + board.stoneSize * y - board.flowerSpotSize / 2, board.flowerSpotSize, board.flowerSpotSize);
                }
            }
        }

        private void Board_MouseDown(object sender, MouseEventArgs e) {
            int x = (e.X - board.margin + board.stoneSize / 2) / board.stoneSize;
            int y = (e.Y - board.margin + board.stoneSize / 2) / board.stoneSize;

            if (board.board[x, y] != board.STONE.none) return;

            Rectangle r = new Rectangle(board.margin + board.stoneSize * x - board.stoneSize / 2,
                board.margin + board.stoneSize * y - board.stoneSize / 2, board.stoneSize, board.stoneSize);

            if (board.flag == false) {
                Bitmap bmp = new Bitmap("../../Images/black.png");
                board.g.DrawImage(bmp, r);
                board.flag = true;
                board.board[x, y] = board.STONE.black;
                DrawStoneSequence(board.stoneCount++, Brushes.White, r);
            } else {
                Bitmap bmp = new Bitmap("../../Images/white.png");
                board.g.DrawImage(bmp, r);
                board.flag = false;
                board.board[x, y] = board.STONE.white;
                DrawStoneSequence(board.stoneCount++, Brushes.Black, r);
            }

            CheckOmok(x, y);
        }

        private void DrawStoneSequence(int v, Brush color, Rectangle r) {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            board.g.DrawString(v.ToString(), new Font("맑은 고딕", 10), color, r, stringFormat);
        }

        private void CheckOmok(int x, int y) {
            int count = 1;

            for (int i = x + 1; i <= board.lineCount; i++) {
                if (board.board[i, y] == board.board[x, y]) count++;
                else break;
            }

            for (int i = x - 1; i <= board.lineCount; i--) {
                if (board.board[i, y] == board.board[x, y]) count++;
                else break;
            }

            if (count >= 5) {
                GameOver(x,y);
                return;
            }
        }

        private void GameOver(int x, int y) {
            DialogResult res = MessageBox.Show(board.board[x, y].ToString().ToUpper() + " Wins");

            if (res == DialogResult.Yes)
                NewGame();
            else if (res == DialogResult.No)
                this.Close();
        }

        private void NewGame() {
            board.flag = false;

            for (int x = 0; x < board.lineCount + 1; x++)
                for (int y = 0; y < board.lineCount + 1; y++)
                    board.board[x, y] = board.STONE.none;

            Board.Refresh();
            DrawBoard();
        }
    }
}
