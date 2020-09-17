using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrbShield : MonoBehaviour
{
    public static bool hasGreenShieldBeenBought = false;
    public static bool isGreenShieldActive = true;

    private void Start()
    {
        if(hasGreenShieldBeenBought == true)
        {
            isGreenShieldActive = true;
            this.gameObject.GetComponent<ParticleSystem>().Play();
            GameObject.Find("GreenOrbShield").GetComponent<CircleCollider2D>().enabled = true;
        }
        else if(hasGreenShieldBeenBought == false)
        {
            isGreenShieldActive = false;
            this.gameObject.GetComponent<ParticleSystem>().Stop();
            GameObject.Find("GreenOrbShield").GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Projectile") && hasGreenShieldBeenBought == true)
        {
            CameraShake.Instance.ShakeCamera(13f, 0.2f);
            Vector3 vel = collision.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
            var force = transform.position - collision.transform.position;
            force.Normalize();

            isGreenShieldActive = false;
            this.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }

    public void ResetGreenOrbShield()
    {
        isGreenShieldActive = true;
        this.gameObject.GetComponent<ParticleSystem>().Play();
        GameObject.Find("GreenOrbShield").GetComponent<CircleCollider2D>().enabled = true;
    }
}
