using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;
using PGGE;

public class NewPlayer : MonoBehaviour
{
    [HideInInspector]
    public FSM mNFsm = new FSM();
    public Animator mAnimator;
    public PlayerMovement mPlayerMovement;

    [HideInInspector]
    public bool[] mAttackButtons = new bool[3];

    public Transform mGunTransform;
    public LayerMask mPlayerMask;
    public AudioSource mAudioSource;
    public AudioClip mAudioClipGunShot;
    public AudioClip mAudioClipReload;


    // Start is called before the first frame update
    void Start()
    {
        //mNFsm.Add(new NewPlayerState_MOVEMENT(this));
        //mNFsm.Add(new NewPlayerState_ATTACK(this));
        //mNFsm.Add(new PlayerState_RELOAD(this));
        mNFsm.SetCurrentState((int)PlayerStateType.MOVEMENT);

        PlayerConstants.PlayerMask = mPlayerMask;
    }

    void Update()
    {
        mNFsm.Update();
        

        // For Student ----------------------------------------------------//
        // Implement the logic of button clicks for shooting. 
        //-----------------------------------------------------------------//

        if (Input.GetButton("Fire1"))
        {
            mAttackButtons[0] = true;
            mAttackButtons[1] = false;
            mAttackButtons[2] = false;
        }
        else
        {
            mAttackButtons[0] = false;
        }

        if (Input.GetButton("Fire2"))
        {
            mAttackButtons[0] = false;
            mAttackButtons[1] = true;
            mAttackButtons[2] = false;
        }
        else
        {
            mAttackButtons[1] = false;
        }

        if (Input.GetButton("Fire3"))
        {
            mAttackButtons[0] = false;
            mAttackButtons[1] = false;
            mAttackButtons[2] = true;
        }
        else
        {
            mAttackButtons[2] = false;
        }
    }

    

    public void Move()
    {
        mPlayerMovement.HandleInputs();
        mPlayerMovement.Move();
    }

    

    public void Reload()
    {
        StartCoroutine(Coroutine_DelayReloadSound());
    }

    IEnumerator Coroutine_DelayReloadSound(float duration = 1.0f)
    {
        yield return new WaitForSeconds(duration);

        mAudioSource.PlayOneShot(mAudioClipReload);
    }
}
