using System;

namespace Cogneco.Transpiler.Pom
{
	public class Definition 
		: Expression
	{
		public Definition Parent { get; private set; }

		public string Name { get; private set; }

		public Definition(Definition parent, string name, Type.Expression type)
			: base(type)
		{
			this.Parent = parent;
			this.Name = name;
		}
	}
}

