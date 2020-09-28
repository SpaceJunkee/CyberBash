using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBulletPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bomb") || collision.gameObject.tag.Equals("Lava"))
        {
            Destroy(gameObject);
        }
    }
}
