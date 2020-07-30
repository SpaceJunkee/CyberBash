using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    private bool cooldown = false;

    public LineRenderer lineRenderer;

    public TimeManager timeManager;

    private void Update()
    {

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
        if ( cooldown == false)
        {
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

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;

    }

}
