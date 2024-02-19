using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Collections.ObjectModel;
using System.Linq;
using Autodesk.Revit.DB.Architecture;
using static RevitAPISetup.SecondButton.SecondButtonViewModel;
using System.Collections.Generic;

namespace RevitAPISetup.SecondButton
{
    //Interacts with revit
    public class SecondButtonModel
    {
        public UIApplication UiApp { get; }
        public Autodesk.Revit.DB.Document Doc { get; }
        public SecondButtonModel(UIApplication uiApp)
        {
            UiApp = uiApp;
            Doc = uiApp.ActiveUIDocument.Document;
        }

        public ObservableCollection<SpatialObjectWrapper>CollectSpatialObjects() 
        {
            var spatialObjects = new FilteredElementCollector(Doc)
                .OfClass(typeof(SpatialElement))
                .WhereElementIsNotElementType()
                .Cast<SpatialElement>()
                .Where(x => x is Room)
                .Cast<Room>()
                .Select(x => new SpatialObjectWrapper(x));

            return new ObservableCollection<SpatialObjectWrapper> (spatialObjects);
        
        }
        public void Delete(List<SpatialObjectWrapper> selected)
        {
            var ids = selected.Select(x => x.ElementId).ToList();
            using (var trans = new Transaction(Doc, "DeleteRoom"))
            {
                trans.Start();
                Doc.Delete(ids);
                trans.Commit();
            }
        }
    }
}
