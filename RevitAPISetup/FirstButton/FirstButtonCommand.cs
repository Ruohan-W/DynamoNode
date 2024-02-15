using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPISetup.Utility;
using System;
using System.Reflection;
using System.Windows;


//addin file currently points to Appcommand, if only wokring on an external command point it to class containing IExternalCommand interface in this file
//by editing the <FullClassName> tag in the addin file to read namespace.classname and change the <AddIn> "type attribute" to read Command instead of Application

namespace RevitAPISetup.FirstButton
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    internal class FirstButtonCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                MessageBox.Show("Hello", "yournamehere", MessageBoxButton.OK);
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
                "First" + Environment.NewLine + "Button", assembly.Location,
                MethodBase.GetCurrentMethod().DeclaringType.FullName)
            { 
                ToolTip = "First button Command",
                LargeImage = ImageUtils.LoadImage(assembly, "_32X32.1.png")
            });

        }
    }
}
