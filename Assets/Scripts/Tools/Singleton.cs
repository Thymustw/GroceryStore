using UnityEngine;

// Create a singleton for everybody.
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    // For inside.
    private static T instance;
    // For outside.
    public static T Instance
    {
        get{ return instance;}
    }

    protected virtual void Awake() 
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (instance == (T)this)
            instance = null;
    }
}
