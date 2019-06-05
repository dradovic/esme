namespace esme.Infrastructure.Data
{
    public class CircleUser
    {
        public int CircleId { get; set; }
        public Circle Circle { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
