using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using GyorokRentService.ViewModel;
using SQLConnectionLib;

namespace GyorokRentService.ViewModel
{
    class searchTool_ModelView : ViewModelBase
    {
        ObservableCollection<Tools> temp = new ObservableCollection<Tools>();

        private string _searchText;
        private ObservableCollection<Tools> _foundTools;
        private long _selectedToolID;
        private Tools _selectedTool;
        private string _plannedBringBackDate;        
                
        public string searchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (_searchText == value)
                {
                    return;
                }

                _searchText = value;
                RaisePropertyChanged("searchText");
            }
        }
        public ObservableCollection<Tools> foundTools
        {
            get
            {
                return _foundTools;
            }

            set
            {
                if (_foundTools == value)
                {
                    return;
                }

                _foundTools = value;
                RaisePropertyChanged("foundTools");
            }
        }
        public long selectedToolID
        {
            get
            {
                return _selectedToolID;
            }

            set
            {
                if (_selectedToolID == value)
                {
                    return;
                }

                selectedTool = SQLConnection.Execute.ToolsTable.Single(t => t.toolID == value);
                
                _selectedToolID = value;
                RaisePropertyChanged("selectedToolID");
            }
        }
        public Tools selectedTool
        {
            get
            {
                return _selectedTool;
            }

            set
            {
                if (_selectedTool == value)
                {
                    return;
                }

                if (value.toolStatusID == 3)
                {
                    var r = SQLConnection.Execute.RentalsTable.Where(rt => rt.toolID == value.toolID);
                    var lastr = r.Single(rs => rs.rentalID == r.Max(rm => rm.rentalID));
                    var re = lastr.rentalEnd;

                    plannedBringBackDate = "Vissza: " + re.ToString("D");
                }
                else
                {
                    plannedBringBackDate = string.Empty;
                }

                _selectedTool = value;
                RaisePropertyChanged("selectedTool");
            }
        }
        public string plannedBringBackDate
        {
            get
            {
                return _plannedBringBackDate;
            }

            set
            {
                if (_plannedBringBackDate == value)
                {
                    return;
                }

                _plannedBringBackDate = value;
                RaisePropertyChanged("plannedBringBackDate");
            }
        }

        public ICommand searchTextChanged { get { return new RelayCommand(searchTextChangedExecute, () => true); } }
        void searchTextChangedExecute()
        {
            RefreshToolList();
        }
        public ICommand toolSelected { get { return new RelayCommand(toolSelectedExecute, () => true); } }
        void toolSelectedExecute()
        {
            try
            {
                //db.Dispose();
                //db = new dbGyorokEntities();
                _selectedTool = (from t in SQLConnection.Execute.ToolsTable where t.toolID == _selectedToolID select t).First();

                AppMessages.ToolToSelect.Send(_selectedTool);
            }
            catch (Exception)
            {
                
            }
        }
        public ICommand openNewToolWindow { get { return new RelayCommand(openNewToolWindowExecute, () => true); } }
        void openNewToolWindowExecute()
        {
            View.NewTool wndNewTool = new View.NewTool();
            wndNewTool.Show();
        }
        public ICommand delTool { get { return new RelayCommand(delToolExecute, () => true); } }
        void delToolExecute()
        {
            if (_selectedToolID != 0)
            {
                MessageBoxResult result;

                result = MessageBox.Show("Biztosan törlöd?", "Szerszám törlése...", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    SQLConnection.Execute.delTool(_selectedToolID);
                    _selectedToolID = 0;
                    AppMessages.ToolListModified.Send("");
                } 
            }
        }

        public searchTool_ModelView()
        {  
            if (!this.IsInDesignMode)
            {
                searchText = "";
                //AppMessages.ToolToSelect.Register(this, (t) => RefreshToolList());
                AppMessages.ToolModified.Register(this, s => RefreshToolList());
                AppMessages.ToolListModified.Register(this, t => RefreshToolList());
                AppMessages.RentalPaid.Register(this, p => RefreshToolList());
                AppMessages.RentGroupClosed.Register(this, rg => RefreshToolList());
                RefreshToolList();
            }
        }

        private void RefreshToolList()
        {
            foundTools = new ObservableCollection<Tools>(SQLConnection.Execute.ToolsTable.Where<Tools>(t => t.toolName.StartsWith(_searchText) && t.isDeleted == false).OrderBy<Tools, string>(ot => ot.toolName).ToList<Tools>());                    
        }
    }
}
