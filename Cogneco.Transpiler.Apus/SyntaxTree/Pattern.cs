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
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public abstract class Pattern : Node
	{
		public Type AssignedType { get; set; }
		public Type InferredType { get; set; }
		protected Pattern()
		{
		}
		protected bool WriteType(Text.Indenter indenter)
		{
			return this.AssignedType.IsNull() || indenter.Write(": ") && this.AssignedType.Write(indenter);
		}
		#region Static Parse
		internal static Pattern ParsePattern(Tokens.Lexer lexer)
		{
			Pattern result;
			if (lexer.Current is Tokens.Identifier)
				result = (lexer.Current as Tokens.Identifier).Name == "_" ? (Pattern)new WildcardPattern() { Region = lexer.Current.Region } : new IdentifierPattern(lexer.Current as Tokens.Identifier);
			else if (lexer.Current is Tokens.LeftParenthesis)
				result = TuplePattern.ParseTuplePattern(lexer);
			else
			{
				new Exception.SyntaxError("Identifier, Tuple Pattern or Wildcard", lexer.Current.Raw, lexer.Current.Region).Throw();
				result = null;
			}
			if (lexer.Next().Is<Tokens.PostfixOperator>(t => t.Name == ":"))
				result.AssignedType = Type.ParseType(lexer);
			return result;
		}
		#endregion
	}
}

