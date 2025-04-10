using BLL.ModelsService;
using System;
using System.Text;

namespace appz_lab_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            UI ui = new UI();
            ui.ShowUi();
        }
    }
}
