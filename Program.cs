using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.ComponentModel;

namespace City_Finder_Challenge
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            City currentCity;

            bool geekyResponse = false;

            Console.WriteLine("Want full http response? (Y/N)");
            //this is to get more info out of response
            if (Console.ReadLine().ToLower().Contains('y')) geekyResponse = true;

            string[] inputArr = new string[2];

            Console.WriteLine("Please use current formatting: {country} {zip/postalcode}");

            Console.WriteLine("Example:\nUS 33020\nthis will return hollywood\n");

            while (true)
            {
                Console.WriteLine("Enter country and zip/postal code");

                string input = Console.ReadLine();
                //validate answer
                if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Value inserted not recognized, try again\n");
                    continue;
                }

                int i = 0;

                inputArr = new string[2];
                //weird stuff that transforms the string input into string inputArr[2]
                foreach (char letter in input)
                {
                    if (letter == ' ')
                    {
                        i++;
                        if (i == 2)
                        {
                            break;
                        }
                        continue;
                    }
                    inputArr[i] += letter;
                }
                //more answer validation
                if (!int.TryParse(inputArr[1], out _))
                {
                    Console.WriteLine("Value inserted not recognized, try again\n");
                    continue;
                }
                //more answer validation
                if (inputArr[0].Length > 2)
                {
                    Console.WriteLine("Value inserted not recognized, try again\n");
                    continue;
                }
                //this is where we make http request
                try
                {
                    string requestUri = $"http://api.zippopotam.us/{inputArr[0].ToLower()}/{inputArr[1]}";

                    HttpResponseMessage response = await client.GetAsync(requestUri);
                    //this helps to throw exception
                    response.EnsureSuccessStatusCode();
                    //response from server
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Deserialize it
                    currentCity = JsonSerializer.Deserialize<City>(responseBody,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"There has been an exception, more details: {e}");
                    continue;
                }
                if (geekyResponse)
                {
                    //this is for those that want full answer
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(currentCity))
                    {
                        string name = descriptor.Name;

                        object value = descriptor.GetValue(currentCity);

                        if (value.GetType() == currentCity.Places.GetType())
                        {
                            Console.WriteLine("\n");

                            foreach (var item in (List<Place>)value)
                            {
                                foreach (PropertyDescriptor ddescriptor in TypeDescriptor.GetProperties(item))
                                {
                                    string nname = ddescriptor.Name;

                                    object vvalue = ddescriptor.GetValue(item);

                                    Console.WriteLine("{0}: {1}", nname, vvalue);
                                }
                                Console.WriteLine("\n");
                            }
                        }
                        Console.WriteLine("{0}: {1}", name, value);
                    }
                    Console.WriteLine("\n");
                }
                else
                {
                    //this is for those that just want city and state
                    Console.WriteLine($"There are {currentCity.Places.Count} cities");

                    foreach (var item in currentCity.Places)
                    {
                        Console.WriteLine($"City name: {item.PlaceName} in the state of: {item.State}");
                    }
                    Console.WriteLine("\n");
                }
            }
        }
    }
}