using Entities.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Base : Notifies
    {               
        public int Id { get; set; }
       
        public string Nome { get; set; }
    }
}
