namespace TestTaskEscher.Services.ConsoleServices
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine(string line = null)
        {
            Console.WriteLine(line);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void Write(string line)
        {
            Console.Write(line);
        }

        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
