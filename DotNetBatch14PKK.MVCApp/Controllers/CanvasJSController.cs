using DotNetBatch14PKK.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.MVCApp.Controllers;

public class CanvasJSController : Controller
{
    {
        var model = new PieCanvasJsModel
        {
            Categories = new string[] { "Apples", "Bananas", "Oranges" },
            Data = new double[] { 10, 20, 30 }
        };
        return View(model);
    }

    {
        var model = new ColumnCanvasJsModel
        {
            Categories = new string[] { "January", "February", "March" },
            Data = new int[] { 100, 200, 300 }
        };
        return View(model);
    }

    {
        var model = new LineCanvasJsModel
        {
            Categories = new string[] { "2018", "2019", "2020" },
            Data = new int[] { 50, 60, 70 }
        };
        return View(model);
    }

    {
        var model = new AreaCanvasJsModel
        {
            Categories = new string[] { "Q1", "Q2", "Q3" },
            Data = new double[] { 5.0, 7.5, 6.0 }
        };
        return View(model);
    }

    {
        var model = new ScatterCanvasJsModel
        {
            Data = new double[][] {
                new double[] { 1.5, 2.3 },
                new double[] { 2.0, 1.8 },
                new double[] { 2.8, 3.0 }
            }
        };
        return View(model);
    }

    public IActionResult StepLineChart()
    {
        var model = new MultiSeriesStepLineChartModel
        {
            Series1 = new List<StepLineChartDataPoint>
                {
                    new StepLineChartDataPoint { X = new DateTime(2008, 2, 1), Y = 15.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 3, 1), Y = 14.50 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 4, 1), Y = 14.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 5, 1), Y = 14.50 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 6, 1), Y = 14.75 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 7, 1), Y = 16.30 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 8, 1), Y = 15.80 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 9, 1), Y = 17.50 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 10, 1), Y = 18.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 11, 1), Y = 18.50 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 12, 1), Y = 19.00 }
                },
            Series2 = new List<StepLineChartDataPoint>
                {
                    new StepLineChartDataPoint { X = new DateTime(2008, 2, 1), Y = 41.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 3, 1), Y = 42.50 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 4, 1), Y = 41.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 5, 1), Y = 45.30 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 6, 1), Y = 47.55 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 7, 1), Y = 45.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 8, 1), Y = 40.70 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 9, 1), Y = 37.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 10, 1), Y = 35.00 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 11, 1), Y = 34.50 },
                    new StepLineChartDataPoint { X = new DateTime(2008, 12, 1), Y = 33.00 }
                }
        };

        return View(model);
    }
}
