using System;

namespace Cogneco.Transpiler.Pom
{
	public class VariableExpression 
		: Expression
	{
		public string Name { get; private set; }
		public VariableExpression(string name, Type.Expression type)
			: base(type)
		{
			this.Name = name;
		}
	}
}

