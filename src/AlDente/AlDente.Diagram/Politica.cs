//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlDente.Diagram
{
    using System;
    using System.Collections.Generic;
    
    public partial class Politica
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoSancionId { get; set; }
        public int NumeroMaximo { get; set; }
        public int Periodo { get; set; }
        public int EstadoReservaId { get; set; }
        public int RestauranteId { get; set; }
    
        public virtual EstadoReserva EstadoReserva { get; set; }
        public virtual Restaurante Restaurante { get; set; }
        public virtual TipoSancion TipoSancion { get; set; }
    }
}