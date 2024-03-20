using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnManager : MonoBehaviour
{
    public GameObject[] sectionPrefabs;
    public Transform player;
    public int initialSections = 6;
    public float sectionLength = 100f;
    public float sectionSpawnDistance = 100f;

    private List<Transform> sectionPool = new List<Transform>();
    private int currentSectionIndex = 0;
    private float lastSectionEndZ = 0f;

    void Start()
    {
        InitializeObjectPool();
        ShuffleSections();
        SpawnInitialSections();
    }

    void InitializeObjectPool()
    {
        // Prefab'lerin bir listesini oluştur
        for (int i = 0; i < sectionPrefabs.Length; i++)
        {
            GameObject newSectionObject = sectionPrefabs[i];
            Transform newSection = newSectionObject.transform;
            newSection.gameObject.SetActive(false); // Yolu başlangıçta etkisiz yap
            sectionPool.Add(newSection);
        }
    }

    void ShuffleSections()
    {
        // Liste içindeki yolları karıştır
        for (int i = 0; i < sectionPool.Count; i++)
        {
            Transform temp = sectionPool[i];
            int randomIndex = Random.Range(i, sectionPool.Count);
            sectionPool[i] = sectionPool[randomIndex];
            sectionPool[randomIndex] = temp;
        }
    }

    void SpawnInitialSections()
    {
        // Başlangıç yollarını oluştur
        float spawnZ = 0f;
        for (int i = 0; i < initialSections; i++)
        {
            Transform section = sectionPool[i];
            UpdateSectionPosition(section, spawnZ);
            spawnZ += sectionLength;
        }
        lastSectionEndZ = spawnZ - sectionLength;
    }

    void Update()
    {
        // Oyuncu son yola yaklaştığında bir sonraki yol oluştur
        if (player.position.z > lastSectionEndZ - sectionSpawnDistance)
        {
            SpawnNextSection();
        }
    }

    void SpawnNextSection()
    {
        // Rastgele bir sonraki yolu seç
        currentSectionIndex = Random.Range(0, sectionPool.Count);
        Transform currentSection = sectionPool[currentSectionIndex];

        // Yeni yolun başlangıç pozisyonunu belirle ve aktif et
        float spawnZ = lastSectionEndZ;
        UpdateSectionPosition(currentSection, spawnZ);

        // Yolların sonunu güncelle
        lastSectionEndZ += sectionLength;
    }

    void UpdateSectionPosition(Transform section, float spawnZ)
    {
        section.position = new Vector3(2.5f, 0f, spawnZ);
        section.gameObject.SetActive(true);
    }
}
