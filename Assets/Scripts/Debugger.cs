using UnityEngine;

public class Debugger : Singleton<Debugger>
{
    public static void CheckInstanceIsNull(Object obj)
    {
        if(obj == null)
        {
            Debug.LogError(obj.name + "is Null");
        }
    }
}
