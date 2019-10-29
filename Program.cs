using System;
using System.Collections.Generic;
using static ConsoleApplication1.Structure.GraphAssembler;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ConsoleApplication1.Structure;

namespace ConsoleApplication1
{
    class MainClass
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;


        /**
         * Inputs:
         * 63 68 69 80 90 42 52 101 111 151 131 161 181 201 201 261 301 281 351 //price: 37090 
         * 67 54 69 38 60 45 42 10 11 9 13 16 18 20 20 26 30 28 35 //price: 3471 (my var)
         * P1 P2 P3 C1 C2 C3 C4 P1->C1 P1->C2 P1->C3 P1->C4 P2->C1 P2->C2 P2->C3 P2->C4 P3->C1 P3->C2 P3->C3 P3->C4 
         */

        public static void Main()
        {
            var cnw = GetConsoleWindow();
            ShowWindow(cnw, SW_HIDE);
            MainWindow mainWindow = new MainWindow();
            Application.Run(mainWindow);
        }
    }
}