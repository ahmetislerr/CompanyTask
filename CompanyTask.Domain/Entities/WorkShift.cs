namespace CompanyTask.Domain.Entities
{
    public class WorkShift : IBaseEntity
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Statu? Statu { get; set; }

        //Navigation Property
        public AppUser User { get; set; }
        public int? StatuId { get; set; }
    }
}
