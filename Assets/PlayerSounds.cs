using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public Animator mAnimator;

    public List <AudioClip> concreteSounds;
    public List<AudioClip> dirtSounds;
    public List<AudioClip> woodSounds;
    public List<AudioClip> metalSounds;
    public List<AudioClip> waterSounds;

    enum MaterialSounds
    {
        Concrete , Dirt, Wood, Metal, Water, Empty
    }

    private AudioSource footstepSource;

    // Start is called before the first frame update
    void Start()
    {
        footstepSource = GetComponent<AudioSource>();

    }

    private MaterialSounds SurfaceSelect()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                surfaceMaterial = surfaceRenderer ? surfaceRenderer.sharedMaterial : null;
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
        return MaterialSounds.Empty;

    }

    void Footsteps()
    {
        AudioClip clip = null;

        MaterialSounds surface = SurfaceSelect();

        switch(surface)
        {
            case MaterialSounds.Concrete:
                clip = concreteSounds[Random.Range(0, concreteSounds.Count)];
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

        if (surface != MaterialSounds.Empty && clip != null)
        {
            footstepSource.clip = clip;
            

            if (mAnimator.GetFloat("PosZ") == 1)
            {
                footstepSource.volume = Random.Range(0.2f, 0.5f);
                footstepSource.pitch = Random.Range(1f, 1.5f);
            }
            else
            {
                footstepSource.volume = Random.Range(0.05f, 0.2f);
                footstepSource.pitch = Random.Range(0.8f, 1.2f);
            }

            footstepSource.Play();

        }

    }
}
