using Autodesk.Revit.UI;
using RevitAPISetup.FirstButton;
using System;
using System.Linq;
using RevitAPISetup.SecondButton;

namespace Revit2024
{
    public class AppCommand : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication app)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication app)
        {
            try
            {
                app.CreateRibbonTab("Yournamehere"); //replace with name of your ribbon tab 
            }
            catch (Exception e) 
            { 
                //ignored
            }

            //create ribbon panel look for existing panels if none create new panel and assign to variable

            var ribbonPanel = app.GetRibbonPanels().FirstOrDefault(x => x.Name == "yourpanelnamehere") ?? 
                              app.CreateRibbonPanel("yournamehere", "yourpanelnamehere");

            //Create button here
            FirstButtonCommand.CreateButton(ribbonPanel);
            SecondButtonCommand.CreateButton(ribbonPanel);

            return Result.Succeeded;
        }
    }
}
