using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCycleScript : MonoBehaviour
{
    Animator animator;
    GameObject[] players;

    [SerializeField] private AudioSource doorOpenAudioSource = null;
    [SerializeField] private float openDelay = 0;

    [SerializeField] private AudioSource doorCloseAudioSource = null;
    [SerializeField] private float closeDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = 0;
        bool door_state = animator.GetBool("character_nearby");

        foreach (GameObject player in players)
        {
            distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
            if (distance < 3.0)
            {
                animator.SetBool("character_nearby", true);
                break;
            }
            else if (animator.GetBool("character_nearby") == true)
            {
                animator.SetBool("character_nearby", false);
            }
        }

        if(animator.GetBool("character_nearby") != door_state)
        {
            if (animator.GetBool("character_nearby") == true)
            {
                //Sound fx to open
                doorOpenAudioSource.PlayDelayed(openDelay);
            }
            else
            {
                //Sound fx to close
                doorCloseAudioSource.PlayDelayed(closeDelay);
            }
        }
    }
}
