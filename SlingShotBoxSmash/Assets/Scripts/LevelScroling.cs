using System.Collections;
using System.Collections.Generic;
using UnityEditor.Android;
using UnityEngine;

public class LevelScroling : MonoBehaviour
{

    public GameObject[] levels;
    private Camera mainCam;
    private Vector2 screenBounds;
    public float choke;

    private void Start()
    {
        mainCam = gameObject.GetComponent<Camera>();
        screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));

        foreach(GameObject obj in levels)
        {
            LoadChildObjects(obj);
        }

    }

    private void LoadChildObjects(GameObject obj)
    {
        float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.x * 2 / objWidth);
        GameObject clone = Instantiate(obj) as GameObject;

        for(int i = 0;  i <= childsNeeded; i++)
        {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objWidth * i, obj.transform.position.y, obj.transform.position.z);
            c.name = obj.name + i;
        }

        Destroy(clone);
        
    }

    private void LateUpdate()
    {
        foreach(GameObject obj in levels)
        {
            repositionChildObjects(obj);
        }
    }

    private void repositionChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();

        if(children.Length > 1)
        {
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;
            float halfObjWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;

          
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjWidth)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
                
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }

}
