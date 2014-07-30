//
//  Pattern.cs
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
	public abstract class Pattern : Node
	{
		public Type AssignedType { get; set; }
		public Type InferredType { get; set; }
		protected Pattern()
		{
		}
		protected string TypeString()
		{
			return this.AssignedType.NotNull() ? ": " + this.AssignedType : "";
		}
		#region Static Parse
		internal static Pattern ParsePattern(Parser parser)
		{
			Pattern result;
			if (parser.Current is Tokens.Identifier)
				result = new IdentifierPattern(parser.Current as Tokens.Identifier);
			//			else if (this.Current is Tokens.LeftParenthesis)
			//			{
			//				// FIXME: Parse TuplePattern
			//			}
			else
			{
				new Exception.SyntaxError("Identifier, Tuple Pattern or Wildcard", parser.Current.Raw, parser.Current.Region).Throw();
				result = null;
			}
			if (parser.Next().Is<Tokens.PostfixOperator>(t => t.Name == ":"))
				result.AssignedType = Type.ParseType(parser);
			return result;
		}
		#endregion
	}
}

