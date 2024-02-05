using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autodesk.DesignScript.Runtime;
using Autodesk.Revit.DB;
using Revit.Elements;
using RevitServices.Transactions;
using RevitServices.Persistence;
using Autodesk.DesignScript.Geometry;
using Revit.GeometryConversion;

namespace DynamoNode
{
    public class Grids
    {
        public static List<Autodesk.DesignScript.Geometry.Rectangle> RectangularGrid(int xCount, int yCount)
        {
            double x = 0;
            double y = 0;
            var pList = new List<Autodesk.DesignScript.Geometry.Rectangle>();

            for (int i = 0; i < xCount; i++)
            {
                y++;
                x = 0;

                for (int j = 0; j < yCount; j++) 
                {
                    x++;
                    Autodesk.DesignScript.Geometry.Point pt = Autodesk.DesignScript.Geometry.Point.ByCoordinates(x, y);
                    Autodesk.DesignScript.Geometry.Vector vec = Autodesk.DesignScript.Geometry.Vector.ZAxis();
                    Autodesk.DesignScript.Geometry.Plane bP = Autodesk.DesignScript.Geometry.Plane.ByOriginNormal(pt, vec);
                    Autodesk.DesignScript.Geometry.Rectangle rect = Autodesk.DesignScript.Geometry.Rectangle.ByWidthLength(bP, 1, 1);
                    pList.Add(rect);
                }
            }

            return pList;
        }
    }
}
