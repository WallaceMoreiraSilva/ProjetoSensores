﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoresAPP.ViewModels
{
    public class SensorViewModel
    {       
        [Key]       
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Número é obrigatório")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [NotMapped]        
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Data Cadastro é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]      
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Data Alteração é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data Alteração")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }
        
        [Required(ErrorMessage = "Região é obrigatório")]
        public int RegiaoId { get; set; }

        public RegiaoViewModel Regiao { get; set; }

        //sai
        [Display(Name = "Região")]
        public string NomeRegiao { get; set; }

        [Required(ErrorMessage = "País é obrigatório")]
        public int PaisId { get; set; }

        public PaisViewModel Pais { get; set; }

        //sai
        [Display(Name = "País")]
        public string NomePais { get; set; }
                
        public int StatusSensorId { get; set; }

        public StatusSensorViewModel StatusSensor { get; set; }

        //sai
        [Display(Name = "Status")]
        public string StatusDoSensor { get; set; }

    }
}
