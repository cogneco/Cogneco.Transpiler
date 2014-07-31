//
//  Lexer.cs
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
	public abstract class Lexer<TToken> where TToken : class
	{
		TToken current;
		public TToken Current
		{ 
			get { return this.current; }
			private set
			{
				if (this.current.NotNull())
					this.Last = this.current;
				this.current = value;
			}
		}
		public TToken Last { get; private set; }
		Generic.IEnumerator<TToken> backend;
		public bool Empty { get { return this.Current.IsNull(); } }
		protected Lexer(Generic.IEnumerable<TToken> backend)
		{
			this.backend = backend.GetEnumerator();
			this.Next();
		}
		public virtual TToken Next()
		{
			return this.Current = (this.backend.MoveNext() ? this.backend.Current : null);
		}
		public override string ToString()
		{
			return string.Format("[Lexer: Current={0}, Last={1}, Empty={2}]", this.Current, this.Last, this.Empty);
		}
	}
}

