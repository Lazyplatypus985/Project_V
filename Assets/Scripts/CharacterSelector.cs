using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public PlayerScriptableObject playerdata;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Debug.LogWarning("EXTRA " + this + " DELETED");
            Destroy(gameObject);
        }
    }

    public static PlayerScriptableObject GetData()
    {
        return instance.playerdata;
    }

    public void SelectCharater(PlayerScriptableObject player)
    {
        playerdata = player;
    }
    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
