using System.IO;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.GameService;

namespace SudokuSolution.Console.ConsoleGame {
	[UsedImplicitly]
	public class ConsoleGameProvider : IConsoleGameProvider {
		private const int MaxSolved = 100;

		private readonly IGameService gameService;

		public ConsoleGameProvider(IGameService gameService) {
			this.gameService = gameService;
		}

		public void Start(string pathToFile) {
			if (pathToFile.IsNullOrEmpty() || !File.Exists(pathToFile)) {
				System.Console.WriteLine("Для запуска перенесите файл содержащий поле на исполняемой файл. " +
					"В файле перечислите все числа на поле, где 0 отметьте пустые клетки. Для игры 4х4 должно быть 4 строки в файле. Для игры 9х9 - 9 строк.");
				return;
			}

			var lines = File.ReadAllLines(pathToFile).Where(line => line.IsSignificant()).ToArray();
			if (lines.Length != 1 && lines.Length != 4 && lines.Length != 9 && lines.Length != 16 && lines.Length != 25) {
				System.Console.WriteLine("Некорректное число строк. Допустимые размеры игр: 1х1, 4х4, 9х9, 16х16, 25х25");
				return;
			}

			var field = new Field(lines.Length);
			Enumerable.Range(0, field.MaxValue)
				.ForEach(row => lines[row].Split(' ', ',', '\t', '-')
					.Where(valueString => valueString.IsSignificant())
					.Select((valueString, column) => new { Value = int.TryParse(valueString, out var value) ? value : 0, Column = column })
					.Where(group => group.Value != 0)
					.ForEach(group => field.Cells[row, group.Column].Final = group.Value));

			var solvedFields = gameService.Solve(field).Take(MaxSolved).ToArray();

			switch (solvedFields.Length) {
				case 0:
					System.Console.WriteLine("У заданного поля нет решений");
					return;
				case > 1:
					System.Console.WriteLine("У заданного поля множество решений");
					break;
			}

			solvedFields.ForEach(solvedField => System.Console.WriteLine(FieldToString(solvedField)));

			System.Console.WriteLine("Введите путь к файлу для сохранения результата, либо пустую строку, чтобы не сохранять");
			System.Console.Write("=> ");
			var pathToSave = System.Console.ReadLine();

			if (pathToSave.IsNullOrWhiteSpace())
				return;

			File.WriteAllLines(pathToSave, solvedFields.Select(FieldToString));
		}

		private static string FieldToString(Field field) {
			var stringBuilder = new StringBuilder();

			Enumerable.Range(0, field.MaxValue)
				.Select(row => field.Cells.SelectRow(row).Select(cell => cell.HasFinal ? cell.Final : 0).ToArray())
				.ForEach(x => stringBuilder.AppendLine(string.Join(" ", x)));

			return stringBuilder.ToString();
		}
	}
}