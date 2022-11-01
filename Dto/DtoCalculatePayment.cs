namespace ApiCoppel.Dto
{
    public class DtoCalculatePayment
    {
        public int Hours { get; set; }
        public int TotalDelivery { get; set; }
        public int TotalBonous { get; set; }
        public double Retention { get; set; }
        public double GralTotal { get; set; }
        public int EmployeeId { get; set; }
        public DtoEmployee Employee { get; set; }
    }
}
