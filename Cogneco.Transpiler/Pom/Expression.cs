using System;

namespace Cogneco.Transpiler.Pom
{
	public abstract class Expression
	{
		public Type.Expression Type { get; private set; }

		protected Expression(Type.Expression type)
		{
			this.Type = type;
		}
	}
}

