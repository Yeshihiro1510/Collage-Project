public class MyInputSystem
{
    public static InputSystem_Actions Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = new InputSystem_Actions();
            return _instance;
        }
    }
    
    private static InputSystem_Actions _instance;
}