namespace ECommerce.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, object key)
        : base($"{entity} com id '{key}' não foi encontrado.") { }
}