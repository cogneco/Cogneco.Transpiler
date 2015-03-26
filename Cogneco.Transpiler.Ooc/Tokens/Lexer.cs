//
//  Lexer.cs
//
//  Author:
//       Simon Mika <simon@mika.se>
//
//  Copyright (c) 2015 Simon Mika
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
using Kean;
using Kean.Extension;
using IO = Kean.IO;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public class Lexer : FrontEnd.Lexer<Token>
	{
		public override Token Next()
		{
			Token result = base.Next();
			if (result is WhiteSpace || result is Comment)
				result = this.Next();
			return result;
		}
		Lexer(Tokenizer tokenizer) : base(tokenizer)
		{
		}
		#region Static Open
		public static Lexer Open(IO.ICharacterReader reader)
		{
			return Lexer.Open(Tokenizer.Open(reader));
		}
		static Lexer Open(Tokenizer tokenizer)
		{
			return tokenizer.NotNull() ? new Lexer(tokenizer) : null;
		}
		#endregion
	}
}

