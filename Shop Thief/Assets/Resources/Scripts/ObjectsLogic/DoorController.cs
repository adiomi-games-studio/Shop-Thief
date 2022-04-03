using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    #region variables
    private Animator doorAnim;

    [SerializeField]
    private AudioSource doorSound;
    #endregion

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }

    /// <summary>
    /// Open door
    /// </summary>
    public IEnumerator OpenDoor(bool open)
    {
        //open door if them are closed
        if (!doorAnim.GetBool("open"))
        {
            doorAnim.SetBool("open", true);
            doorSound.Play();
        }

        yield return new WaitForSecondsRealtime(3f);

        //close door after 3 seconds if player is far from the door
        if (!ControlPlayer.Instante.playerIsEntryShop)
        {
            doorAnim.SetBool("open", false);
            doorSound.Play();
        }
        else
            StartCoroutine(OpenDoor(true));
    }
}
