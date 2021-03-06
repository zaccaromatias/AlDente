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
    
    public partial class Reserva
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reserva()
        {
            this.ReservaMesa = new HashSet<ReservaMesa>();
        }
    
        public int Id { get; set; }
        public string Codigo { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaReserva { get; set; }
        public System.DateTime FechaAsistencia { get; set; }
        public Nullable<System.DateTime> FechaCancelacion { get; set; }
        public string MotivoCancelacion { get; set; }
        public string Notas { get; set; }
        public int CantidadComensales { get; set; }
        public int EstadoReservaId { get; set; }
        public int ClienteId { get; set; }
        public int RestauranteId { get; set; }
        public Nullable<int> EmpleadoId { get; set; }
        public int TurnoId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Empleado Empleado { get; set; }
        public virtual EstadoReserva EstadoReserva { get; set; }
        public virtual Restaurante Restaurante { get; set; }
        public virtual Turno Turno { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReservaMesa> ReservaMesa { get; set; }
    }
}
