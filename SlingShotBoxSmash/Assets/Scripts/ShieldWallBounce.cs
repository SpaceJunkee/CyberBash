using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ShieldWallBounce : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {   
         
        if (collision.gameObject.tag.Equals("PlayerBig"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        CameraShake.Instance.ShakeCamera(5f, 1f);

        if (gameObject.name.Equals("ShieldWallBottom"))
        {
            ActivateShieldSmashEffect("ShieldSmashBottom");
        }
        else if (gameObject.name.Equals("ShieldWallLeft"))
        {
            ActivateShieldSmashEffect("ShieldSmashLeft");
        }
        else if (gameObject.name.Equals("ShieldWallRight"))
        {
            ActivateShieldSmashEffect("ShieldSmashRight");
        }
        else if (gameObject.name.Equals("ShieldWallTop"))
        {
            ActivateShieldSmashEffect("ShieldSmashTop");
        }
            
    }

    private void ActivateShieldSmashEffect(string name)
    {
        GameObject.Find(name).GetComponent<ParticleSystem>().Play();
    }

    public void ResetShields()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}
