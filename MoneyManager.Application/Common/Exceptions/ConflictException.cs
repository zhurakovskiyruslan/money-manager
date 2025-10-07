namespace MoneyManager.Application.Common.Exceptions;

public class ConflictException(string m, Exception? inner = null) : AppException(m , inner);