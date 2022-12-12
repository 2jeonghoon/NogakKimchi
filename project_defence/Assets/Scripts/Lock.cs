using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject imageLock;
    public GameObject imageTower;
    public bool isLockOff = true;
    // Start is called before the first frame update
    void Start()
    {
        isLockOff = true;
        //imageLock.SetActive(true);
    }

    public void LockOn()
    {
        imageLock.SetActive(false);
    }

    public void LockOffImage()
    {
        if (isLockOff)
        {
            Debug.Log("On");
            imageTower.SetActive(true);
            isLockOff = false;
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
