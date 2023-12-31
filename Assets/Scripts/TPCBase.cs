﻿using System.Collections;
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

            //to make the camera at head level by adding the y of the camera position
            Vector3 playerOffset = new Vector3(mPlayerTransform.position.x, mPlayerTransform.position.y + CameraConstants.CameraPositionOffset.y, mPlayerTransform.position.z);
            //to get the vector of the camera position to the player head
            Vector3 camToPlayer = mCameraTransform.position - playerOffset;
            //checking if the ray hits the mask(wall) and storing the hit info
            if (Physics.Raycast(playerOffset, camToPlayer.normalized, out hit, camToPlayer.magnitude, mask))
            {
                //debugging
                Debug.Log("Hit");
                Debug.DrawLine(playerOffset, hit.point, Color.red);
                //transforming the camera to the hit point position (where ray hit the collider)
                mCameraTransform.position = hit.point;

            }
        }
            

        public abstract void Update();
    }
}
