namespace MoneyManager.Application.Common.Exceptions;

public class ConcurrencyException(string m) : Exception(m);