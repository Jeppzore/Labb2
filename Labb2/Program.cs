using System.IO;
using System.Linq.Expressions;


internal class Program
{

    private static void Main(string[] args)
    {
        // Startar spelet och bestämmer storleken på konsollen3
        Console.SetWindowSize(100, 30);
        //Console.SetBufferSize(100, 30);
        GameLoop.Start();       
   
    }

}
       



    
