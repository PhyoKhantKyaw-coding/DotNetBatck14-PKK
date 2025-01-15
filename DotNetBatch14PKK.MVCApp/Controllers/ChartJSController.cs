using Microsoft.AspNetCore.Mvc;
using DotNetBatch14PKK.MVCApp.Models;

namespace DotNetBatch14PKK.MVCApp.Controllers;

public class ChartJSController : Controller
{
    public IActionResult LineChart()
    {
        LineChartJSModel model = new LineChartJSModel
        {
            Data = new int[] { 65, 59, 80, 81, 56, 55, 40 },
            Labels = new string[] { "January", "February", "March", "April", "May", "June", "July" }
        };
        return View(model);
    }

    public IActionResult BarChart()
    {
        BarChartJSModel model = new BarChartJSModel
        {
            Data = new int[] { 12, 19, 3, 5, 2, 3 },
            Labels = new string[] { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" }
        };
        return View(model);
    }

    public IActionResult PieChart()
    {
        PieChartJSModel model = new PieChartJSModel
        {
            Data = new int[] { 12, 19, 3, 5, 2, 3 },
            Labels = new string[] { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" }
        };
        return View(model);
    }

    public IActionResult RadarChart()
    {
        RadarChartJSModel model = new RadarChartJSModel
        {
            Data = new int[] { 20, 10, 4, 2, 8, 5 },
            Labels = new string[] { "Eating", "Drinking", "Sleeping", "Designing", "Coding", "Cycling", "Running" }
        };
        return View(model);
    }

    public IActionResult PolarAreaChart()
    {
        PolarAreaChartJSModel model = new PolarAreaChartJSModel
        {
            Data = new int[] { 11, 16, 7, 3, 14 },
            Labels = new string[] { "Red", "Green", "Yellow", "Grey", "Blue" }
        };
        return View(model);
    }
}
