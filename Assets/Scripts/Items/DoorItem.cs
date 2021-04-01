using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : MonoBehaviour, InteractableItem
{
    public void InteractAction()
    {
        gameObject.SetActive(false);
    }
}
