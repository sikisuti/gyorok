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
using MiddleLayer.Representations;
using MiddleLayer;
using System.Threading.Tasks;
using Common.Enumerations;

namespace GyorokRentService.ViewModel
{
    public class searchTool_ModelView : ViewModelBase
    {
        public event EventHandler ToolSelected;
        public void OnToolSelected()
        {
            if (this.ToolSelected != null)
            {
                ToolSelected(selectedTool, null);
            }
        }

        public event EventHandler NewToolRequested;
        private void OnNewToolRequested(EventArgs e)
        {
            if (NewToolRequested != null)
            {
                NewToolRequested(null, e);
            }
        }

        ObservableCollection<Tools> temp = new ObservableCollection<Tools>();

        public List<ToolRepresentation> allTools;

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                if (_IsBusy != value)
                {
                    _IsBusy = value;
                    RaisePropertyChanged("IsBusy");
                }
            }
        }

        private string _searchText;
        private ObservableCollection<ToolRepresentation> _foundTools;
        private ToolRepresentation _selectedTool;
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
                FilterTools();
                RaisePropertyChanged("searchText");
            }
        }
        public ObservableCollection<ToolRepresentation> foundTools
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
        public ToolRepresentation selectedTool
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

                if (value != null && value.toolStatus.id == (long)ToolStatusEnum.Rented)
                {
                    RentalRepresentation rental = DataProxy.Instance.GetLastRentalByToolId(value.id);

                    plannedBringBackDate = "Vissza: " + rental.rentalEnd.ToString("D");
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
        
        public ICommand toolSelected { get { return new RelayCommand(toolSelectedExecute, () => true); } }
        void toolSelectedExecute()
        {
            OnToolSelected();
        }
        public ICommand openNewToolWindow { get { return new RelayCommand(openNewToolWindowExecute, () => true); } }
        void openNewToolWindowExecute()
        {
            OnNewToolRequested(EventArgs.Empty);
        }
        public ICommand delTool { get { return new RelayCommand(delToolExecute, () => true); } }
        void delToolExecute()
        {
            if (_selectedTool != null)
            {
                MessageBoxResult result;

                result = MessageBox.Show("Biztosan törlöd?", "Szerszám törlése...", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    DataProxy.Instance.DeleteToolById(selectedTool.id);
                    allTools.Remove(selectedTool);
                    FilterTools();
                    // TODO: Ha már ki volt választva, akkor a ToolSelector-ból is törölni kell!
                } 
            }
        }

        public searchTool_ModelView()
        {  
            if (!this.IsInDesignMode)
            {
                searchText = "";
                RefreshToolList();

                DataProxy.Instance.ToolsChanged += (s, a) => { RefreshToolList(); };
            }
        }

        public void RefreshToolList()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                IsBusy = true;
                allTools = DataProxy.Instance.GetAllTools();
                FilterTools();
                IsBusy = false;
            });
                     
        }

        private void FilterTools()
        {
            if (allTools != null)
            {
                foundTools = new ObservableCollection<ToolRepresentation>(allTools.Where(t => t.toolName.ToLower().StartsWith(_searchText.ToLower())).OrderBy(ot => ot.toolName).ToList());
            }
            
        }
    }
}
