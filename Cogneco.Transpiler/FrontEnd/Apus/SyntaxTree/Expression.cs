//
//  Expression.cs
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

namespace Cogneco.Transpiler.FrontEnd.Apus.SyntaxTree
{
	public abstract class Expression : Statement
	{
		public abstract int Precedence { get; }
		public Type AssignedType { get; set; }
		public Type InferredType { get; set; }
		protected Expression()
		{
		}
		public string ToString(int precedence)
		{
			string result = this.ToStringHelper();
			if (precedence > this.Precedence)
				result = "(" + result + ")";
			return result + this.TypeString();
		}
		public sealed override string ToString()
		{
			return this.ToStringHelper() + this.TypeString();
		}
		string TypeString()
		{
			return this.AssignedType.NotNull() ? ": " + this.AssignedType : "";
		}
		protected abstract string ToStringHelper();
	}
}

