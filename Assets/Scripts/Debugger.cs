using UnityEngine;

public class Debugger : Singleton<Debugger>
{
    public static void CheckInstanceIsNullAndQuit(Object obj)
    {
        if(obj == null)
        {
            Debug.LogError(obj.name + "is null");
            Application.Quit();
        }
    }
}
