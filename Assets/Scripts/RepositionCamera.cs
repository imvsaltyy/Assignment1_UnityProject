using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepositionCamera : MonoBehaviour
{
    public Transform mPlayer;
    //ThirdPersonCamera TPC;
    private float cameraRotation;
    
    Vector3 targetPos;

    public float offsetDist = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //Camera.main.transform.position = ThirdPersonCamera.transform.position;
        //Camera.main.transform.position = new Vector3(0,0.5f,0);
        //cameraRotation = 0f;
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
            Debug.Log("Hit");
            Vector3 newCamPos = transform.position - mPlayer.transform.TransformDirection(Vector3.back) * offsetDist;
            Camera.main.transform.position = newCamPos;
            //targetPos = (hit.point - transform.position) * 0.8f + transform.position;
            
        }
        //else
            //Debug.Log("Not hitting the wall");
    }
}
