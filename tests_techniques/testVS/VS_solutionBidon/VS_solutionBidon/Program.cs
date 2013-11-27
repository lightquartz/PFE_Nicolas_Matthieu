﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace VS_solutionBidon
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length != 0)
            {
                Application.Run(new Form1(args[0]));
            }
            else
                Application.Run(new Form1());
        }
    }
}