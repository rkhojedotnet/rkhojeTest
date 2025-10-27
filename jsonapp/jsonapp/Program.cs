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

                var dataObject1 = dataObject.Select(g => new
                {
                    customer = g.CustomerId,
                    CallsPrice = g.aPI_Calls.All(char.IsDigit) == false ? 0 : int.Parse(g.aPI_Calls),
                    StoragePrice = g.Storage_GB,
                    ComputeTime = g.Compute_Minutes
                });


                foreach (var grp in dataObject1)
                {
                    Console.WriteLine("Customer :" + grp.customer + " API Calls :" + grp.CallsPrice.ToString() + " Storage GB :" + grp.StoragePrice.ToString() + " Call Minutes : " + grp.ComputeTime.ToString());
                }

                var dataObject2 = dataObject1.GroupBy(s => s.customer)
                 .Select(g => new
                 {
                     customer = g.Key,
                     TotalCallsPrice = (g.Sum(s => s.CallsPrice) <= 10000 ? g.Sum(s => s.CallsPrice) * 0.01 : g.Sum(s => s.CallsPrice) * 0.08),
                     TotalStoragePrice = (g.Sum(s => s.StoragePrice) * 0.25),
                     TotalComputeTime = (g.Sum(s => s.ComputeTime) * 0.05)
                 });

                Console.WriteLine("---------------------------------------------------------------------------------------------------------");



                foreach (var grp in dataObject2)
                {
                    Console.WriteLine("Customer ID :" + grp.customer + " Total API Calls price :" + grp.TotalCallsPrice.ToString() + " Total Storage GB price :" + grp.TotalStoragePrice.ToString() + " Total Call Minutes price : " + grp.TotalComputeTime.ToString());
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

