public interface IToast
{
    void Open(string message);
    void Close();
    bool IsOpen { get; }
}