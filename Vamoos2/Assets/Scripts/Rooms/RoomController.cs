using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject[] sala;
    public int[] numEnems;
    public bool fighting;
    public GameObject[] doors;

    private void Start()
    {
        fighting = false;
    }

}
