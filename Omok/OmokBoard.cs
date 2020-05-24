using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omok {
    class OmokBoard {
        public readonly static int margin = 40;
        public readonly static int gradationSize = 30;
        public readonly static int stoneSize = 28;
        public readonly static int flowerSpotSize = 10;
        public readonly static int lineCount = 18;

        public enum STONE { none,black,white};
        public static STONE[,] board = new STONE[19, 19];
        public static bool flag = false;
        public static int stoneCount = 1; 
        
        public static Graphics g;
        public static Pen pen = new Pen(Color.Black);
        public static Brush wBrush = new SolidBrush(Color.White), bBrush = new SolidBrush(Color.Black);



    }
}
