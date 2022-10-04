using System.IO;
using RestSharp;

string userPath = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
if (Environment.OSVersion.Version.Major >= 6)
{
    userPath = Directory.GetParent(userPath).ToString();
}

//Sensitive info hidden away in gitignored key file
var client = new RestClient("http://api.openweathermap.org/data/2.5/forecast?id=" + WeatherDesktop.Key.loc + "&appid=" + WeatherDesktop.Key.key);
var request = new RestRequest();
RestResponse response = client.Execute(request);

if (!response.IsSuccessful)
{
    Console.WriteLine("Error with API endpoint!");
    Environment.Exit(0);
}

string secondPart = response.Content.ToString().Split("\"main\":\"")[1];
string weatherString = secondPart.Split("\"")[0];

Random rand = new Random();
string file = "\\Desktop\\Backgrounds\\" + weatherString + "\\" + rand.Next(0, 9) + ".jpg";

string fullPath = "file:///" + userPath + file;
Uri uri = new Uri(fullPath);

Wallpaper.Set(uri, Wallpaper.Style.Stretched);

Console.WriteLine(fullPath);