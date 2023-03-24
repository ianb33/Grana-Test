using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int levelID;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetLevelID(int ID)
    {
        levelID = ID;
    }
}
