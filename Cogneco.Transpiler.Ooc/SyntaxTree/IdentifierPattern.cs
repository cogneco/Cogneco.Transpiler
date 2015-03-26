﻿//
//  IdentifierPattern.cs
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
using System.Security;
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Ooc.SyntaxTree
{
	public class IdentifierPattern : Pattern
	{
		public string Name { get; set; }
		public IdentifierPattern(Tokens.Identifier identifier)
		{
			this.Name = identifier.Name;
			this.Region = identifier.Region;
		}
		internal override bool Write(Text.Indenter indenter)
		{
			return indenter.Write(this.Name) && this.WriteType(indenter);
		}
	}
}

