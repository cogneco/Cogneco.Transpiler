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
	public abstract class Parser<TToken, TResult> where TToken : class where TResult : class, new()
	{
		public TToken Current { get; private set; }
		Generic.IEnumerator<TToken> tokens;
		public TResult Result { get; private set; }
		public bool Empty { get { return this.Current.IsNull(); } }
		protected Parser()
		{
		}
		public virtual TToken Next()
		{
			return this.Current = (this.tokens.MoveNext() ? this.tokens.Current : null);
		}
		public TResult Parse(Uri.Locator resource)
		{
			TResult result;
			var lexer = this.OpenLexer(IO.CharacterReader.Open(resource));
			try
			{
				result = this.Parse(lexer);
			}
			finally
			{
				if (lexer is IDisposable)
					(lexer as IDisposable).Dispose();
			}
			return result;
		}
		protected abstract Generic.IEnumerable<TToken> OpenLexer(IO.ICharacterReader reader);
		public TResult Parse(Generic.IEnumerable<TToken> tokens)
		{
			return this.Parse(tokens.GetEnumerator());
		}
		public TResult Parse(Generic.IEnumerator<TToken> tokens)
		{
			this.tokens = tokens;
			this.Result = new TResult();
			this.Next();
			this.Parse();
			return this.Result;
		}
		protected abstract void Parse();
	}
}

