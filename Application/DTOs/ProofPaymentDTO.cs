namespace Application.DTOs
{
    public class ProofPaymentDTO
    {
        public int Id { get; set; }
        public int IdContrato { get; set; }
        public int IdUser { get; set; }
        public string? NameEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public int NitEmpresa { get; set; }
        public int IdPeriodo { get; set; }
        public int IdNomina { get; set; }
        public int Month { get; set; }
        public string? MonthDescription { get; set; }
        public int Year { get; set; }
        public string? DateStart { get; set; }
        public string? DateEnd { get; set; }

    }
}
