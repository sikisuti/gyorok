using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;

namespace GyorokRentService.ViewModel
{
    class NewCustomerWindow_ViewModel : ViewModelBase
    {
        public event EventHandler RequestClose;

        public NewCustomerWindow_ViewModel()
        {
            AppMessages.CloseNewCustomerWindow.Register(this, closeWindow);
        }

        private void closeWindow(string s)
        {
            EventHandler handler = RequestClose;
            handler(this, EventArgs.Empty);
        }

        
    }
}
