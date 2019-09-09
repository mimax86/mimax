using MugenMvvmToolkit.Attributes;
using System.Windows;

namespace Mimax.Desktop
{
    [ViewModel(typeof(DialogViewModel))]
    public partial class DialogView : Window
    {
        public DialogView()
        {
            InitializeComponent();
        }
    }
}