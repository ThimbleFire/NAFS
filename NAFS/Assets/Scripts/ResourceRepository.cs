using System;
using UnityEngine;

public class ResourceRepository<GameObject> : MonoBehaviour

{
    public GameObject[] resource;

    public GameObject this[int i]

   {

      get { return resource[i]; }


   }
}
