namespace XIANGQI{
    class Table{
        public Chess[,] chessboard;
        public Table(){
            chessboard = new Chess[10,9];//10行9列的棋子数组
        }

        //初始化
        public void Initialize(){
            //all white
            for(int i = 0;i<10;i++){
                for(int j = 0;j<9;j++){
                    chessboard[i,j] = null;//全部置空
                }    
            }

            //Initialize chess in black
            chessboard[0,0] = new Che(Chess.ChessColour.BLACK);
            chessboard[0,1] = new Ma(Chess.ChessColour.BLACK);
            chessboard[0,2] = new Xiang(Chess.ChessColour.BLACK);
            chessboard[0,3] = new Shi(Chess.ChessColour.BLACK);
            chessboard[0,4] = new Jiang(Chess.ChessColour.BLACK);
            chessboard[0,5] = new Shi(Chess.ChessColour.BLACK);
            chessboard[0,6] = new Xiang(Chess.ChessColour.BLACK);
            chessboard[0,7] = new Ma(Chess.ChessColour.BLACK);
            chessboard[0,8] = new Che(Chess.ChessColour.BLACK);
            chessboard[2,1] = new Pao(Chess.ChessColour.BLACK);
            chessboard[2,7] = new Pao(Chess.ChessColour.BLACK);
            chessboard[3,0] = new Bing(Chess.ChessColour.BLACK);
            chessboard[3,2] = new Bing(Chess.ChessColour.BLACK);
            chessboard[3,4] = new Bing(Chess.ChessColour.BLACK);
            chessboard[3,6] = new Bing(Chess.ChessColour.BLACK);
            chessboard[3,8] = new Bing(Chess.ChessColour.BLACK);

            //Initialize chess in red
            chessboard[9,0] = new Che(Chess.ChessColour.RED);
            chessboard[9,1] = new Ma(Chess.ChessColour.RED);
            chessboard[9,2] = new Xiang(Chess.ChessColour.RED);
            chessboard[9,3] = new Shi(Chess.ChessColour.RED);
            chessboard[9,4] = new Jiang(Chess.ChessColour.RED);
            chessboard[9,5] = new Shi(Chess.ChessColour.RED);
            chessboard[9,6] = new Xiang(Chess.ChessColour.RED);
            chessboard[9,7] = new Ma(Chess.ChessColour.RED);
            chessboard[9,8] = new Che(Chess.ChessColour.RED);
            chessboard[7,1] = new Pao(Chess.ChessColour.RED);
            chessboard[7,7] = new Pao(Chess.ChessColour.RED);
            chessboard[6,0] = new Bing(Chess.ChessColour.RED);
            chessboard[6,2] = new Bing(Chess.ChessColour.RED);
            chessboard[6,4] = new Bing(Chess.ChessColour.RED);
            chessboard[6,6] = new Bing(Chess.ChessColour.RED);
            chessboard[6,8] = new Bing(Chess.ChessColour.RED);
        }

        //计算两个点直接的横纵距离
        public static (int XDis,int YDis) DisCaculate(int XNow,int YNow,int XDes,int YDes,Chess.ChessColour colour){
            //Calculate lateral movement distance
            int XDis = XNow-XDes;
            int YDis;

            //Calculate longitudinal movement distance
            if(colour == Chess.ChessColour.RED){
                YDis = YNow -YDes;//红色方向下走为负数，向上走为正数
            }else //black and red chess pieces are different  
                YDis = -(YNow - YDes);//黑色方向上走为负数，向下走为正数

            return (XDis,YDis);
        }

        //计算两个棋子直接有几个非空白的棋子（直线寻找）
        public static int IsBlock(int Now,int Des,int Line,bool isX,Chess[,] chessboard){
            
            //获取到坐标之间的最大最小值以判断是向前找还是向后找
            int Max = System.Math.Max(Now,Des);
            int Min = System.Math.Min(Now,Des);
            int count = 0;

            for(int i = Min+1;i<Max;i++){
                if(!isX && chessboard[Line-1,i-1] != null)
                    count++;//Y上找
                if(isX && chessboard[i-1,Line-1] != null)
                    count++;///X上找
            }
            return count;
        }

       
    }
}