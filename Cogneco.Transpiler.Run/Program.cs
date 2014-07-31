using System;
using Uri = Kean.Uri;
using Argument = Kean.Cli.Argument;
using IO = Kean.IO;
using Kean.Extension;

namespace Cogneco.Transpiler.Run
{
	class Program
	{
		public static void Main(string[] arguments)
		{
			Uri.Locator program = null;
			var argumentParser = new Argument.Parser();
			argumentParser.UnassociatedParameterHandler = argument => program = argument;
			argumentParser.Parse(arguments);
			var parser = new Apus.SyntaxTree.Parser();
			foreach (var statement in parser.Parse(program))
				Console.Write(statement);
		}
	}
}
