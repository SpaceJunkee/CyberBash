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

    public static bool hasBezerkBeenBought = false;
    public static bool isInBerzerkMode = false;

    public LineRenderer lineRenderer;

    public TimeManager timeManager;

    public BerzerMeter berzerkMeter;

    public bool canFillCombo2 = false;
    public bool canFillCombo3 = false;
    public bool canFillCombo4 = false;
    public bool canFillCombo5 = false;
    public bool canFillCombo6 = false;
    public bool isBerzerkMeterBlocked = false;

    private void Start()
    {
        berzerkMeter.SetMaxFill(100);
        berzerkMeter.SetFill(0);
        isInBerzerkMode = false;
        isBerzerkMeterBlocked = false;
        maxDragDistance = 8f;
        rigidBody.angularDrag = 0.05f;
        rigidBody.drag = 0f;
        trail.startWidth = 5;
        trail.startColor = new Color32(0, 241, 255, 255);
        lineRenderer.startWidth = 2;
        lineRenderer.startColor = new Color32(0, 241, 255, 255);
    }

    private void Update()
    {
        AddToBerzerkMeter();

        if (BerzerMeter.currentValue >= 100 && hasBezerkBeenBought == true)
        {
            EnableBerzerk();          
        }
   
        if (ComboHandler.hitCount < 3)
        {
            NormalObstacle.comboSlowMo = 1f;
            DoublePointObstacle.comboSlowMo = 1f;        
        }

        if (isHeldDown == true)
        {
            canFillCombo2 = false;
            canFillCombo3 = false;
            canFillCombo4 = false;
            canFillCombo5 = false;
            canFillCombo6 = false;

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

    public void EnableBerzerk()
    {
        isInBerzerkMode = true;
        isBerzerkMeterBlocked = true;
        maxDragDistance = 15f;
        rigidBody.angularDrag = 7500f;
        rigidBody.drag = 1f;
        trail.startWidth = 9.5f;
        trail.startColor = new Color32(255,0,33,255);
        lineRenderer.startWidth = 5;
        lineRenderer.startColor = new Color32(255, 0, 33, 255);
            
        GameObject.Find("BerzerkSprite").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("BerzerkSprite").GetComponent<CircleCollider2D>().enabled = true;
        GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("PlayerSprite").GetComponent<CircleCollider2D>().enabled = false;

        StartCoroutine(DisableBerzerk());
    }

    IEnumerator DisableBerzerk()
    {
        berzerkMeter.SetFill(0);
        yield return new WaitForSeconds(10);
        isInBerzerkMode = false;
        isBerzerkMeterBlocked = false;
        maxDragDistance = 8f;
        rigidBody.angularDrag = 0.05f;
        rigidBody.drag = 0f;
        trail.startWidth = 5;
        trail.startColor = new Color32(0, 241, 255, 255);
        lineRenderer.startWidth = 2;
        lineRenderer.startColor = new Color32(0, 241, 255, 255);

        GameObject.Find("BerzerkSprite").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("BerzerkSprite").GetComponent<CircleCollider2D>().enabled = false;
        GameObject.Find("PlayerSprite").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("PlayerSprite").GetComponent<CircleCollider2D>().enabled = true;     
    }

    public void AddToBerzerkMeter()
    {
       
        if (ComboHandler.hitCount == 2 && isBerzerkMeterBlocked == false)
        {
            if (!canFillCombo2)
            {
                berzerkMeter.IncreaseFill(7);
                canFillCombo2 = true;
            }
        }
        else if (ComboHandler.hitCount == 3 && isBerzerkMeterBlocked == false)
        {
            if (!canFillCombo3)
            {
                berzerkMeter.IncreaseFill(10);
                canFillCombo3 = true;
            }
        }
        else if (ComboHandler.hitCount == 4 && isBerzerkMeterBlocked == false)
        {
            if (!canFillCombo4)
            {
                berzerkMeter.IncreaseFill(15);
                canFillCombo4 = true;
            }
        }
        else if (ComboHandler.hitCount == 5 && isBerzerkMeterBlocked == false)
        {
            if (!canFillCombo5)
            {
                berzerkMeter.IncreaseFill(20);
                canFillCombo5 = true;
            }
        }
        else if (ComboHandler.hitCount >= 6 && isBerzerkMeterBlocked == false)
        {
            if (!canFillCombo6)
            {
                berzerkMeter.IncreaseFill(25);
                canFillCombo6 = true;
            }
        }
    }

    
}
