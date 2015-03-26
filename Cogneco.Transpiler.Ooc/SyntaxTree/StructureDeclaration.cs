//
//  StructureDeclaration.cs
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
using Collection = Kean.Collection;
using Kean.Collection.Extension;
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Ooc.SyntaxTree
{
	public class StructureDeclaration : Declaration
	{
		public IdentifierPattern Name { get; private set; }
		public Collection.IReadOnlyVector<Statement> Statements { get; private set; }
		public StructureDeclaration()
		{
		}
		internal override bool Write(Text.Indenter indenter)
		{
			return indenter.Write("struct ") &&
			this.Name.Write(indenter) &&
			indenter.WriteLine("{") &&
			indenter.AddIndent() &&
			this.Statements.All(statement => statement.Write(indenter) && indenter.WriteLine()) &&
			indenter.RemoveIndent() &&
			indenter.WriteLine("}");
		}
		#region Static Parse
		public static StructureDeclaration ParseStructureDeclaration(Tokens.Lexer lexer)
		{
			var result = new StructureDeclaration() { Region = lexer.Current.Region };
			if (!(lexer.Next() is Tokens.Identifier))
				new Exception.SyntaxError("structure identifier", lexer).Throw();
			result.Name = new IdentifierPattern(lexer.Current as Tokens.Identifier);
			lexer.Next();
			result.Statements = new Collection.ReadOnlyVector<Statement>(Statement.ParseCodeBlock(lexer).ToArray());
			return result;
		}
		#endregion
	}
}

