using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class CampaignViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Nazwa jest wymagana!")]
        public string Name { get; set; }

        [DisplayName("Imię Lidera")]
        [Required(ErrorMessage = "Imię Lidera jest wymagane!")]
        public string Leader { get; set; }

        [DisplayName("Opis")]
        [Required(ErrorMessage = "Opis jest wymagany!")]
        public string Description { get; set; }

        [DisplayName("Koszt")]
        [Required(ErrorMessage = "Koszt jest wymagany!")]
        //[DisplayFormat(DataFormatString = "{0:0.##}")]
        [RegularExpression(@"[0-9]+([\.,][0-9]+)?", ErrorMessage ="Niewłaściwy format, właściwy to 00.00")]
        [Range(0, int.MaxValue, ErrorMessage = "Wartość musi być większa niż 0")]
        public decimal Cost { get; set; }
    }
}
