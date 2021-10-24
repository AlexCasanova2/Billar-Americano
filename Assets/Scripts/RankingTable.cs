using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RankingTable : MonoBehaviour
{
    protected GameManager gameManager;
    public Transform rankingContainer;
    public Transform rankingEntry;
    int score;
    public List<RankingScoreEntry> rankingScoreEntryList;

    private void Awake()
    {
        rankingContainer = transform.Find("RankingContainer");
        rankingEntry = transform.Find("RankingEntry");
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        ShowRanking();
    }

    public void CreateRankingScore(RankingScoreEntry rankingScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;

        Transform entryTransform = Instantiate(rankingEntry, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);    

        transformList.Add(entryTransform);
    }

    public void AddRankingEntry(int score, int hits, string name)
    {
        RankingScoreEntry rankingScoreEntry = new RankingScoreEntry { score = score, hits = hits, name = name };

        Debug.Log(rankingScoreEntry);
    }

    public void ShowRanking()
    {
        for (int i = 0; i < rankingScoreEntryList.Count; i++)
        {
            Debug.Log(rankingScoreEntryList);
        }
    }

    public class RankingScoreEntry
    {
        public int score;
        public string name;
        public int hits;
    }
}
