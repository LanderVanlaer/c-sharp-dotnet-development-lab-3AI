// Write a generic class that will create a list of any type objects containing an int “Id” parameter.
// All objects in the list must have a unique id within the list.
//
// The class has a method (List ReturnBetweenIds) that will accept 2 int parameters (LowerID, UpperID)
// which returns a list containing all objects with an Id between these parameters.
//
// The class contains a method (void ReplaceItemAtId) which will accept an object and an id as a parameter.
// The object with the id given as a parameter will be replaced by the object given as a parameter.
//
// The class contains a method (object PopId) which will accept an id as a parameter. The object with this
// id will be removed from the list and returned from the method.

using System.Collections;
using System.Text;
using generics;

GenericList<Item> list = new()
{
    new Item(2),
    new Item(1),
    new Item(4),
    new Item(3),
};

Console.WriteLine(list);

Console.WriteLine("list.PopId(1)");
list.PopId(1);
Console.WriteLine(list);

Console.WriteLine("list.PopId(-1)");
list.PopId(-1);
Console.WriteLine(list);

Console.WriteLine("list.ReturnBetweenIds(3, 10)");
foreach (Item item in list.ReturnBetweenIds(3, 10)) Console.Write(item.Id.ToString() + ' ');
Console.WriteLine();

Console.WriteLine("list.ReplaceItemAtId(new Item(5))");
list.ReplaceItemAtId(new Item(5));
Console.WriteLine(list);

Console.WriteLine("list.ReplaceItemAtId(new Item(3))");
list.ReplaceItemAtId(new Item(3));
Console.WriteLine(list);

namespace generics
{
    internal class Item(int id) : IIdentifier
    {
        public int Id { get; } = id;

        public override string ToString() => id.ToString();
    }

    internal class GenericList<T> : IEnumerable<T> where T : IIdentifier
    {
        private readonly List<T> _list = new();

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerable<T> ReturnBetweenIds(int lowerId, int upperId) =>
            from item in _list where lowerId <= item.Id && item.Id < upperId select item;

        public void ReplaceItemAtId(T item)
        {
            int i = _list.FindIndex(t => t.Id == item.Id);

            if (i == -1)
                _list.Add(item);
            else
                _list[i] = item;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            foreach (T o in this)
                stringBuilder.Append(o);

            return string.Join(' ', _list);
            return stringBuilder.ToString();
        }

        public void Add(T item)
        {
            ReplaceItemAtId(item);
        }

        public T? PopId(int id)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                T item = _list[i];
                if (item.Id != id) continue;

                _list.Remove(item);
                return item;
            }

            return default;
        }
    }

    internal interface IIdentifier
    {
        public int Id { get; }
    }
}