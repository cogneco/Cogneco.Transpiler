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
using Kean.Extension;
using Generic = System.Collections.Generic;
using Uri = Kean.Uri;
using IO = Kean.IO;
using Collection = Kean.Collection;

namespace Cogneco.Transpiler.FrontEnd.Apus.SyntaxTree
{
	public class Parser : FrontEnd.Parser<Tokens.Token, Statement>
	{
		public Parser()
		{
		}
		public override Tokens.Token Next()
		{
			Tokens.Token result = base.Next();
			if (result is Tokens.WhiteSpace || result is Tokens.Comment)
				result = this.Next();
			return result;
		}
		protected override Generic.IEnumerable<Tokens.Token> OpenLexer(IO.ICharacterReader reader)
		{
			return Tokens.Lexer.Open(reader);
		}
		protected override Generic.IEnumerable<Statement> Parse()
		{
			while (!this.Empty)
				if (this.Current is Tokens.Keyword)
				{
					switch ((this.Current as Tokens.Keyword).Name)
					{
						case Tokens.Keywords.Let:
							yield return VariableDeclaration.ParseVariableDeclaration(this, true);
							break;
						case Tokens.Keywords.Var:
							yield return VariableDeclaration.ParseVariableDeclaration(this, false);
							break;
						default:
							this.Next();
							break;
					}
				}
				else
					this.Next();
		}
	}
}

