using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Transform[] puntosSpawn;
    float multiplicador = 0.2f;

    int rondas = 0;
    int enemigosPorRonda = 10;
    int enemigosVivos = 0;

    public GameObject enemy;

    public static GameManager instance;

    public GameObject player;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    void Spawn()
    {
        int donde = Random.Range(0, puntosSpawn.Length);
        GameObject enemigo = Instantiate(enemy, puntosSpawn[donde].position, transform.rotation);
        enemigo.GetComponent<EnemyController>().player = player;
    }

    public void EnemyDead()
    {
        enemigosVivos--;
        if(enemigosVivos <= 0)
        {
            rondas++;
            //Si no le sé a las matemáticas, pido disculpas (por adelantado)
            enemigosPorRonda = (int)(enemigosPorRonda * multiplicador) + enemigosPorRonda;
            StartCoroutine(StartWave());
        }
    }

    IEnumerator StartWave()
    {
        enemigosVivos = enemigosPorRonda;
        for(int i = 0; i < enemigosVivos; i++)
        {
            Spawn();
        }
        yield return null;
    }
}
