namespace AnagramSolver.Contracts.Interfaces
{
    public interface IFileReader
    {
        byte[] GetFile();

        string ReadFile(string path);

        void WriteFile(string lines);
    }
}
