using System.Collections.Immutable;

namespace Backend.Utils;

public interface IHandler<TItem, in TItemIdentifier>
{
    public ImmutableHashSet<TItem> All { get; }
    public TItem this[TItemIdentifier index] { get; }

    public void Remove(TItem item);
    public void Remove(TItemIdentifier identifier);
}