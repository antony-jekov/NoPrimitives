namespace NoPrimitives;

public interface IValueObject<TPrimitive>
{
    TPrimitive Value { get; }
}