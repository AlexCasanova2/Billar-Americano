using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour {
    public static HighscoreTable instance { get; private set; }

    public Transform entryContainer;
    public Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake() {

      
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

         DontDestroyOnLoad(gameObject);
        

        entryTemplate.gameObject.SetActive(false);

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // Crear tabla por defecto
            Debug.Log("Initializing table with default values...");
            AddHighscoreEntry(7, 3, "ACV");
            AddHighscoreEntry(5, 2, "MAX");
            AddHighscoreEntry(6, 4, "DAV");
            AddHighscoreEntry(8, 6, "JOS");
            AddHighscoreEntry(4, 9, "ERC");
            AddHighscoreEntry(2, 2, "ALC");
            // Cargar
            jsonString = PlayerPrefs.GetString("highscoreTable");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }
            // Ordenar el Ranking en funcion de los puntos
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++) {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++) {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score) {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList) {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList) {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank) {
        default:
            rankString = rank + "TH"; break;

        case 1: rankString = "1ST"; break;
        case 2: rankString = "2ND"; break;
        case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("Position").GetComponent<TextMeshProUGUI>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("Points").GetComponent<TextMeshProUGUI>().text = score.ToString();

        int hits = highscoreEntry.hits;
        entryTransform.Find("Hits").GetComponent<TextMeshProUGUI>().text = hits.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;
        
        if (rank == 1) {
            entryTransform.Find("Position").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("Points").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().color = Color.green;
            entryTransform.Find("Hits").GetComponent<TextMeshProUGUI>().color = Color.green;
        }

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, int hits, string name) {
        // Creamos la entrada
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, hits = hits, name = name };
        
        // Cargamos el archivo
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null) {
            // Si no hay entradas las inicializamos
            highscores = new Highscores() {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Añadimos la entrada a la lista
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Guardamos
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    private class Highscores {
        public List<HighscoreEntry> highscoreEntryList;
    }

    public void DeleteHighscoreEntryTransform()
    {
        // Cargamos el archivo
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList.ToList())
        {
            //Borramos las entradas
            highscores.highscoreEntryList.Remove(highscoreEntry);
            //entryTransform.gameObject.SetActive(false);

        }
        // Guardamos
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    
    [System.Serializable] 
    private class HighscoreEntry {
        public int score;
        public int hits;
        public string name;
    }

}
