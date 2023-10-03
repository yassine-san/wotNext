using wotNext.Services;

namespace wotNext
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmdService = new CommandsService(args);
            cmdService.ManageArgs();
        }
    }
}