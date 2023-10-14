using Newtonsoft.Json;

namespace Backend.Utils;

public class Base
{
    public static Formatting DefaultFormatting = Formatting.None; //NOSONAR

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this, DefaultFormatting);
    }
}