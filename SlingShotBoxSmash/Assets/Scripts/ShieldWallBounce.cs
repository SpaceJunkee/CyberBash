
using UnityEngine;

public class ShieldWallBounce : MonoBehaviour
{

    public AudioSource shieldBreakAudioSource;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Player") && SlingShot.isHeldDown)
        {

        }
        else 
        {
            if (collision.gameObject.tag.Equals("Player")){
                
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;

                if (gameObject.name.Equals("ShieldWallBottom"))
                {
                    Invoke("DeactivateBounceBottom", 0.1f);                   
                    ActivateShieldSmashEffect("ShieldSmashBottom");
                }
                else if (gameObject.name.Equals("ShieldWallLeft"))
                {
                    Invoke("DeactivateBounceLeft", 0.1f);
                    ActivateShieldSmashEffect("ShieldSmashLeft");
                }
                else if (gameObject.name.Equals("ShieldWallRight"))
                {
                    Invoke("DeactivateBounceRight", 0.1f);
                    ActivateShieldSmashEffect("ShieldSmashRight");
                }
                else if (gameObject.name.Equals("ShieldWallTop"))
                {
                    Invoke("DeactivateBounceTop", 0.1f);
                    ActivateShieldSmashEffect("ShieldSmashTop");
                }

                CameraShake.Instance.ShakeCamera(5f, 1f);
                shieldBreakAudioSource.Play();
            }
            
        }
    }

    private void DeactivateBounceBottom()
    {
        GameObject.Find("ShieldBounceBottom").GetComponent<BoxCollider2D>().enabled = false;
    }
    private void DeactivateBounceLeft()
    {
        GameObject.Find("ShieldBounceLeft").GetComponent<BoxCollider2D>().enabled = false;
    }

    private void DeactivateBounceRight()
    {
        GameObject.Find("ShieldBounceRight").GetComponent<BoxCollider2D>().enabled = false;
    }

    private void DeactivateBounceTop()
    {
        GameObject.Find("ShieldBounceTop").GetComponent<BoxCollider2D>().enabled = false;
    }


    private void ActivateShieldSmashEffect(string name)
    {
        GameObject.Find(name).GetComponent<ParticleSystem>().Play();
    }

    public void ResetShields()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("ShieldBounceBottom").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("ShieldBounceLeft").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("ShieldBounceRight").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("ShieldBounceTop").GetComponent<BoxCollider2D>().enabled = true;
    }
}
