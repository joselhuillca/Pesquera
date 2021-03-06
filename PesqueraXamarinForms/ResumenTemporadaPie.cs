﻿#region Copyright Syncfusion Inc. 2001 - 2014
// Copyright Syncfusion Inc. 2001 - 2014. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


#endregion
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PesqueraXamarinForms
{
	public class ResumenTemporadaPie : ContentPage
	{
		private StackLayout main_layout;
		private Picker pmenu_pesquera_;
		private Picker p_list_period;
		private Picker p_list_year;
		private Picker p_list_zone;
		private double exploratoria;
		private double temporada;
		private double saldo;

		public ResumenTemporadaPie()
		{
			GetChart ();
			this.Content = main_layout;
		}

		async void ShowMyPage(){
			pmenu_pesquera_.SelectedIndex = 0;
			await Navigation.PushAsync( new MyPage() ) ;
		}

		async void ShowPescaRegionColumn(){
			pmenu_pesquera_.SelectedIndex = 0;
			await Navigation.PushAsync( new PescaRegionColumn() ) ;
		}

		async void ShowPescaPuertoColumn(){
			pmenu_pesquera_.SelectedIndex = 0;
			await Navigation.PushAsync( new PescaPuertoColumn() ) ;
		}

		async void ShowPescaPlantaBar(){
			pmenu_pesquera_.SelectedIndex = 0;
			await Navigation.PushAsync( new PescaPlantaBar() ) ;
		}

		async void ShowPescaDiaColumnSpline(){
			pmenu_pesquera_.SelectedIndex = 0;
			await Navigation.PushAsync( new PescaDiaColumnSpline() ) ;
		}


		async public Task  getData () {
		
			var anio = int.Parse("2015");
			var zona = int.Parse("11");
			var per = int.Parse("1");

			//String url = "http://apptemporadapesca.produce.gob.pe/api/Grafico01?anotempo={0}&codigoZona={1}&periodo={2}"; 


			var url = string.Format("http://apptemporadapesca.produce.gob.pe/api/Grafico01?anotempo={0}&codigoZona={1}&periodo={2}", anio, zona, per);
			await GetWeatherAsync(url);

		}

		public async Task GetWeatherAsync(string url) {
			
			HttpClient client = new HttpClient();
			client.BaseAddress = new Uri(url);

			client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");

			var response =  await client.GetAsync(client.BaseAddress);
		    response.EnsureSuccessStatusCode();
			var JsonResult = response.Content.ReadAsStringAsync().Result;
			var weather = JsonConvert.DeserializeObject<dtoGrafico01[]>(JsonResult);
			if (weather.Length > 0)
				SetValues(weather[0]);
		}

		private void SetValues(dtoGrafico01 weather) {
			string stationName = weather.codigoZona;
			long elevation =  (long)weather.avancePorcentaje;
			long temperature = (long)weather.cuotaSaldo;
			long humidity = weather.anoTempo;

		//	StationName = stationName;
			//Elevation = elevation;
		//	Temperature = temperature;
		//	Humidity = humidity;
			exploratoria = weather.tmExploratoria;
			temporada = weather.tmTemporada;
			saldo = weather.cuotaSaldo;
		}


		private void GetChart()
		{

			getData ();

			/*
			main_layout.Orientation = StackOrientation.Vertical;
			main_layout.Spacing = 15;
			main_layout.VerticalOptions = LayoutOptions.FillAndExpand;
			main_layout.HorizontalOptions = LayoutOptions.FillAndExpand;
			*/

			SfChart chart = new SfChart() { Legend = new ChartLegend(){
					DockPosition = LegendPlacement.Bottom
				} };

			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
			datas.Add (new ChartDataPoint ("Exploratoria", 50));
			datas.Add (new ChartDataPoint ("Temporada", 30));
			datas.Add (new ChartDataPoint ("Saldo", 20));


			//PieSeries
			DoughnutSeries pie = new DoughnutSeries();

			//pie.ExplodeOnTouch = true;
			pie.ItemsSource = datas;
			pie.LegendIcon = ChartLegendIcon.Diamond;
			//pie.EnableDataPointSelection = true;
			//pie.DataMarker.ShowLabel = true;

			pie.DataMarkerPosition = CircularSeriesDataMarkerPosition.OutsideExtended;
			pie.ConnectorLineType = ConnectorLineType.Bezier;
			pie.DataMarker = new ChartDataMarker ();
			pie.DataMarker.ShowMarker = true;
			pie.DataMarker.MarkerWidth = 5;
			pie.DataMarker.LabelContent = LabelContent.Percentage;
			chart.Series.Add(pie);

			chart.VerticalOptions = LayoutOptions.FillAndExpand;
			chart.HorizontalOptions = LayoutOptions.FillAndExpand;


			////////////// Picker#
			/// 
			/// Picker Period
			p_list_period = new Picker
			{
				Title = "Periodo",
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] periodNameList = {"periodo 1", "periodo 2"};

			foreach (string periodName in periodNameList)
			{
				p_list_period.Items.Add(periodName);
			} 
			p_list_period.SelectedIndex = 0;

			/// Picker year
			p_list_year = new Picker
			{
				Title = "Año",
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] yearNameList = {"2010", "2011", "2012", "2013", "2014"};
			foreach (string yearName in yearNameList)
			{
				p_list_year.Items.Add(yearName);
			}
			p_list_year.SelectedIndex = 0;

			/// Picker zona
			p_list_zone = new Picker
			{
				Title = "Zona",
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] zoneNameList = {"Norte", "Sur", "Este", "Oeste"};
			foreach (string zoneName in zoneNameList)
			{
				p_list_zone.Items.Add(zoneName);
			}
			p_list_zone.SelectedIndex = 0;



			p_list_year.SelectedIndexChanged += (sender, args) =>
			{
				if (p_list_year.SelectedIndex == -1)
				{
					pie.ItemsSource = datas;
				}
				else
				{
					int num = p_list_year.SelectedIndex;
					ObservableCollection<ChartDataPoint> datas2 = new ObservableCollection<ChartDataPoint>();
					datas2.Add (new ChartDataPoint ("Exploratoria", 50 * (num + 1) ));
					datas2.Add (new ChartDataPoint ("Temporada", 30));
					datas2.Add (new ChartDataPoint ("Saldo", 20));

					pie.ItemsSource = datas2;

				}
			};

			pmenu_pesquera_ = GetMenuPesquera ();
			pmenu_pesquera_.VerticalOptions = LayoutOptions.Start;

		
			main_layout = new StackLayout (){
				Padding = GlobalParameters.MAIN_LAYOUT_PADDING_,
				Spacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Children = {
					pmenu_pesquera_,
					new Label(){
						Text = "AVANCE PESCA POR ZONA",
						HorizontalOptions = LayoutOptions.Center
					},
					new StackLayout(){
						Spacing = 0,
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation = StackOrientation.Horizontal,
						Children = {
							new Label(){
								Text = "Año: ",
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_
							},
							p_list_year,

							new StackLayout{
								Spacing = 0,
								VerticalOptions = LayoutOptions.Start,
								HorizontalOptions = LayoutOptions.CenterAndExpand,
								Orientation = StackOrientation.Horizontal,
								Children = {
									new Label(){
										Text = "Zona: ",
										FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_
									},
									p_list_zone
								}
							},
							new Label(){
								Text = "Periodo: ",
								FontSize = GlobalParameters.LABEL_TEXT_SIZE_15_	
							},
							p_list_period
						}
					},
					chart
				}
			};
		}


		private  Picker GetMenuPesquera(){
			Picker p_list_menu = new Picker
			{
				Title = "Cuadros",
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			String [] menuNameList = {"Avance pesca por zona",
				"Avance pesca por región",
				"Avance pesca por puerto",
				"Avance pesca por planta",
				"Avance pesca / descargas por día",
				"Avance pesca / descargas quincena",
				"Avance por grupos",
				"Avance por grupos en [Rango %]",
			};
			foreach (string menuName in menuNameList)
			{
				p_list_menu.Items.Add(menuName);
			}
			p_list_menu.SelectedIndex = 0;

			// WHEN p_list_menu is selected
			p_list_menu.SelectedIndexChanged += (sender, args) =>
			{ 
				if (p_list_menu.SelectedIndex == -1)
				{
				}
				else
				{
					switch(p_list_menu.SelectedIndex) 
					{
					case 1:
						ShowPescaRegionColumn();
						break;
					case 2:
						ShowPescaPuertoColumn();
						break;
					case 3:
						ShowPescaPlantaBar();
						break;
					case 4:
						ShowPescaDiaColumnSpline();
						break;
					case 5:
						ShowMyPage();
						break;
					}


				}
			};
			return p_list_menu;
		}
	}
}

