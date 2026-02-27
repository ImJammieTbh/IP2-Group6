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
    }
}