namespace ExtendableTextEditor
{
    using System.Text;

    public interface IControllerState
    {
        StringBuilder Text { get; }
        int CurrentPosition { get; set; }

        bool IsValid();
    }
}
