namespace MoneyManager.Application.Common.Exceptions;

public class NotFoundException(string m, Exception? inner = null) : AppException(m , inner);