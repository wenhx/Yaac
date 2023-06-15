namespace Yaac.Shared;

public class Constants
{
    public class Models
    {
        public const int MaximumUserNameLength = 20;
        public const int MinimumUserNameLength = 5;
        public const int MaximumPasswordLength = 20;
        public const int MinimumPasswordLength = 8;
    }

    public class Errors
    {
        public static readonly string InvalidModelState = "Invalid Model State";
    }
}
