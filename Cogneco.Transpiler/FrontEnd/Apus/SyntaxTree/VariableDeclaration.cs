//
//  VariableDeclaration.cs
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
	public class VariableDeclaration : Declaration
	{
		public Pattern Pattern { get; set; }
		public Expression Expression { get; set; }
		readonly bool constant;
		public bool Constant { get { return this.constant; } }
		public VariableDeclaration(bool constant)
		{
			this.constant = constant;
		}
		public override string ToString()
		{
			return string.Format("{0} {1} = {2}", this.Constant ? "let" : "var", this.Pattern, this.Expression);
		}
		#region Static Parse
		internal static VariableDeclaration ParseVariableDeclaration(Parser parser, bool constant)
		{
			var result = new VariableDeclaration(constant) { Region = parser.Current.Region };
			parser.Next();
			result.Pattern = Pattern.ParsePattern(parser);
			if (!parser.Current.Is<Tokens.BinaryOperator>(t => t.Name == "="))
				new Exception.SyntaxError("binary operator \"=\"", parser.Current.Raw, parser.Current.Region).Throw();
			parser.Next();
			result.Expression = Expression.ParseExpression(parser);
			return result;
		}
		#endregion
	}
}

