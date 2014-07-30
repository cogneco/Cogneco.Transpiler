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

namespace Cogneco.Transpiler.FrontEnd
{
	public abstract class Parser<TToken, TResult> where TToken : class
	{
		public TToken Current { get; private set; }
		Generic.IEnumerator<TToken> tokens;
		public bool Empty { get { return this.Current.IsNull(); } }
		protected Parser()
		{
		}
		public virtual TToken Next()
		{
			return this.Current = (this.tokens.MoveNext() ? this.tokens.Current : null);
		}
		public Generic.IEnumerable<TResult> Parse(Uri.Locator resource)
		{
			var lexer = this.OpenLexer(IO.CharacterReader.Open(resource));
			try
			{
				foreach (var result in this.Parse(lexer))
					yield return result;
			}
			finally
			{
				if (lexer is IDisposable)
					(lexer as IDisposable).Dispose();
			}
		}
		protected abstract Generic.IEnumerable<TToken> OpenLexer(IO.ICharacterReader reader);
		public Generic.IEnumerable<TResult> Parse(Generic.IEnumerable<TToken> tokens)
		{
			return this.Parse(tokens.GetEnumerator());
		}
		public Generic.IEnumerable<TResult> Parse(Generic.IEnumerator<TToken> tokens)
		{
			this.tokens = tokens;
			this.Next();
			return this.Parse();
		}
		protected abstract Generic.IEnumerable<TResult> Parse();
	}
}

