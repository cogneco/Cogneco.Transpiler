//
//  TupleExpression.cs
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
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class TupleExpression : Expression
	{
		public override int Precedence { get { return 500; } }
		readonly Collection.IList<Expression> items = new Collection.List<Expression>();
		public Collection.IList<Expression> Items { get { return this.items; } }
		public TupleExpression()
		{
		}
		protected override bool WriteHelper(Text.Indenter indenter)
		{
			return indenter.Write("(") &&
			this.Items.All(item => item.Write(indenter) && indenter.Write(", ")) &&
			indenter.Write(")");
		}
	}
}

