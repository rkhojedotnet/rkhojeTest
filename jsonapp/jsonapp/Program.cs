// See https://aka.ms/new-console-template for more information
using JSONApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

try
{


    string path = @"c:\temp\usagedata.json";


    using (StreamReader file = new StreamReader(path))
    {
        string jsonString = File.ReadAllText(path);
        if (jsonString != string.Empty)
        {
            Usage[]? dataObject = JsonConvert.DeserializeObject<Usage[]>(jsonString);

            if (dataObject != null)
            {
                foreach (Usage U in dataObject)
                {
                    Console.WriteLine("Customer Id " + U.CustomerId + " API Calls " + U.aPI_Calls.ToString() + " Storage GB " + U.Storage_GB.ToString() + " Compute Minutes " + U.Compute_Minutes.ToString());
                }

                Console.WriteLine("---------------------------------------------------------------------------------------------------------");

                var dataObject1 = dataObject.GroupBy(s => s.CustomerId)
                                .Select(g => new
                                {
                                    customer = g.Key,
                                    TotalCallsPrice = (g.Sum(s => s.aPI_Calls.All(char.IsDigit) == true ? int.Parse(s.aPI_Calls) : 0 ) <= 10000) ? (g.Sum(s => s.aPI_Calls.All(char.IsDigit) == true ? int.Parse(s.aPI_Calls) : 0) * 0.01) : (0.008 * g.Sum(s => s.aPI_Calls.All(char.IsDigit) == true ? int.Parse(s.aPI_Calls) : 0)),
                                    TotalStoragePrice = (g.Sum(s => s.Storage_GB) * 0.25),
                                    TotalComputeTime = (g.Sum(s => s.Compute_Minutes) * 0.05)
                                });

                foreach (var grp in dataObject1)
                {
                    Console.WriteLine("Customer :" + grp.customer + " API Calls :" + grp.TotalCallsPrice.ToString() + " Storage GB :" + grp.TotalStoragePrice.ToString() + " Call Minutes : " + grp.TotalComputeTime.ToString());
                }


                Console.ReadLine();
            }
        }

    }
}
catch(Exception ex)
{
    Console.WriteLine(ex);
    Console.ReadLine();

}

