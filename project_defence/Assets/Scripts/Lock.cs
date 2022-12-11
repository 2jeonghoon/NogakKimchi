using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject imageLock;
    public GameObject imageTower;
    // Start is called before the first frame update
    void Start()
    {
        //imageLock.SetActive(true);
    }

    public void LockOn()
    {
        imageLock.SetActive(false);
    }

    public void LockOffImage()
    {
        if (imageLock.active)
        {
            Debug.Log("On");
            imageTower.SetActive(true);
        }
    }
    public void ImageTowerOff()
    {
        imageTower.SetActive(false);
    }

    public void LockOff()
    {
        imageLock.SetActive(false);
    }
}
