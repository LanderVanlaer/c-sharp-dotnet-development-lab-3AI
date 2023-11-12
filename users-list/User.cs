namespace users_list;

public record User(string FirstName, string LastName)
{
    public override string ToString()
    {
        return FirstName + ' ' + LastName;
    }
}