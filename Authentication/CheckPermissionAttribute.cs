using MwTesting.Model;

namespace MwTesting.Authentication
{
    // make this attribute target methods only i can change  ofcourse and choose if i want to put only one attribute with every method or not in our case we use one 
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(Perm permisson)
        {
            Permisson = permisson;
        }

        public Perm Permisson { get; }
    }
}