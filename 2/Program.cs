// Write a Cat, Dog and Cow class, a Car class and a Cube class. Create a list of animal, vehicle, and shape objects. Write the following methods:
//   - Race will except 2 animal and/or vehicle objects and print out the fastest of the 2.
//        E.g.: Race(cat1,car1)  Car is first with 100km/h
//   - CompareVolumes will except 2 animal, vehicle and/or shape objects and prints the volume of the largest thing
//        E.g.: CompareVolumes(car1,cube1)  cube is the largest with 1000m3
//   - MakeSound will accept a variable number of animal and/or vehicle lists and print the sound that all objects will make. E.g.:
//        E.g.: MakeSound(AnimalList,VehicleList)
//          Victor the cow says Moooo
//          Lena the cat says Miauw
//          The Volvo says Vroom
//          The Ford says Vroom

List<IAnimal> animals = new() { new Cat(), new Dog(), new Cow() };
List<IVehicle> vehicles = new() { new Car() };
List<IShape> shapes = new() { new Cube() };

Console.WriteLine("##########      RACE      ##########");
IEnumerable<IRaceAble> raceAbles = animals.Concat<IRaceAble>(vehicles).ToList();
foreach (IRaceAble obj1 in raceAbles)
foreach (IRaceAble obj2 in raceAbles)
    Console.WriteLine($"{obj1.GetType()} <-> {obj2.GetType()} => " + Race(obj1, obj2).GetType());

Console.WriteLine("##########     VOLUME     ##########");
IEnumerable<IVolumeObject> volumeObjects = animals.Concat<IVolumeObject>(vehicles).Concat(shapes).ToList();
foreach (IVolumeObject obj1 in volumeObjects)
foreach (IVolumeObject obj2 in volumeObjects)
    Console.WriteLine($"{obj1.GetType()} <-> {obj2.GetType()} => " + CompareVolumes(obj1, obj2).GetType());

MakeSound(animals, vehicles);
MakeSound(vehicles);

return;

static IRaceAble Race(IRaceAble obj1, IRaceAble obj2)
{
    return obj1.Speed > obj2.Speed ? obj1 : obj2;
}

static IVolumeObject CompareVolumes(IVolumeObject obj1, IVolumeObject obj2)
{
    return obj1.Volume > obj2.Volume ? obj1 : obj2;
}

static void MakeSound(params IEnumerable<ISoundable>[] soundables)
{
    foreach (IEnumerable<ISoundable> enumerable in soundables)
    {
        foreach (ISoundable soundable in enumerable)
        {
            Console.WriteLine($"{soundable.GetType()} {soundable.MakeSound()}");
        }
    }
}

internal interface IRaceAble
{
    int Speed { get; }
}

internal interface IVolumeObject
{
    int Volume { get; }
}

internal interface ISoundable
{
    string MakeSound();
}

internal interface IAnimal : IRaceAble, IVolumeObject, ISoundable
{
}

internal interface IVehicle : IRaceAble, IVolumeObject, ISoundable
{
}

internal interface IShape : IVolumeObject
{
}

internal class Cat : IAnimal
{
    public int Speed => 10;
    public int Volume => 2;

    public string MakeSound()
    {
        // ReSharper disable once StringLiteralTypo
        return "Miauw";
    }
}

internal class Dog : IAnimal
{
    public int Speed => 20;
    public int Volume => 4;

    public string MakeSound()
    {
        return "Waf";
    }
}

internal class Cow : IAnimal
{
    public int Speed => 3;
    public int Volume => 20;

    // ReSharper disable once StringLiteralTypo
    public string MakeSound()
    {
        return "Mooooo";
    }
}

internal class Car : IVehicle
{
    public int Speed => 80;
    public int Volume => 25;

    public string MakeSound()
    {
        return "Vroom";
    }
}

internal class Cube : IShape
{
    public int Volume => 10;
}