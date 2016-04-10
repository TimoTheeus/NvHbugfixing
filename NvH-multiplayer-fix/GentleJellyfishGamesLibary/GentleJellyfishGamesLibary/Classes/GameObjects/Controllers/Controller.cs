//Class kinda empty, only used to specify sidebranch for controllers of objects.
public class Controller : Object
{
    public Controller(int layer = 0, string id = "") : base(id, layer)
    {
        isController = true;
    }
}

