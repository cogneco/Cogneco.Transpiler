//
//  VariableDeclaration.cs
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
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class VariableDeclaration : Declaration
	{
		public Pattern Pattern { get; set; }
		readonly bool constant;
		public bool Constant { get { return this.constant; } }
		public VariableDeclaration(bool constant)
		{
			this.constant = constant;
		}
		internal override bool Write(Text.Indenter indenter)
		{
			return indenter.Write(this.Constant ? "let " : "var ") && this.Pattern.Write(indenter);
		}
		#region Static Parse
		internal static VariableDeclaration ParseVariableDeclaration(Tokens.Lexer lexer, bool constant, bool declarationOnly)
		{
			VariableDeclaration result;
			var region = lexer.Current.Region;
			lexer.Next();
			var pattern = Pattern.ParsePattern(lexer);
			if (lexer.Current.Is<Tokens.InfixOperator>(t => t.Symbol == "="))
			{
				if (declarationOnly)
					new Exception.SyntaxError("declaration only without definition", lexer).Throw();
				lexer.Next();
				result = new VariableDefinition(constant) {
					Region = region,
					Pattern = pattern,
					Expression = Expression.ParseExpression(lexer)
				};
			}
			else
				result = new VariableDeclaration(constant) { Region = region,  Pattern = pattern };
			return result;
		}
		#endregion
	}
}

