using System.ComponentModel;

namespace ProjetoDDD.Sensores.CrossCutting.Enum
{
	public enum StatusEventoDisparadoEnum
	{
		[Description("Erro")]
		Erro = 0,

		[Description("Processado")]
		Processado = 1		
	}
}
