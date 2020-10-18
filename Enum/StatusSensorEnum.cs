using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Enum
{
	public enum StatusSensorEnum
	{
		[Description("Ativo")]
		Ativo = 1,

		[Description("Inativo")]
		Inativo = 2
	}
}
