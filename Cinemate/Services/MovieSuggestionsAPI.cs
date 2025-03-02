using System.Text;
using Newtonsoft.Json.Linq;

namespace Cinemate.Services
{
    class MovieSuggestionsAPI
    {
        public static async Task<string> GetSuggestions(int noSuggestions, string favMovies, string genMovies, int noYears)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("https://bots.easy-peasy.ai/bot/604bf087-e526-4194-87a6-07a48db385ae/api"),
                        Content = new StringContent("{\"message\": \"Give me " + noSuggestions + " movie suggestions based on the following answears: My favourite movie(s) is/are: " + favMovies + ". I want to see a(n) " + genMovies + " movie that was released in the last " + noYears + " years.\", \"history\": [], \"stream\": true}", Encoding.UTF8, "application/json")
                    };

                    request.Headers.Add("x-api-key", "cd66dc4c-ae97-4118-81f2-767a64f458f6");

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (!response.IsSuccessStatusCode)
                    {
                        return "An error occurred while parsing the response.";
                    }

                    string responseBody = await response.Content.ReadAsStringAsync();

                    string searchString = "event: result\ndata: ";
                    int startIndex = responseBody.IndexOf(searchString);
                    if (startIndex != -1)
                    {
                        startIndex += searchString.Length;
                        int endIndex = responseBody.IndexOf("\n\n", startIndex);
                        string jsonText = responseBody.Substring(startIndex, endIndex - startIndex);

                        try
                        {
                            JObject parsedJson = JObject.Parse(jsonText);
                            return parsedJson["bot"]["text"].ToString(); ;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"JSON parsing error: {ex.Message}");
                            return "An error occurred while parsing the response.";
                        }
                    }
                    else
                    {
                        Console.WriteLine("The expected data format was not found in the response.");
                        return "An error occurred while parsing the response.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return "An error occurred while parsing the response.";
            }
        }
    }
}
