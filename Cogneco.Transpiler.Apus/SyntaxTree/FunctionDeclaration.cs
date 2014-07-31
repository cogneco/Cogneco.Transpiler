//
//  FunctionDeclaration.cs
//
//  Author:
//       Simon Mika <simon@mika.se>
//
//  Copyright (c) 2014 Simon Mika
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Kean.Extension;
using Collection = Kean.Collection;
using Kean.Collection.Extension;
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class FunctionDeclaration : Declaration
	{
		public IdentifierPattern Name { get; set; }
		public TuplePattern Arguments { get; set; }
		public Type ReturnType { get; set; }
		public Collection.IReadOnlyVector<Statement> Statements { get; set; }
		public FunctionDeclaration()
		{
		}
		public override string ToString()
		{
			Text.Builder result = "func ";
			result += this.Name;
			result += this.Arguments;
			if (this.ReturnType.NotNull())
				result += " -> " + this.ReturnType;
			result += " { " + this.Statements.Map(s => s.ToString()).Join("; ") + " }";
			return result;
		}
		#region Static Parse
		internal static FunctionDeclaration ParseFunctionDeclaration(Tokens.Lexer lexer)
		{
			var result = new FunctionDeclaration() { Region = lexer.Current.Region };
			if (!(lexer.Next() is Tokens.Identifier))
				new Exception.SyntaxError("function identifier", lexer).Throw();
			result.Name = new IdentifierPattern(lexer.Current as Tokens.Identifier);
			if (!(lexer.Next() is Tokens.LeftParenthesis))
				new Exception.SyntaxError("left parenthesis \"(\"", lexer).Throw();
			result.Arguments = TuplePattern.ParseTuplePattern(lexer);
			result.Statements = new Collection.ReadOnlyVector<Statement>(Statement.ParseCodeBlock(lexer).ToArray());
			return result;
		}
		#endregion
	}
}

