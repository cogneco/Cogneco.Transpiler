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

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public abstract class Type : Node
	{
		protected Type()
		{
		}
		#region Static Parse
		internal static Type ParseType(Tokens.Lexer lexer)
		{
			Type result = null;
			if (lexer.Current is Tokens.Identifier)
			{
				result = new TypeIdentifier((lexer.Current as Tokens.Identifier).Name) { Region = lexer.Current.Region };
				lexer.Next();
			}
			else if (lexer.Current is Tokens.RightParenthesis)
			{
				result = new TypeIdentifier((lexer.Current as Tokens.Identifier).Name) { Region = lexer.Current.Region };
				lexer.Next();
			}
			else
				new Exception.SyntaxError("type expression", lexer).Throw();
			return result;
		}
		#endregion
	}
}

