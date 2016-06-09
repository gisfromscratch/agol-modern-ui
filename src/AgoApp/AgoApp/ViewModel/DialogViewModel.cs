using GalaSoft.MvvmLight;
using MahApps.Metro.Controls.Dialogs;

namespace AgoApp.ViewModel
{
    public class DialogViewModel : ViewModelBase
    {
        private IDialogCoordinator _dialogCoordinator;

        public DialogViewModel()
        {
            Coordinator = DialogCoordinator.Instance;
        }

        public IDialogCoordinator Coordinator
        {
            get { return _dialogCoordinator; }
            set { Set(ref _dialogCoordinator, value); }
        }
    }
}
