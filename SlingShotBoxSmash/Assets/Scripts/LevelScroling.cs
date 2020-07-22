using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroling : MonoBehaviour
{
 
     private Vector3 reset;
 
     void Start () {
         reset = Vector3.right * 4 * 10.24f;
     }
     
      void Update () {
         Vector3 Trans = Vector3.left * Time.deltaTime;
         transform.Translate (Trans);
 
         if (transform.position.x < -20) {
             transform.Translate (reset);
         }
 
     }
 
}
