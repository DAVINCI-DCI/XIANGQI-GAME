using System;

namespace XIANGQI{
    class tableService{

        Table table;
        public tableService(){
            table =  new Table();//Create a table which is a two-dimensional array of chess pieces
        }

        void DisplayTable(){
            Console.WriteLine("\nY\\X  1     2     3     4     5     6     7     8     9    |\n");
            for(int i = 0;i<10;i++){
                if(i == 5){
                    Console.WriteLine("|                                                         |");
                    Console.WriteLine("|                  楚河               汉界                |");
                    Console.WriteLine("|                                                         |\n");
                    
                }

                if(i == 9)
                    Console.Write(i+1 + "  ");/*special case when i+1 = 10, will causes the tenth  line  
                                               have one more space than the other lines*/
                else
                    Console.Write(i+1 + "   "); //Write a b c .... of y in the table               
                    

                for(int j = 0;j<9;j++){
                    
                    if(table.chessboard[i,j] == null)
                        Console.Write(" .    ");
                    else{
                        if(table.chessboard[i,j].Colour == Chess.ChessColour.RED){
                            Console.ForegroundColor = ConsoleColor.Red;//改变颜色
                            
                        }//Change the colour
                        if(table.chessboard[i,j].Colour == Chess.ChessColour.BLACK){
                            Console.ForegroundColor = ConsoleColor.Yellow;
                           
                        }
                        Console.Write(table.chessboard[i,j].ToString() + "    ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    
                }
                Console.WriteLine("|\n");
            }
        }

        //DisplayTable的重载方法，传入当前点的坐标，遍历同时判断是否可以走，可以标亮底色
        void DisplayTable(int XNow,int YNow){
            Console.WriteLine("\nY\\X  1     2     3     4     5     6     7     8     9    |\n");
            for(int i = 0;i<10;i++){
                if(i == 5){
                    Console.WriteLine("|                                                         |");
                    Console.WriteLine("|                  楚河               汉界                |");
                    Console.WriteLine("|                                                         |\n");
                    
                }

                if(i == 9)
                    Console.Write(i+1 + "  ");/*special case when i+1 = 10, will causes the tenth  line  
                                               have one more space than the other lines*/
                else
                    Console.Write(i+1 + "   "); //Write a b c .... of y in the table               
                    

                for(int j = 0;j<9;j++){
                    
                    //当前点测试一遍是否可走，可以的话就将底色改为DarkGreen
                    if(table.chessboard[YNow-1,XNow-1].MovingRule(XNow,YNow,j+1,i+1,table.chessboard)){
                         if(table.chessboard[i,j] == null)//排除空指针的情况
                             Console.BackgroundColor = ConsoleColor.DarkGreen;
                        else if(table.chessboard[i,j].Colour != table.chessboard[YNow-1,XNow-1].Colour)//排除颜色相同的情况
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                        
                    }
                        
                    
                    if(table.chessboard[i,j] == null){
                        Console.Write(" .");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("    ");
                    }else{
                        if(table.chessboard[i,j].Colour == Chess.ChessColour.RED){
                            Console.ForegroundColor = ConsoleColor.Red;//改变颜色
                            
                        }//Change the colour
                        if(table.chessboard[i,j].Colour == Chess.ChessColour.BLACK){
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            
                        }
                        Console.Write(table.chessboard[i,j].ToString());
                        Console.ForegroundColor = ConsoleColor.White; 
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write("    ");
                    }
                    
                }
                Console.WriteLine("|\n");
            }
        }

        //获取目标和目的地的棋子坐标
        (int X,int Y) Getcoordinate(bool isMove){
            int X;
            int Y;

            if(isMove){
                Console.WriteLine("\nPlease enter the x of the chess you want to move :  ");
                X = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\nPlease enter the y of the chess you want to move :  ");
                Y = Convert.ToInt32(Console.ReadLine());

            }else{
                Console.WriteLine("\nPlease enter the x of the position you want to put :  ");
                X = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\nPlease enter the y of the position you want to put :  ");
                Y = Convert.ToInt32(Console.ReadLine());
                
            }

            return (X,Y);
        }

        //返回判断输赢的条件和判断哪方获胜
         (bool isWing,bool isRedWing) MoveChess(bool isRed){

            int XNow = 0;
            int YNow = 0;
            int XDes = 0;
            int YDes = 0;
            bool isWing = false;
            bool isRedWing = false;
            
            try{

                //当前坐标
                bool Flag = false;
                while(!Flag){

                    //Choose the chess pieces
                    (XNow,YNow) = Getcoordinate(true);
                    Flag = SelectingChess(XNow,YNow,isRed);
                }

                Console.Clear();

                //目的地坐标
                Flag = false;
                while(!Flag){
                    //为没有游戏经验的玩家提供当前棋子可以走的位置
                    DisplayTable(XNow,YNow);
                    
                    //move the chess pieces
                    (XDes,YDes) = Getcoordinate(false);
                    (Flag,isWing,isRedWing) = MovingChess(XNow,YNow,XDes,YDes);
                }
            }catch(Exception e){
                    Console.WriteLine(e.Message);//输出报错信息
            } 

            
            return (isWing,isRedWing);
        }

        //判断输入坐标合不合格
       bool SelectingChess(int XNow, int YNow,bool isRed){
           if(XNow > 9 || XNow < 1 || YNow > 10 || YNow < 1){//Determine if it is out of bounds
                Console.WriteLine("\n! The postion doesn't exist!");
                return false;
           }

            Chess chess = table.chessboard[YNow-1,XNow-1];
            
             if(chess == null){
                Console.WriteLine("\n! There is no chess on the position!");
                return false;
            }

            //Make sure you don't choose the wrong pieces
            if(!isRed && chess.Colour == Chess.ChessColour.BLACK){
                Console.WriteLine("\n! It's red's turn now !");
                return false;
            }

            if(isRed && chess.Colour == Chess.ChessColour.RED){
                Console.WriteLine("\n ! It's black's turn now !");
                return false;
            }

            return true;
       }


        //return 是否要继续输出目的地坐标（目的地坐标不合格） 判断输赢的条件和判断哪方获胜
       (bool isContinue,bool isWing,bool isRedWing) MovingChess(int XNow,int YNow,int XDes,int YDes){
           //Determine if it is out of bounds
           if( YDes > 10 || YDes < 1 || XDes > 9 || XDes < 1){
                Console.WriteLine("\n! The postion doesn't exist!");   
                return (false,false,false);
           }

            Chess chess = table.chessboard[YNow-1,XNow-1];
            Chess chess_des = table.chessboard[YDes-1,XDes-1];

            if(chess_des == null){//排除空指针的情况
                //The way the pieces are moved does not conform to the rules
                if(!chess.MovingRule(XNow,YNow,XDes,YDes,table.chessboard)){//每个棋子分别重写父类的MovingRule方法
                    Console.WriteLine( chess.ToString() + " can't move like this!");//失败的情况
                    return (false,false,false);         
                }

            }else{
                 //When moving a piece, it is blocked by pieces of the same color
                if(chess_des.Colour == chess.Colour){
                    Console.WriteLine("\n! A chess is already here, Please choose another place !");
                    return (false,false,false);
                }

                //The way the pieces are moved does not conform to the rules
                if(!chess.MovingRule(XNow,YNow,XDes,YDes,table.chessboard)){//每个棋子分别重写父类的MovingRule方法
                    Console.WriteLine( chess.ToString() + " can't move like this!");         
                    return (false,false,false);
                }

                if(chess_des.Colour == Chess.ChessColour.RED && chess_des.Type == Chess.TypeChess.JIANG)
                    return (true,true,false);//黄色赢
                if(chess_des.Colour == Chess.ChessColour.BLACK && chess_des.Type == Chess.TypeChess.JIANG)
                    return (true,true,true);//黑色赢
            }

            //Officially move chess pieces
            table.chessboard[YDes-1,XDes-1] = chess; // 将Now 转到 Des
            table.chessboard[YNow-1,XNow-1] = null; // 置为空

            return (true,false,false);//继续下棋
       }

        bool switchChesser(bool isRed){
            Console.WriteLine("*******************************************");
            
            if(isRed)
                Console.WriteLine("**********   It's Red's turn   ************");
            else
                Console.WriteLine("**********  It's Yellow's turn  ************");
            
            Console.WriteLine("*******************************************");

            return !isRed;//每回合转换下棋方
        }


        //use table to call play to play chess rather than direct call DisplayTbale....
        public void play(){
            Console.Clear();//Make console more clear
            Console.WriteLine("**************     Let us play     **************");
            table.Initialize();

            bool isRed = true;
            bool isWing = false;
            bool isRedWing = false;
            while(!isWing){
                DisplayTable();
                isRed = switchChesser(isRed);//转换红黄
                (isWing,isRedWing) = MoveChess(isRed);//if isWing == true 说明赢了， isRedWing 则判断是哪一方获胜 
             }   
            
            
            if(isRedWing)
                Console.WriteLine("\nCongratulations, the red side is win\n"); 
            else    
                Console.WriteLine("\nCongratulations, the yellow side is win\n");          
        }
    }
}