using System;

namespace Cogneco.Transpiler.Pom
{
	public class FunctionDefinition 
		: Definition
	{
		public Expression Body { get; private set; }

		public FunctionDefinition(Definition parent, string name, Type.Expression type, Expression body)
			: base(parent, name, type)
		{
			this.Body = body;
		}
	}
}

