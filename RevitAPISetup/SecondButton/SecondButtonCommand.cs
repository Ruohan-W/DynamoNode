using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Revit2023;
using RevitAPISetup.Utility;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Interop;


//addin file currently points to Appcommand, if only wokring on an external command point it to class containing IExternalCommand interface in this file
//by editing the <FullClassName> tag in the addin file to read namespace.classname and change the <AddIn> "type attribute" to read Command instead of Application

namespace RevitAPISetup.SecondButton
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class SecondButtonCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uiApp = commandData.Application;
                var m = new SecondButtonModel(uiApp);
                var vm = new SecondButtonViewModel(m);
                var v = new SecondButtonView
                {
                    DataContext = vm
                };

                var _ = new WindowInteropHelper(v); //functionality for modeless windows that operate independent of revit process so may stay open if revit is closed, not a problem for show dialog
                _.Owner = Process.GetCurrentProcess().MainWindowHandle; //ties model v to the revit main window 
                v.ShowDialog();
                return Result.Succeeded;

            }

            catch 
            {
                return Result.Failed;
            }
        }
        //create button logic here
        //save new buttons in a new folder under the shared project and change "Build action" under the png file's properties to "Embedded Resource"
        public static void CreateButton(RibbonPanel panel)
        {
            var assembly = Assembly.GetExecutingAssembly();
            panel.AddItem(new PushButtonData(MethodBase.GetCurrentMethod().DeclaringType.Name,
                "Second" + Environment.NewLine + "Button", assembly.Location,
                MethodBase.GetCurrentMethod().DeclaringType.FullName)
            { 
                ToolTip = "Second button Command",
                LargeImage = ImageUtils.LoadImage(assembly, "_32X32.2.png")
            });

        }
    }
}
