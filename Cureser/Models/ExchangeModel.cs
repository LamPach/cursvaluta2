using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cureser
{
    public class ExchangeModel
    {
        public ExchangeModel() { }

        [DisplayName("Код валюты")]
        public string Code { get; set; }

        [DisplayName("Курс валюты")]
        public double ExchangeRate { get; set; }
    }
}
