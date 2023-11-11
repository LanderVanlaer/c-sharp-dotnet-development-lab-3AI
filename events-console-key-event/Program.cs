// Write a program that counts keystrokes (Console.readKey). Use an event to alert 3 separate messagboard
// object every time that the number of keystrokes can be devided by 10 (10,20,30,40, …) which will
// display a message (E.g.: Messageboard1: 10 keystrokes)

ConsoleKeyEventHandler consoleKeyEventHandler = new();

new MessageBoard("MessageBoard1", consoleKeyEventHandler);
new MessageBoard("MessageBoard2", consoleKeyEventHandler);
new MessageBoard("MessageBoard3", consoleKeyEventHandler);

consoleKeyEventHandler.ReadKeys();


internal class ConsoleKeyEventHandler
{
    internal delegate void ConsoleKeyEvent(ConsoleKeyInfo consoleKeyInfo);

    public event ConsoleKeyEvent? KeyEvent;

    public void ReadKeys()
    {
        while (true)
        {
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();

            Console.WriteLine();
            if (consoleKeyInfo.Key == ConsoleKey.Escape)
                return;

            KeyEvent?.Invoke(consoleKeyInfo);
        }
    }
}

internal class MessageBoard
{
    private const int NumberOfStrokesDividend = 10;
    private readonly string _name;
    private int _counter = 0;

    public MessageBoard(string name, ConsoleKeyEventHandler consoleKeyEventHandler)
    {
        _name = name;
        consoleKeyEventHandler.KeyEvent += OnKeyEvent;
    }

    private void OnKeyEvent(ConsoleKeyInfo keyInfo)
    {
        ++_counter;
        if (_counter % NumberOfStrokesDividend == 0)
        {
            Console.WriteLine(_name + ": " + _counter);
        }
    }
}