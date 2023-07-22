namespace AnagramSolver.Contracts.Interfaces
{
    [Obsolete]
    public interface IFileReader
    {
        byte[] GetFile();

        string ReadFile(string path);

        void WriteFile(string lines);
    }
}
