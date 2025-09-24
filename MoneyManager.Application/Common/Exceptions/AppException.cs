public abstract class AppException : Exception
{
    protected AppException(string m, Exception? inner = null) : base(m, inner) { }
}