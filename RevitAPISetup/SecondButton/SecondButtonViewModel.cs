using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight;

namespace RevitAPISetup.SecondButton
{
    //go-between model and view 
    public class SecondButtonViewModel : ViewModelBase
    {
        public SecondButtonModel Model { get; set; }

        public RelayCommand<Window> Close { get; set; }

        public RelayCommand<Window> Delete { get; set; }


        private ObservableCollection<SpatialObjectWrapper> _spatialObjects;
        public ObservableCollection<SpatialObjectWrapper> SpatialObjects {
            
            get { return _spatialObjects; }
            set { _spatialObjects = value; RaisePropertyChanged(() => SpatialObjects); }

        }
        
        public SecondButtonViewModel(SecondButtonModel model) 
        {
            Model = model;
            SpatialObjects = Model.CollectSpatialObjects();
            Close = new RelayCommand<Window>(OnClose);
            Delete = new RelayCommand<Window>(OnDelete);
        }

        private void OnClose(Window win) 
        { 
        win.Close();
        }
        private void OnDelete(Window win)
        {
            var selected = SpatialObjects.Where(x => x.IsSelected).ToList();
            Model.Delete(selected);
            
        }

        public class SpatialObjectWrapper : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public double Area { get; set; }

        public ElementId ElementId { get; set; }

        private bool _isSelected;
        public bool IsSelected 
            {
                get { return _isSelected; }
                set { _isSelected = value; RaisePropertyChanged(nameof(IsSelected)); } 
            }

        public SpatialObjectWrapper(Room room) 
        {
            Name = room.Name;
            Area = room.Area;
            ElementId = room.Id;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
            private void RaisePropertyChanged(string propertyName) 
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    }
