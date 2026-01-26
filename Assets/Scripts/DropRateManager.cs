using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        [Range(0f, 100f)]
        public float dropRate;
    }

    public List<Drops> drops;

     void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        float randomNumber = Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops drop in drops)
        {
            if (randomNumber <= drop.dropRate)
            {
                possibleDrops.Add(drop);
            }
        }

        if (possibleDrops.Count > 0)
        {
            Drops selectedDrop =
                possibleDrops[Random.Range(0, possibleDrops.Count)];

            Instantiate(
                selectedDrop.itemPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }
}
