namespace TestTaskEscher.Services.ConsoleServices
{
    public interface IConsoleService
    {
        void WriteLine(string line);
        void WriteLine();
        void Write(string line);
        ConsoleKeyInfo ReadKey(bool intercept = false);
        string ReadLine();
    }
}