using System.ComponentModel;

namespace ProjetoDDD.Sensores.Infra.CrossCutting.Enum
{
	public enum StatusSensorEnum
	{
		[Description("Inativo")]
		Inativo = 0,

		[Description("Ativo")]
		Ativo = 1		
	}
}
