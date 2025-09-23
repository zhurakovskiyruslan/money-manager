namespace MoneyManager.Application.Common.Exceptions;

public class ForbiddenException(string m) : Exception(m);