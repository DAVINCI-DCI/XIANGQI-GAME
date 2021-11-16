using System;

namespace XIANGQI{
    class Program{
       public static void menu(){
            Console.WriteLine("*****   Welcome to XIANG QI   ****");
            Console.WriteLine("*****          1. play         ****");
            Console.WriteLine("*****          0. exit         ****");
       }

       
        static void Main(String[] args){
            while(true){
                menu();
                
                try{
                    Console.WriteLine("enter key you want: ");
                    int key = Convert.ToInt32(Console.ReadLine());

                    if(key == 0)//s输入0结束游戏
                     break;
                     
                    if(key == 1){
                        tableService service = new tableService();
                        service.play();
                    }else   
                        Console.WriteLine("Don't konw what you want, Please enter 0 or 1 !");
                }catch(Exception e){
                    Console.WriteLine(e.Message + "  Please enter 0 or 1");
                }
                
            }
        }
    }    
}