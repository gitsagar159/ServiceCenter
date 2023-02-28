using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceCenter.Models
{
    public class DashboardModel
    {
    }


    public class JobDashboard
    {
        public long TotalJobCount { get; set; }
        public long JobDoneCount { get; set; }
        public long OpenJobCount { get; set; }
        public long NewJobCount { get; set; }

        public MonthModel CurrentYearData { get; set; }
        public MonthModel PreviousYearData { get; set; }

    }


    #region Chart


    public class MonthModel
    {
        public int January { get; set; }
        public int February { get; set; }
        public int March { get; set; }
        public int April { get; set; }
        public int May { get; set; }
        public int June { get; set; }
        public int July { get; set; }
        public int August { get; set; }
        public int September { get; set; }
        public int October { get; set; }
        public int November { get; set; }
        public int December { get; set; }

    }

    public class MonthlyJobCountByYear
    {
        public string Year { get; set; }
        public List<MonthModel> MonthlyJobCount { get; set; }
    }

    public class JobCharStatesticModel
    {
        public List<MonthlyJobCountByYear> JobChartStatestic { get; set; }
    }



    public class ChartData
    {
        public List<string> labels { get; set; }
        public List<ChartDataset> datasets { get; set; }
    }

    public class ChartDataset
    {
        public string label { get; set; }
        public List<int> data { get; set; }
        public int borderWidth { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public List<int> borderDash { get; set; }
    }

    public class GridLines
    {
        public bool display { get; set; }
    }

    public class Options
    {
        public Scales scales { get; set; }
        public bool responsive { get; set; }
        public bool maintainAspectRatio { get; set; }
    }

    public class Chart
    {
        public string type { get; set; }
        public ChartData data { get; set; }
        public Options options { get; set; }
    }

    public class Scales
    {
        public List<Axis> xAxes { get; set; }
        public List<Axis> yAxes { get; set; }
    }

    public class Ticks
    {
        public string fontColor { get; set; }
    }

    public class Axis
    {
        public GridLines gridLines { get; set; }
        public Ticks ticks { get; set; }
    }

    public class ChartColor
    {
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
    }

    public class ChartColorListModel
    {
        public List<ChartColor> ChartColorList { get; set; }

        public List<ChartColor> GetChartColorList()
        {
            List<ChartColor> ChartColor = new List<ChartColor>();

            ChartColor.Add( new ChartColor() { backgroundColor = "rgba(28,180,255,.05)", borderColor = "rgba(28,180,255,1)" });
            ChartColor.Add(new ChartColor() { backgroundColor = "rgba(28,180,111,.05)", borderColor = "rgba(28,180,111,1)" });

            return ChartColor;

        }

    }


    #endregion
}