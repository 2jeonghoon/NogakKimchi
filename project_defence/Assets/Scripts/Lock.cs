using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject imageLock;
    // Start is called before the first frame update
    void Start()
    {
        imageLock.SetActive(true);
    }

    public void LockOff()
    {
        imageLock.SetActive(false);
    }
}
