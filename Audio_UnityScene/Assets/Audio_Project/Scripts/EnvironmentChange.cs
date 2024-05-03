using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnvironmentChange : MonoBehaviour
{
    public AudioMixerSnapshot indoorSnapshot;
    public AudioMixerSnapshot outdoorSnapshot;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Indoor":
                indoorSnapshot.TransitionTo(2f);
                break;
            case "Outdoor":
                outdoorSnapshot.TransitionTo(2f);
                break;
            default:
                break;
        }
    }
}
