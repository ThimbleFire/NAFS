using System;
using UnityEngine;

public class ResourceRepository<GameObject> : MonoBehaviour

{
    public List<GameObject> resource;

    public GameObject this[string i] =>

   

      get { return resource.Find(x=>x.Name == i); }


   
}
