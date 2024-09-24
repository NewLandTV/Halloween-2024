using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] notes;
    [SerializeField]
    private Transform noteParent;
    [SerializeField, Range(1, 1000)]
    private int makeNoteCount = 50;

    private List<GameObject> noteList;

    [Serializable]
    public class SpawnTrigger
    {
        public float time;

        [Serializable]
        public struct NoteSpawnData
        {
            public Vector3 position;
            public float waitTime;
        }

        public NoteSpawnData[] noteSpawnDatas;
    }

    [SerializeField]
    private SpawnTrigger[] spawnTriggers;
    private int currentSpawnTriggerIndex;
    private float currentTime;

    private void Awake()
    {
        noteList = new List<GameObject>();

        for (int i = 0; i < makeNoteCount; i++)
        {
            MakeNotes();
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentSpawnTriggerIndex >= spawnTriggers.Length)
        {
            return;
        }

        if (currentTime >= spawnTriggers[currentSpawnTriggerIndex].time)
        {
            StartCoroutine(SpawnNoteWithNoteSpawnData(currentSpawnTriggerIndex++));
        }
    }

    private IEnumerator SpawnNoteWithNoteSpawnData(int index)
    {
        for (int i = 0; i < spawnTriggers[index].noteSpawnDatas.Length; i++)
        {
            Vector3 position1 = spawnTriggers[index].noteSpawnDatas[i].position;
            float waitTime1 = spawnTriggers[index].noteSpawnDatas[i].waitTime;

            yield return SpawnNote(position1, waitTime1);
        }
    }

    private void MakeNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            GameObject instance = Instantiate(notes[i], noteParent);

            instance.SetActive(false);

            noteList.Add(instance);
        }
    }

    private GameObject GetNote(int start = 0)
    {
        for (int i = start; i < noteList.Count; i++)
        {
            if (!noteList[i].activeSelf)
            {
                return noteList[i];
            }
        }

        int next = noteList.Count - 1;

        MakeNotes();

        return GetNote(next);
    }

    private IEnumerator SpawnNote(Vector3 position, float waitTime)
    {
        GameObject note = GetNote();

        note.transform.position = position;

        note.SetActive(true);

        yield return new WaitForSeconds(waitTime);
    }
}
