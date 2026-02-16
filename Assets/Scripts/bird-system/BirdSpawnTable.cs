using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace bird_system
{
    [CreateAssetMenu(menuName = "Bird System/Spawn Table")]
    public class BirdSpawnTable : ScriptableObject
    {
        public List<BirdData> birds;

        public BirdData GetRandomBird(BirdData.Biome biome, bool isNight)
        {
            var validBirds = birds.Where
                (b => b.biome == biome && (!b.isNightOnly || isNight)).ToList();
            
            if (validBirds.Count == 0)
                return null;
            
            float totalWeight = validBirds.Sum(b => b.spawnWeight);
            float randomValue = Random.Range(0, totalWeight);

            float current = 0f;

            foreach (var bird in validBirds)
            {
                current += bird.spawnWeight;
                if (randomValue <= current)
                    return bird;
            }

            return validBirds[0];
        }
    }
}