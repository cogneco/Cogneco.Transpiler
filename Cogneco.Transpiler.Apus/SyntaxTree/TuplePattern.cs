//
//  TuplePattern.cs
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
using Collection = Kean.Collection;
using Kean.Extension;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class TuplePattern : Pattern
	{
		readonly Collection.IList<Pattern> items = new Collection.List<Pattern>();
		public Collection.IList<Pattern> Items { get { return this.items; } }
		public TuplePattern()
		{
		}
		public override string ToString()
		{
			return "(" + this.Items.Map(item => item.ToString()).Join(", ") + ")";
		}
		#region Static Parse
		internal static TuplePattern ParseTuplePattern(Tokens.Lexer lexer)
		{
			if (!(lexer.Current is Tokens.LeftParenthesis))
				new Exception.SyntaxError("left parenthesis \"(\"", lexer).Throw();
			TuplePattern result = new TuplePattern { Region = lexer.Current.Region };
			if (!(lexer.Next() is Tokens.RightParenthesis))
			{
				do
					result.Items.Add(Pattern.ParsePattern(lexer));
				while (lexer.Current is Tokens.Comma && lexer.Next().NotNull());
				if (!(lexer.Current is Tokens.RightParenthesis))
					new Exception.SyntaxError("right parenthesis \")\"", lexer).Throw();
			}
			lexer.Next();
			return result;
		}
		#endregion
	}
}

