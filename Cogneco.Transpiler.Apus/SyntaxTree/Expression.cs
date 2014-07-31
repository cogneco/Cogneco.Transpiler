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

using Kean.Extension;
using Generic = System.Collections.Generic;
using Uri = Kean.Uri;
using IO = Kean.IO;
using Collection = Kean.Collection;

namespace Cogneco.Transpiler.Apus.SyntaxTree
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
		#region Static Parse
		internal static Expression ParseExpression(Tokens.Lexer lexer)
		{
			return Expression.ParseExpression(lexer, 0, new Collection.Stack<Expression>());
		}
		internal static Expression ParseExpression(Tokens.Lexer lexer, int precedence, Collection.IStack<Expression> stack)
		{
			Expression result = null;
			Tokens.Token current = lexer.Current;
			if (current is Tokens.Literal)
			{
				result = Literal.Create(current as Tokens.Literal);
				lexer.Next();
			}
			else if (current is Tokens.Identifier)
				result = Identifier.ParseIdentifier(lexer);
			else if (current is Tokens.BinaryOperator)
			{
				BinaryOperator o = BinaryOperator.Create(current as Tokens.BinaryOperator);
				if (o.NotNull() && precedence < o.Precedence)
				{
					if ((o.Left = stack.Pop()).IsNull())
						new Exception.SyntaxError("left hand expression of binary operator \"" + o.Symbol + "\"", "nothing", o.Region).Throw();
					if (lexer.Next().IsNull() || (o.Right = Expression.ParseExpression(lexer, o.Associativity == Associativity.Right ? o.Precedence + 1 : o.Precedence, stack)).IsNull())
						new Exception.SyntaxError("right expression of binary operator \"" + o.Symbol + "\"", "nothing", o.Region).Throw();
					result = o;
				}
			}
			else if (current is Tokens.LeftParenthesis)
			{
				if (lexer.Next().IsNull() || (result = Expression.ParseExpression(lexer)).IsNull() || !(lexer.Current is Tokens.RightParenthesis))
					new Exception.SyntaxError("matching end parenthesis", lexer).Throw();
				lexer.Next();
			}
			if (result.NotNull())
			{
				result = Expression.ParseExpression(lexer, precedence, stack.Push(result)) ?? stack.Pop();
				if (lexer.Current.Is<Tokens.PostfixOperator>(t => t.Name == ":"))
					result.AssignedType = Type.ParseType(lexer);
			}
			return result;
		}
		#endregion
	}
}

