using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SlingShot : MonoBehaviour
{
    private bool isHeldDown = false;
    public Rigidbody2D rigidBody;
    public Rigidbody2D anchorRb;
    public float releaseTime = 0.15f;
    public float maxDragDistance = 2f;

    public TimeManager timeManager;

    private void Update()
    {
        if(isHeldDown == true)
        {

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
        isHeldDown = true;
        rigidBody.isKinematic = true;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

        timeManager.StartSlowMotion();

        anchorRb.position = rigidBody.position;
        GetComponent<SpringJoint2D>().enabled = true;
    }

    private void OnMouseUp()
    {
        isHeldDown = false;
        rigidBody.isKinematic = false;
        rigidBody.constraints = RigidbodyConstraints2D.None;

        timeManager.StopSlowMotion();

        StartCoroutine(Release());
    }

    IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseTime);

        GetComponent<SpringJoint2D>().enabled = false;

    }

    private bool HasObjectBeenHit()
    {
        //if object is hit return true and set SpringJoint2d to enabled.
        return false;
    }
}
