//
//  Identifier.cs
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
using Text = Kean.IO.Text;
using Kean.Extension;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class Identifier : Expression
	{
		public override int Precedence { get { return 500; } }
		public Identifier Parent { get; set; }
		public Declaration Declaration { get; set; }
		readonly string name;
		public string Name { get { return this.name; } }
		public Identifier(string name)
		{
			this.name = name;
		}
		protected override bool WriteHelper(Text.Indenter indenter)
		{
			return (this.Parent.IsNull() || this.Parent.Write(indenter) && indenter.Write(".")) && indenter.Write(this.Name);
		}
		#region Static Parse
		internal static Identifier ParseIdentifier(Tokens.Lexer lexer)
		{
			Tokens.Identifier current = lexer.Current as Tokens.Identifier;
			lexer.Next();
			Identifier result = null;
			if (lexer.Current is Tokens.LeftParenthesis)
				;// function call do ParseTupleExpression
			else if (lexer.Current is Tokens.InfixOperator && (lexer.Current as Tokens.InfixOperator).Symbol == ".")
			{
				if (!(lexer.Next() is Tokens.Identifier))
					new Exception.SyntaxError("identfifier following dot operator \".\"", lexer).Throw();
				result = Identifier.ParseIdentifier(lexer);
				result.Parent = new Identifier(current.Name) { Region = current.Region };
			}
			else
				result = new Identifier(current.Name) { Region = current.Region };
			return result;
		}
		#endregion
	}
}

