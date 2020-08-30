using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlingShot : MonoBehaviour
{
    public static bool isHeldDown = false;
    public Rigidbody2D rigidBody;
    public Rigidbody2D anchorRb;
    public TrailRenderer trail;
    public float releaseTime = 0.15f;
    public float maxDragDistance = 2f;
    public Text tutorialText;
    public static bool cooldown = false;
    public AudioSource audio;
    public AudioClip playerDeathSound;
    public AudioClip playerFlickSound;

    public static bool isDead = false;

    public LineRenderer lineRenderer;

    public TimeManager timeManager;

    private void Update()
    {
   
        if (ComboHandler.hitCount < 3)
        {
            NormalObstacle.comboSlowMo = 1f;
            DoublePointObstacle.comboSlowMo = 1f;
            
        }

        if (isHeldDown == true)
        {
            SetLinePos();
            Vector2 mousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            if (Vector3.Distance(mousPosition, anchorRb.position) > maxDragDistance)
            {
                rigidBody.position = anchorRb.position + (mousPosition - anchorRb.position).normalized * maxDragDistance;
            }
            else
            {
                rigidBody.position = mousPosition;
            }
                
        }
    }


    private void OnMouseDown()
    {    
        if (isDead)
        {
            return;
        }

        ComboHandler.ResetValues();

        if ( cooldown == false && isDead == false)
        {
            audio.Stop();
            isHeldDown = true;
            tutorialText.text = "Now let go!";
            rigidBody.isKinematic = true;
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
            rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;

            timeManager.StartSlowMotion(0.5f);
            trail.emitting = false;
            lineRenderer.enabled = true;

            anchorRb.position = rigidBody.position;
            GetComponent<SpringJoint2D>().enabled = true;

            Invoke("ResetCooldown", 0.5f);
            cooldown = true;
        }
               
    }

    void ResetCooldown()
    {
        cooldown = false;
    }

    private void SetLinePos()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = rigidBody.position;
        positions[1] = anchorRb.position;

        lineRenderer.SetPositions(positions); 
    }

    private void OnMouseUp()
    {
        if(isDead == false)
        {

            if (cooldown)
            {
                audio.Play();
            }

            // CameraShake.Instance.ShakeCamera(12f, 0.5f);
            Destroy(tutorialText);
            isHeldDown = false;
            rigidBody.isKinematic = false;
            rigidBody.constraints = RigidbodyConstraints2D.None;

            trail.emitting = true;

            lineRenderer.enabled = false;

            timeManager.StopSlowMotion();

            StartCoroutine(Release());
        }
        else
        {
            return;
        }
        
    }

    IEnumerator Release()
    {
        
        yield return new WaitForSeconds(releaseTime);
        GetComponent<SpringJoint2D>().enabled = false;
        
    }

    
}
