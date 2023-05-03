using System;
using UnityEngine;

/*
    basically when you plant something it gets added to a list. 
    All plants in that list get aged every 6 minutes.
    
    if all plants are fully grown a system notification will be sent.

*/

public class GameTime : MonoBehaviour
{
    public delegate void OnTickHandler();
    public static event OnTickHandler OnTck;

    private static float timer = 0.0f;
    private readonly float interval = 5.0f;

    private static bool active = false;

    public static void ClockStart() => active = true;
    public static void ClockStop() => active = false;
    public static void ClockReset() => timer = 0.0f;

    public void Update() {

        if (active == false)
            return;

        timer += Time.deltaTime;

        if (timer >= interval) {
            timer -= interval;
            OnTck?.Invoke();
        }
    }
}
