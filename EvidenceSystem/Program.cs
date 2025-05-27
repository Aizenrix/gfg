using EvidenceSystem;
using EvidenceSystem.UI;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var service = new EvidenceService();
        var ui = new ConsoleUI(service);
        await ui.StartAsync();
    }
}