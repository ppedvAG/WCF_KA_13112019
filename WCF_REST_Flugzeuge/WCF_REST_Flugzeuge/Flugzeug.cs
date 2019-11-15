using System;

namespace WCF_REST_Flugzeuge
{
    public class Flugzeug
    {
        public int Id { get; set; }
        public decimal Gewicht { get; set; }
        public int AnzahlPassagiere { get; set; }
        public string Modell { get; set; }
        public DateTime Baujahr { get; set; }
    }
}
