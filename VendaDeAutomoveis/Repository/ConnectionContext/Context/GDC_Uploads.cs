//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendaDeAutomoveis.Repository.ConnectionContext.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class GDC_Uploads
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GDC_Uploads()
        {
            this.GDC_Bancos = new HashSet<GDC_Bancos>();
            this.GDC_Rodas = new HashSet<GDC_Rodas>();
            this.GDC_Veiculos = new HashSet<GDC_Veiculos>();
        }
    
        public string Id { get; set; }
        public System.DateTime Data_Inclusao { get; set; }
        public string Nome_Arquivo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GDC_Bancos> GDC_Bancos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GDC_Rodas> GDC_Rodas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GDC_Veiculos> GDC_Veiculos { get; set; }
    }
}