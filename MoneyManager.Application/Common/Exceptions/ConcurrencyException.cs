namespace MoneyManager.Application.Common.Exceptions;

public class ConcurrencyException(string m, Exception? inner = null) : AppException(m , inner);