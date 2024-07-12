namespace MwTesting.Model
{
    public class UserPerm
    {
        public User User{ get; set; }
        public int UserId { get; set;}
        public Perm permission { get; set;}
    }
}