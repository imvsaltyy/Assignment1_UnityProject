using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public Animator mAnimator;

    //a list to put all the sounds
    public List<AudioClip> concreteSounds;
    public List<AudioClip> dirtSounds;
    public List<AudioClip> woodSounds;
    public List<AudioClip> metalSounds;
    public List<AudioClip> waterSounds;

    //a representation of the different materials
    enum MaterialSounds
    {
        Concrete, Dirt, Wood, Metal, Water, Empty
    }

    private AudioSource footstepSource;

    // Start is called before the first frame update
    void Start()
    {
        //get the audio
        footstepSource = GetComponent<AudioSource>();
    }

    private MaterialSounds SurfaceSelect()
    {
        //information on the hit
        RaycastHit hit;
        //creates a ray downwards from the player
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        //using the ray, store the hit info, the max distance travelled by the ray, using all layers,
        //ignores the trigger colliders and only allow non-triggers to be collided with the ray
        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            //finds the renderer in the collider that got hit by the ray
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            //checks if there is a renderer
            if (surfaceRenderer)
            {
                //it gets the material from the renderer and makes sure its not null
                surfaceMaterial = surfaceRenderer ? surfaceRenderer.sharedMaterial : null;
                //using the material name to return a material sound corresponding to the name
                if (surfaceMaterial.name.Contains("Concrete"))
                {
                    return MaterialSounds.Concrete;
                }
                else if (surfaceMaterial.name.Contains("Dirt"))
                {
                    return MaterialSounds.Dirt;
                }
                else if (surfaceMaterial.name.Contains("Wood"))
                {
                    return MaterialSounds.Wood;
                }
                else if (surfaceMaterial.name.Contains("Metal"))
                {
                    return MaterialSounds.Metal;
                }
                else if (surfaceMaterial.name.Contains("Water"))
                {
                    return MaterialSounds.Water;
                }
                else
                {
                    return MaterialSounds.Empty;
                }

            }
        }
        //no renderer = returns no sound
        return MaterialSounds.Empty;

    }

    void Footsteps()
    {
        //variable clip to store the sounds
        AudioClip clip = null;

        //find out which material the player is on
        MaterialSounds surface = SurfaceSelect();

        //it checks which surface its on
        switch(surface)
        {
            //if its on concrete, the audio played will at random from the ranges of the concrete sounds provided
            case MaterialSounds.Concrete:
                clip = concreteSounds[Random.Range(0, concreteSounds.Count)];
                //exits the switch statement
                break;

            case MaterialSounds.Dirt:
                clip = dirtSounds[Random.Range(0, dirtSounds.Count)];
                break;

            case MaterialSounds.Wood:
                clip = woodSounds[Random.Range(0, woodSounds.Count)];
                break;

            case MaterialSounds.Metal:
                clip = metalSounds[Random.Range(0, metalSounds.Count)];
                break;

            case MaterialSounds.Water:
                clip = waterSounds[Random.Range(0, waterSounds.Count)];
                break;

            default:
                break;

        }

        Debug.Log(surface);

        //if there is a material below the player and there is a audio for it
        if (surface != MaterialSounds.Empty && clip != null)
        {
            //set the clip into the footstep source to be play
            footstepSource.clip = clip;

            //checks if the player is running (posZ is 1 when running)
            if (mAnimator.GetFloat("PosZ") == 1)
            {
                //set the volume and pitch of the clip to a random range provided
                footstepSource.volume = Random.Range(0.2f, 0.5f);
                footstepSource.pitch = Random.Range(1f, 1.5f);
            }
            else
            {
                footstepSource.volume = Random.Range(0.05f, 0.2f);
                footstepSource.pitch = Random.Range(0.8f, 1.2f);
            }

            //play the clip
            footstepSource.Play();

        }

    }
}
