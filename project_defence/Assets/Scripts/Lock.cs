using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject imageLock;
<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
=======
    public GameObject imageTower;
    public bool isLockOff = true;
    // Start is called before the first frame update
    void Start()
    {
        isLockOff = true;
>>>>>>> origin/Jeonghoon
        //imageLock.SetActive(true);
    }

    public void LockOn()
    {
        imageLock.SetActive(false);
    }

<<<<<<< HEAD
=======
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

>>>>>>> origin/Jeonghoon
    public void LockOff()
    {
        imageLock.SetActive(false);
    }
}
