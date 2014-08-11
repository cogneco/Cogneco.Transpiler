//
//  Sope.cs
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
using Kean;
using Kean.Extension;
using Collection = Kean.Collection;
using Uri = Kean.Uri;
using Text = Kean.IO.Text;
using Generic = System.Collections.Generic;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class Sope
	{
		readonly Collection.IDictionary<string, Collection.IList<IdentifierPattern>> map = new Collection.Dictionary<string, Collection.IList<IdentifierPattern>>();
		readonly Scope parent;
		public Sope(Scope parent)
		{
			this.parent = parent;
		}
		public Sope() : this(null)
		{
		}
		public void Register(string name, IdentifierPattern declaration)
		{
			this.map[name] = (this.map[name] ?? new Collection.List<IdentifierPattern>()).Add(declaration);
		}

	}
}

