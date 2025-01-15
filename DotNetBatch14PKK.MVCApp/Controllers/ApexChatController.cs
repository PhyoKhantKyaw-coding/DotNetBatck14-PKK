using DotNetBatch14PKK.MVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.MVCApp.Controllers;

public class ApexChatController : Controller
{
    public IActionResult PieChat()
    {
        PieChartModel model = new PieChartModel
        {
            Series = new int[] { 44, 55, 13, 43, 22 },
            Labels = new string[] { "Team A", "Team B", "Team C", "Team D", "Team E" }
        };
        return View(model);
    }

    public IActionResult LineChart()
    {
        LineChartModel model = new LineChartModel
        {
            Series = new int[] { 10, 41, 35, 51, 49, 62, 69, 91, 148 },
            Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" }
        };
        return View(model);
    }

    public IActionResult BarChart()
    {
        BarChartModel model = new BarChartModel
        {
            Series = new int[] { 30, 40, 45, 50, 49, 60, 70, 91, 125 },
            Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" }
        };
        return View(model);
    }

    public IActionResult MixedChart()
    {
        MixedChartModel model = new MixedChartModel
        {
            Series1 = new int[] { 44, 55, 41, 37, 22, 43, 21, 49 },
            Series2 = new int[] { 23, 33, 32, 27, 21, 29, 19, 33 },
            Labels = new string[] { "Q1", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8" }
        };
        return View(model);
    }

    public IActionResult FunnelChart()
    {
        FunnelChartModel model = new FunnelChartModel
        {
            Series = new int[] { 120, 100, 80, 60, 40, 20 },
            Labels = new string[] { "Step 1", "Step 2", "Step 3", "Step 4", "Step 5", "Step 6" }
        };
        return View(model);
    }
}
