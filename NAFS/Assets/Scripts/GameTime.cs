using System;
using UnityEngine;

/*
    basically when you plant something it gets added to a list. 
    All plants in that list get aged every 6 minutes.
    
    if all plants are fully grown a system notification will be sent.

*/

public class GameTime : MonoBehaviour

{

private static float timer = 0.0f;
private float interval = 6.0f

private static active = false;

public static void Start() =>
{
    active = true;
}

public static void Stop() =>
{
    active = false;
}

public static void Reset() =>
{
    timer = 0.0f
}

public void Update()
{
    if (active == false)
        return;

    timer += Time.SmoothDeltaTime;
    
    if(timer >= interval)
    {
        timer -= interval;
        
        Tick?.Invoke();
    }
}

}
