using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float droprate;
    }

    public List<Drops> drops ;

    void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>() ;


        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.droprate)
            {
                possibleDrops.Add(rate);

            }
            if (possibleDrops.Count > 0) {
                Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
                Instantiate(rate.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
