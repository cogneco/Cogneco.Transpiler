//
//  Type.cs
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
	public abstract class Type : Node
	{
		protected Type()
		{
		}
		#region Static Parse
		internal static Type ParseType(Parser parser)
		{
			var current = parser.Current as Tokens.PostfixOperator;
			if (parser.Next().IsNull())
				new Exception.SyntaxError("type expression following postfix \":\"", "nothing", current.Region);
			Type result = null;
			if (parser.Current is Tokens.Identifier)
			{
				result = new TypeIdentifier((parser.Current as Tokens.Identifier).Name) { Region = parser.Current.Region };
				parser.Next();
			}
			return result;
		}
		#endregion
	}
}

