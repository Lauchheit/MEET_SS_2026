using DDI_Checker.Infrastructure;

var client = new DataClient();


client.FetchAll();

Console.WriteLine("=== Warfarin + Aspirin ===");
var results = client.SearchInteractionBetweenDrugs("Chlorpromazine","Cocaine");
foreach (var i in results)
    Console.WriteLine(i + "\n");