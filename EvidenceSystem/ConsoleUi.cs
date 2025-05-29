using System;
using System.Threading.Tasks;
using EvidenceSystem.Business;

namespace EvidenceSystem.UI
{
    public class ConsoleUI
    {
        private readonly IEvidenceService _evidenceService;

        public ConsoleUI(IEvidenceService service)
        {
            _evidenceService = service;
        }

        public async Task StartAsync()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Система учета доказательств ===");
                Console.WriteLine("1. Добавить доказательство");
                Console.WriteLine("2. Показать все доказательства");
                Console.WriteLine("3. Найти по ID");
                Console.WriteLine("4. Удалить по ID");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                string input = Console.ReadLine();

                try
                {
                    switch (input)
                    {
                        case "1":
                            await AddEvidence();
                            break;
                        case "2":
                            await ShowAllEvidence();
                            break;
                        case "3":
                            await FindEvidence();
                            break;
                        case "4":
                            await DeleteEvidence();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("❌ Неверный выбор. Нажмите Enter.");
                            Console.ReadLine();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ОШИБКА] {ex.Message}");
                    Console.ReadLine();
                }
            }
        }

        private async Task AddEvidence()
        {
            Console.Write("Название: ");
            string name = Console.ReadLine();
            Console.Write("Описание: ");
            string desc = Console.ReadLine();
            await _evidenceService.AddEvidenceAsync(name, desc);
            Console.WriteLine("✅ Добавлено. Нажмите Enter.");
            Console.ReadLine();
        }

        private async Task ShowAllEvidence()
        {
            var all = await _evidenceService.GetAllEvidenceAsync();
            foreach (var ev in all)
                Console.WriteLine($"ID: {ev.Id} | {ev.Name} - {ev.Description}");
            Console.WriteLine("Нажмите Enter.");
            Console.ReadLine();
        }

        private async Task FindEvidence()
        {
            Console.Write("ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var ev = await _evidenceService.GetByIdAsync(id);
                Console.WriteLine(ev != null
                    ? $"Найдено: {ev.Name} - {ev.Description}"
                    : "❌ Не найдено.");
            }
            else
                Console.WriteLine("❌ Неверный ID.");
            Console.ReadLine();
        }

        private async Task DeleteEvidence()
        {
            Console.Write("ID для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                bool ok = await _evidenceService.DeleteByIdAsync(id);
                Console.WriteLine(ok ? "✅ Удалено." : "❌ Не найдено.");
            }
            else
                Console.WriteLine("❌ Неверный ввод.");
            Console.ReadLine();
        }
    }
}