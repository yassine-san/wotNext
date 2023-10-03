using System;

namespace wotNext.Services
{
    public static class InterfaceManagerService
    {
        public static void RunApp()
        {
            var dbService = new DatabaseService();
            var isDatabaseExist = DatabaseService.IsDbExist();
            if (!isDatabaseExist)
            {
                Console.WriteLine("Database doesn't exist, will be created shortly , please wait !!");
                var code = DatabaseService.CreateDatabase();
                if (code != 0)
                {
                    Console.WriteLine("Something wrong happened ... do Something and try later!");
                    Environment.Exit(0);
                }
                
            }

            Console.WriteLine("done");
            
            
        }
        private static void Gotoxy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
    }
}