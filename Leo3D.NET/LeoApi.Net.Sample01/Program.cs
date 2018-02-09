using System;

namespace LeoApi.Net.Sample01
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (LeoWindow example = new LeoWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
