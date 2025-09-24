namespace MoneyManager.Application.Common.Exceptions;

public class ForbiddenException(string m, Exception? inner = null) : AppException(m , inner);