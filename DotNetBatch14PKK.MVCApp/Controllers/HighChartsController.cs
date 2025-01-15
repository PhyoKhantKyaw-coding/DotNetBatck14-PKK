using DotNetBatch14PKK.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.MVCApp.Controllers;

public class HighChartsController : Controller
{
    public IActionResult PieHighChart()
    {
        var model = new PieHighChartModel
        {
            Categories = new string[] { "Apples", "Bananas", "Oranges" },
            Data = new double[] { 10, 20, 30 }
        };
        return View(model);
    }

    public IActionResult ColumnHighChart()
    {
        var model = new ColumnHighChartModel
        {
            Categories = new string[] { "January", "February", "March" },
            Data = new int[] { 100, 200, 300 }
        };
        return View(model);
    }

    public IActionResult LineHighChart()
    {
        var model = new LineHighChartModel
        {
            Categories = new string[] { "2018", "2019", "2020" },
            Data = new int[] { 50, 60, 70 }
        };
        return View(model);
    }

    public IActionResult AreaHighChart()
    {
        var model = new AreaHighChartModel
        {
            Categories = new string[] { "Q1", "Q2", "Q3" },
            Data = new double[] { 5.0, 7.5, 6.0 }
        };
        return View(model);
    }

    public IActionResult ScatterHighChart()
    {
        var model = new ScatterHighChartModel
        {
            Data = new double[][] {
                new double[] { 1.5, 2.3 },
                new double[] { 2.0, 1.8 },
                new double[] { 2.8, 3.0 }
            }
        };
        return View(model);
    }
}
