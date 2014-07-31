//
//  Statement.cs
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
using Uri = Kean.Uri;
using IO = Kean.IO;
using Collection = Kean.Collection;
using Generic = System.Collections.Generic;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public abstract class Statement : Node
	{
		protected Statement()
		{
		}
		#region Static Parse
		internal static Generic.IEnumerable<Statement> ParseStatements(Tokens.Lexer lexer)
		{
			Statement result;
			while (lexer.NotNull() && !lexer.Empty && (result = Statement.ParseStatement(lexer)).NotNull())
				yield return result;
		}
		internal static Statement ParseStatement(Tokens.Lexer lexer)
		{
			Statement result;
			if (lexer.Current is Tokens.Keyword)
			{
				switch ((lexer.Current as Tokens.Keyword).Name)
				{
					case Tokens.Keywords.Let:
						result = VariableDeclaration.ParseVariableDeclaration(lexer, true);
						break;
					case Tokens.Keywords.Var:
						result = VariableDeclaration.ParseVariableDeclaration(lexer, false);
						break;
					case Tokens.Keywords.Func:
						result = FunctionDeclaration.ParseFunctionDeclaration(lexer);
						break;
					default:
						result = Expression.ParseExpression(lexer);
						break;
				}
			}
			else if (lexer.Current is Tokens.RightBrace)
				result = null;
			else if (lexer.Current is Tokens.LeftBrace)
				result = null;
			else
				result = Expression.ParseExpression(lexer);
			return result;
		}
		internal static Generic.IEnumerable<Statement> ParseCodeBlock(Tokens.Lexer lexer)
		{
			if (!(lexer.Current is Tokens.LeftBrace))
				new Exception.SyntaxError("function body starting with \"{\"", lexer).Throw();
			lexer.Next();
			foreach (var result in Statement.ParseStatements(lexer))
				yield return result;
			if (!(lexer.Current is Tokens.RightBrace))
				new Exception.SyntaxError("function body ending with \"}\"", lexer).Throw();
			lexer.Next();
		}
		#endregion
	}
}

