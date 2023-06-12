using System.Text.Json;
//using GoogleGson.Annotations;
using Holidays.Models;

namespace Holidays;

public partial class Holidays : ContentPage
{
	HttpClient _client;
	JsonSerializerOptions _serializerOptions;
	Root root;

	public Holidays()
	{
		InitializeComponent();
        _client = new HttpClient();
		_serializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = true
		};

		pcrYear.SelectedItem = "2023";
		pcrProvince.SelectedItem = "All";

    }

	

	public async Task<Root> RefreshDataAsync(string year, string province)
	{
		root = new Root();
		string provinceCode = GetProvinceCode(province);
		Holiday holiday = new Holiday();
		Uri apiUri = new Uri(string.Format("https://canada-holidays.ca/api/v1/holidays?year={0}&optional=false",year));
		try
		{
			HttpResponseMessage response = await _client.GetAsync(apiUri);
			if(response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				root = JsonSerializer.Deserialize<Root>(content, _serializerOptions);

				if(!string.IsNullOrEmpty(provinceCode))
				{
					Root filteredRoot = new Root();
					foreach (var item in root.holidays)
					{
						foreach(var p in item.provinces)
						{
							if (p.id.Equals(provinceCode))
							{
								filteredRoot.holidays.Add(item);
								break;
							}
							else
								continue;
						}
						continue;

					}
					//var result = (from c in root.holidays
					//			 from p in c.provinces
					//			 where p.id.Equals(provinceCode) select root).FirstOrDefault();
					return filteredRoot;
								 
				}
			}

		}
		catch(Exception ex)
		{

		}
		return root;
    }

	

	private string GetProvinceCode(string provinceName)
	{
		string provinceCode = string.Empty;
		switch (provinceName.Trim())
		{
			case "Alberta":
				provinceCode = "AB";
				break;
			case "British Columbia":
                provinceCode = "BC";
                break;
			case "Manitoba":
				provinceCode = "MB";
				break;
			case "New Brunswick":
				provinceCode = "NB";
				break;
			case "Newfoundland and Labrador":
                provinceCode = "NL";
                break;
			case "Northwest Territories":
				provinceCode = "NT";
				break;
			case "Nova Scotia":
				provinceCode = "NS";
				break;
			case "Nunavut":
                provinceCode = "NU";
                break;
			case "Ontario":
                provinceCode = "ON";
                break;
			case "Prince Edward Island":
                provinceCode = "PE";
                break;
			case "Quebec":
                provinceCode = "QC";
                break;
			case "Saskatchewan":
                provinceCode = "SK";
                break;
			case "Yukon":
                provinceCode = "YT";
                break;
			default:
				break;
		}

		return provinceCode;
	}

    private async void btnGetHolidays_Clicked(object sender, EventArgs e)
    {
		string provinceName = pcrProvince.SelectedItem.ToString();
		string year = pcrYear.SelectedItem.ToString();
        var data = await RefreshDataAsync(year, provinceName);
		lstvwData.ItemsSource = data.holidays;



    }
}