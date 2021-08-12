using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject[] Blocks;
    public GameManager GameManag;
    private int index;
    // Update is called once per frame

    private void Start()
    {
        createBlock();
    }

    public void createBlock()
    {
        index = Random.Range(0, Blocks.Length);
        Instantiate(Blocks[index], transform.position, Quaternion.identity);
        GameManag.GetComponent<GameManager>().AddPoint(index);
    }
}
