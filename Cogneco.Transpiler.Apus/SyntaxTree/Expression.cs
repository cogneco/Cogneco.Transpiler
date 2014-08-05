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
using Text = Kean.IO.Text;

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
		internal bool Write(int precedence, Text.Indenter indenter)
		{
			return precedence > this.Precedence ? indenter.Write("(") && this.Write(indenter) && indenter.Write(")") : this.Write(indenter);
		}
		internal override bool Write(Text.Indenter indenter)
		{
			return this.WriteHelper(indenter) && (this.AssignedType.IsNull() || indenter.Write(": " + this.AssignedType));
		}
		protected abstract bool WriteHelper(Text.Indenter indenter);
		#region Static Parse
		internal static Expression ParseExpression(Tokens.Lexer lexer)
		{
			return Expression.ParseExpression(lexer, 0);
		}
		static Expression ParseExpression(Tokens.Lexer lexer, int precedence)
		{
			Expression result = null;
			if (lexer.Current is Tokens.Literal)
			{
				result = Literal.Create(lexer.Current as Tokens.Literal);
				lexer.Next();
			}
			else if (lexer.Current is Tokens.Identifier)
				result = Identifier.ParseIdentifier(lexer);
			else if (lexer.Current is Tokens.LeftParenthesis)
			{
				if (lexer.Next().IsNull() || (result = Expression.ParseExpression(lexer)).IsNull() || !(lexer.Current is Tokens.RightParenthesis))
					new Exception.SyntaxError("matching end parenthesis", lexer).Throw();
				lexer.Next();
			}
			else if (lexer.Current is Tokens.PrefixOperator)
			{
				var o = PrefixOperator.Create(lexer.Current as Tokens.PrefixOperator);
				lexer.Next();
				o.Expression = Expression.ParseExpression(lexer, o.Precedence);
				result = o;
			}
			return Expression.ParseExpressionEnd(lexer, precedence, result);
		}
		static Expression ParseExpression(Tokens.Lexer lexer, int precedence, Expression before)
		{
			Expression result = null;
			if (lexer.Current is Tokens.InfixOperator)
			{
				InfixOperator o = InfixOperator.Create(lexer.Current as Tokens.InfixOperator);
				if (o.NotNull() && precedence < o.Precedence)
				{
					if ((o.Left = before).IsNull())
						new Exception.SyntaxError("left hand expression of binary operator \"" + o.Symbol + "\"", "nothing", o.Region).Throw();
					if (lexer.Next().IsNull() || (o.Right = Expression.ParseExpression(lexer, o.Associativity == Associativity.Right ? o.Precedence + 1 : o.Precedence)).IsNull())
						new Exception.SyntaxError("right expression of binary operator \"" + o.Symbol + "\"", "nothing", o.Region).Throw();
					result = o;
				}
			}
			else if (lexer.Current is Tokens.LeftParenthesis) // Function call
				result = new FunctionCall() {
					Function = before,
					Arguments = TupleExpression.ParseTupleExpression(lexer),
					Region = lexer.Current.Region
				};
			return result.IsNull() ? before : Expression.ParseExpressionEnd(lexer, precedence, result);
		}
		static Expression ParseExpressionEnd(Tokens.Lexer lexer, int precedence, Expression before)
		{
			if (before.NotNull())
			{
				before = Expression.ParseExpression(lexer, precedence, before);
				if (lexer.Current.Is<Tokens.PostfixOperator>(t => t.Symbol == ":"))
					before.AssignedType = Type.ParseType(lexer);
			}
			return before;
		}
		#endregion
	}
}

