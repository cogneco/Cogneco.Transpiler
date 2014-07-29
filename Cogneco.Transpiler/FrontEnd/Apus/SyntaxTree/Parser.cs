//
//  Parser.cs
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
using Generic = System.Collections.Generic;
using Uri = Kean.Uri;
using IO = Kean.IO;

namespace Cogneco.Transpiler.FrontEnd.Apus.SyntaxTree
{
	public class Parser : FrontEnd.Parser<Tokens.Token, Module>
	{
		public Parser()
		{
		}
		protected override Generic.IEnumerable<Tokens.Token> OpenLexer(IO.ICharacterReader reader)
		{
			return Tokens.Lexer.Open(reader);
		}
		protected override void Parse()
		{
			if (this.Current is Tokens.Keyword)
			{
				switch ((this.Current as Tokens.Keyword).Name)
				{
					case Tokens.Keywords.Var:
						var result = new VariableDeclaration() { Region = this.Current.Region };

						result.Pattern = this.ParsePattern();
						this.Next();
						result.Expression = this.ParseExpression();
						break;
				}
			}
		}
		protected Pattern ParsePattern()
		{
			return null;
		}
		protected Expression ParseExpression()
		{
			return null;
		}
	}
}

