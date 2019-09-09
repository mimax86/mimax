using MugenMvvmToolkit.ViewModels;

namespace Mimax.Desktop
{
    public class DialogViewModel : CloseableViewModel
    {
        public DialogViewModel()
        {
            Title = "Dialog Window";
        }

        public string Title { get; private set; }
    }
}