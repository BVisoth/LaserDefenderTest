using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilSpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            StartCoroutine(SpawnAllEvilInWave(currentWave));
        }
        return null;
    }

    private IEnumerator SpawnAllEvilInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
             waveConfig.GetEnemyPrefab(),
             waveConfig.GetWaypoints()[0].transform.position,
             Quaternion.identity);
            newEnemy.GetComponent<EvilPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

}
