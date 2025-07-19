using System;
using System.Windows.Forms;
using VehicleTradingSystem.Forms;

namespace VehicleTradingSystem
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm()); // Start with LoginForm instead of DashboardForm
        }
    }
}