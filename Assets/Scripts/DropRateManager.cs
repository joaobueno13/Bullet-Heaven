using UnityEngine;
using System.Collections.Generic;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate; // Chance de drop (0 a 1)
    }

    public List<Drops> drops;

    void OnDestroy()
    {
        float randomNumber = Random.Range(0f, 100f);
        List <Drops> possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        // Se houver itens possíveis para drop, escolha um aleatoriamente e instancie-o
        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);
        }
    }
}
