using System.Text;

namespace TextEditor
{
    public interface IControllerState
    {
        StringBuilder Text { get; }
        int CurrentPosition { get; set; }

        bool IsValid();
    }
}