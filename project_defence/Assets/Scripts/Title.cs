using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("TitleController");
    }

    private IEnumerator TitleController()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
