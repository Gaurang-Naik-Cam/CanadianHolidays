using System.Text.Json;
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

         RefreshDataAsync();

    }

	public async Task<Root> RefreshDataAsync()
	{
		//holidays = new List<Holiday>();
		root = new Root();
		Holiday holiday = new Holiday();
		Uri apiUri = new Uri("https://canada-holidays.ca/api/v1/holidays?year=2021&optional=false");
		try
		{
			HttpResponseMessage response = await _client.GetAsync(apiUri);
			if(response.IsSuccessStatusCode)
			{
				string content = await response.Content.ReadAsStringAsync();
				root = JsonSerializer.Deserialize<Root>(content, _serializerOptions);

			}

		}
		catch(Exception ex)
		{

		}
		return root;
    }

}