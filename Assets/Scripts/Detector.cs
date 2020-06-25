using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detector : MonoBehaviour
{
    [SerializeField]
    private Text Counter;
    [SerializeField]
    private int counted = 0;
    private bool instantiated = false;


    private void Start()
    {
        this.gameObject.transform.parent = GameObject.Find("Canvas").transform;
        Counter.text = counted.ToString();
        var counting = Instantiate(Counter, new Vector3((transform.position.x * (800 / 20)) + 1175, 80, 0), Quaternion.identity);
        counting.transform.parent = GameObject.Find("Canvas").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        var counting = Instantiate(Counter, new Vector3((transform.position.x * (800 / 20)) + 1175, 80, 0), Quaternion.identity);
        counting.transform.parent = this.gameObject.transform;

        Debug.Log("hi");
        counted++;
        Counter.text = counted.ToString();

    }

    IEnumerator Spawn()
    {
        var counting = Instantiate(Counter, new Vector3((transform.position.x * (800 / 20)) + 1175, 80, 0), Quaternion.identity);
        counting.transform.parent = this.gameObject.transform;
        yield return new WaitForSeconds(1);
        Object.Destroy(counting);
        
    }
}
