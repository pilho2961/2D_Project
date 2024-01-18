using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRockTrigger : MonoBehaviour
{
    public GameObject rock;
    public GameObject activateShade;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rock.SetActive(true);
            activateShade.SetActive(true);
        }
    }


}
