using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public AudioSource audio;
    private void Start()
    {
        audio.Play();
    }
}
