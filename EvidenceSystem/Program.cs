using EvidenceSystem.Business;
using EvidenceSystem.UI;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        IEvidenceService service = new EvidenceService();
        var ui = new ConsoleUI(service);
        await ui.StartAsync();
    }
}