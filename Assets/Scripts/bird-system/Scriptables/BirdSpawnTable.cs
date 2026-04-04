using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bird_system
{
    [CreateAssetMenu(menuName = "Bird System/Spawn Table")]
    public class BirdSpawnTable : ScriptableObject
    {
        public List<BirdData> birds;

        public BirdData GetRandomBird(BirdData.Biome biome, bool isNight, BirdData lastBird)
        {
            var validBirds = birds
                .Where(b => b.biome == biome && (!b.isNightOnly || isNight))
                .ToList();
            
            if (validBirds.Count == 0)
                return null;

            //remove the last spawned bird
            //only if we have more than 1 option 
            if (lastBird != null && validBirds.Count > 1)
            {
                validBirds.Remove(lastBird);
            }
            
            float totalWeight = validBirds.Sum(b => b.spawnWeight);
            float randomValue = Random.Range(0, totalWeight);

            float current = 0f;

            foreach (var bird in validBirds)
            {
                current += bird.spawnWeight;
                if (randomValue <= current)
                {
                    lastBird = bird; //storing new last bird
                    return bird;
                }
            }

            //Fallback safety
            //should rarely even hit it
            lastBird = validBirds[0];
            return validBirds[0];
        }

        public List<BirdData> GetRandomBirdsNoWeight(int numWanted)
        {
            if (birds == null || birds.Count == 0 || numWanted <= 0)
                return new List<BirdData>();

            // If asking for more than available, just return all shuffled
            if (numWanted >= birds.Count)
                return birds.OrderBy(x => Random.value).ToList();

            // Create a copy so we don’t modify the original list
            List<BirdData> pool = new List<BirdData>(birds);
            List<BirdData> result = new List<BirdData>();

            for (int i = 0; i < numWanted; i++)
            {
                int index = Random.Range(0, pool.Count);
                result.Add(pool[index]);
                pool.RemoveAt(index); // ensures no duplicates
            }

            return result;
        }
    }
}