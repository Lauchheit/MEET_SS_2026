using DDI_Checker.Infrastructure;

var drugBankClient = new DataClient<DrugBankInteraction>(
    line => new DrugBankInteraction(line));
drugBankClient.Fetch(DrugBankInteraction.FILE_PATH);

var oncHighClient = new DataClient<OncHighPriorityInteraction>(
    line => new OncHighPriorityInteraction(line));
oncHighClient.Fetch(OncHighPriorityInteraction.FILE_PATH);

var oncLowClient = new DataClient<OncNonInterruptiveInteraction>(
    line => new OncNonInterruptiveInteraction(line));
oncLowClient.Fetch(OncNonInterruptiveInteraction.FILE_PATH);

// Ausgabe erste 10 Zeilen je Client
Console.WriteLine("=== DrugBank ===");
foreach (var i in drugBankClient.Interactions.Take(10))
    Console.WriteLine(i + "\n");

Console.WriteLine("=== ONC High Priority ===");
foreach (var i in oncHighClient.Interactions.Take(10))
    Console.WriteLine(i + "\n");

Console.WriteLine("=== ONC Non-Interruptive ===");
foreach (var i in oncLowClient.Interactions.Take(10))
    Console.WriteLine(i + "\n");