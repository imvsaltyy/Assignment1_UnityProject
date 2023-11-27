using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepositionCamera : MonoBehaviour
{
    public Transform mPlayer;

    private float cameraRotation;
    
    Vector3 targetPos;

    public float offsetDistance = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //hit = GetComponent<RaycastHit>();
        Camera.main.transform.position = new Vector3(0,0.5f,0);
        cameraRotation = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Transform transform = Camera.main.transform;

        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit[] hits = Physics.RaycastAll(transform.position, mPlayer.transform.TransformDirection(Vector3.back), Mathf.Infinity);
        if (Physics.Raycast(transform.position, mPlayer.transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity, mask))
        //if (hits.Any(x => x.collider.tag == "Wall"))
        {
            Debug.DrawRay(transform.position, mPlayer.transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            targetPos = (hit.point - transform.position) * 0.8f + transform.position;
            
            Debug.Log("Hit");
        }
        //else
            //Debug.Log("Not hitting the wall");
    }
}
