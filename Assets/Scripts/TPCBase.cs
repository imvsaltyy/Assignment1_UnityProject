using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PGGE
{
    // The base class for all third-person camera controllers
    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;
        public float offsetDist = 0f;
        private float maxRaycastDistance = 5f;
        public float CameraHeight;
        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }

        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }

        public void RepositionCamera()
        {
            //to make sure the wall only affect the ray
            LayerMask mask = LayerMask.GetMask("Wall");
            //info of ray
            RaycastHit hit;

            //between camera and player
            //mPlayerTransform.transform.TransformDirection(Vector3.back).normalized
            //to make the camera at head level by adding the y of the camera position
            Vector3 playerOffset = new Vector3(mPlayerTransform.position.x , mPlayerTransform.position.y + CameraConstants.CameraPositionOffset.y, mPlayerTransform.position.z);
            Vector3 camAndPlayer = mCameraTransform.position - playerOffset;
            //checking if the ray hits the mask(wall) and storing the hit info
            if (Physics.Raycast(playerOffset, camAndPlayer.normalized, out hit, camAndPlayer.magnitude, mask))
            {
                //debugging
                Debug.Log("Hit");
                Debug.DrawLine(mCameraTransform.position, hit.point);
                //transforming the camera to the hit point position (where ray hit the collider)
                mCameraTransform.position = hit.point;

            }
            //else
            //Debug.Log("Not hitting the wall");

            // Hints:
            //-------------------------------------------------------------------
            // check collision between camera and the player.
            // find the nearest collision point to the player
            // shift the camera position to the nearest intersected point
            //-------------------------------------------------------------------
        }

        public abstract void Update();
    }
}
