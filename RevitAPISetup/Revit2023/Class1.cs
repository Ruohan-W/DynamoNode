using Autodesk.Revit.UI;
using RevitAPISetup.FirstButton;
using RevitAPISetup.SecondButton;
using System;

using System.Linq;

//addin file currently points to Appcommand, if only wokring on an external command point it to class containing IExternalCommand interface in button command cs file
//by editing the <FullClassName> tag in the addin file to read namespace.classname and change the <AddIn> "type attribute" to read Command instead of Application
namespace Revit2023
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
                    app.CreateRibbonTab("yournamehere"); //replace with name of your ribbon tab 
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

