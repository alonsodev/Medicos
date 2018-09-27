using Medicos.ViewModels;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Medicos.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PacienteGraficoPage : ContentPage
	{
		public PacienteGraficoPage ()
		{
			InitializeComponent ();
            var bc = (this.BindingContext as MainViewModel);
            /*SfChart chart = new SfChart();
            chart.Title.Text = "Chart";

            //Initializing primary axis
            CategoryAxis primaryAxis = new CategoryAxis();
            primaryAxis.Title.Text = "Mes";
            chart.PrimaryAxis = primaryAxis;

            //Initializing secondary Axis
            NumericalAxis secondaryAxis = new NumericalAxis();
            secondaryAxis.Title.Text = "Cantidad";
            chart.SecondaryAxis = secondaryAxis;

            //Initializing column series
            ColumnSeries series = new ColumnSeries();
            series.ItemsSource = bc.PacienteGrafico.Data;
            series.XBindingPath = "Mes";
            series.YBindingPath = "Cantidad";
            series.Label = "Cantidades";

            series.DataMarker = new ChartDataMarker();
            series.EnableTooltip = true;
            chart.Legend = new ChartLegend();

            chart.Series.Add(series);
            this.Content = chart;

            */
            //sfcPacientesCS.ItemsSource = bc.PacienteGrafico.Data;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vMainViewModel = MainViewModel.GetInstance();
            vMainViewModel.checkExistsSession();
        }
    }
}