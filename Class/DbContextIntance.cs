namespace SQL_Injection_Playground.Class
{
    public class DbContextIntance
    {
        //is not a great way to do this, but it works for now
#pragma warning disable CS8618 // A Non-Nullable field must contain a value other than NULL when the constructor exits. Consider declaring it as Nullable.
        public static DbContext_EF DbContext;
#pragma warning restore CS8618 // A Non-Nullable field must contain a value other than NULL when the constructor exits. Consider declaring it as Nullable.
    }
}
