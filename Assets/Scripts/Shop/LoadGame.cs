
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Diagnostics;
using System.Linq;

public class LoadGame : MonoBehaviour
{
    public string fileName = "gamesConfiguration.xml";
    [SerializeField] private GameObject gamePrefab;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private InputField pathToAdd;

    private void Start()
    {
        LoadGames();
    }


    private void LoadGames()
    {
        string path = Path.Combine(UnityEngine.Application.dataPath + "/StreamingAssets/" + fileName);
        if (File.Exists(path))
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(path);
                XmlNodeList gameNodes = xmlDoc.SelectNodes("/Games/Game");
                foreach (XmlNode gameNode in gameNodes)
                {
                    Game game = new Game
                    {
                        Name = gameNode.SelectSingleNode("Name")?.InnerText,
                        PlayTime = gameNode.SelectSingleNode("PlayTime")?.InnerText,
                        Icon = Resources.Load<Sprite>(gameNode.SelectSingleNode("PreviewImage")?.InnerText),
                        Path = gameNode.SelectSingleNode("Path")?.InnerText
                    };

                    CreateGame(game);
                }
                buttonPrefab.GetComponent<Button>().onClick.AddListener(OnAddButtonClick);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log("Error loading XML file: " + e.Message);
            }
        }
    }

    void ButtonClickHandler(string path)
    {
        string configPath = Path.Combine(Application.dataPath + "/StreamingAssets/" + fileName);
        if (!string.IsNullOrEmpty(path))
        {
            Process currentProcess = Process.Start(path);
            currentProcess.EnableRaisingEvents = true;
            currentProcess.Exited += (sender, e) =>
            {
                TimeSpan playTime = DateTime.Now - currentProcess.StartTime;

                XDocument xmlDoc = XDocument.Load(configPath);

                XElement gameElement = xmlDoc.Descendants("Game")
                    .FirstOrDefault(game => game.Element("Path")?.Value == path);

                if (gameElement != null)
                {
                    XElement playTimeElement = gameElement.Element("PlayTime");
                    if (playTimeElement != null)
                    {
                        playTimeElement.Value = (TimeSpan.Parse(playTimeElement.Value) + playTime).ToString(@"hh\:mm\:ss");
                    }
                }

                xmlDoc.Save(configPath);
            };
        }
    }

    public void OnAddButtonClick()
    {
        string selectedPath = pathToAdd.text;

        if (!string.IsNullOrEmpty(selectedPath))
        {
            AddGame(selectedPath);
        }
    }

    private void AddGame(string selectedPath)
    {
        string path = Path.Combine(Application.dataPath + "/StreamingAssets/" + fileName);
        if (File.Exists(path))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            // Получить имя файла из выбранного пути
            string filename = Path.GetFileName(selectedPath);

            // Создать новый узел игры
            XmlNode gameNode = xmlDoc.CreateElement("Game");

            // Создать узлы для названия, времени и пути игры
            XmlNode nameNode = xmlDoc.CreateElement("Name");
            nameNode.InnerText = filename;
            gameNode.AppendChild(nameNode);

            XmlNode playTimeNode = xmlDoc.CreateElement("PlayTime");
            playTimeNode.InnerText = "0:00:00";
            gameNode.AppendChild(playTimeNode);

            XmlNode pathNode = xmlDoc.CreateElement("Path");
            pathNode.InnerText = selectedPath;
            gameNode.AppendChild(pathNode);

            XmlNode previewImageNode = xmlDoc.CreateElement("PreviewImage");
            previewImageNode.InnerText = filename;
            gameNode.AppendChild(previewImageNode);

            // Добавить новый узел игры в XML-конфигурацию
            XmlNode gamesNode = xmlDoc.SelectSingleNode("/Games");
            gamesNode.AppendChild(gameNode);

            Game game = new Game
            {
                Name = filename,
                PlayTime = playTimeNode.InnerText,
                Path = selectedPath,
                Icon = Resources.Load<Sprite>(previewImageNode.InnerText)
            };

            xmlDoc.Save(path);
            CreateGame(game);
        }
        else
        {
            UnityEngine.Debug.LogError("XML file not found: " + path);
        }
    }

    private void CreateGame(Game game)
    {
        GameObject newGameObject = Instantiate(gamePrefab, parent);
        Text gameNameText = newGameObject.GetComponentInChildren<Text>();
        Text timeText = newGameObject.transform.Find("TimeText").GetComponent<Text>();
        Button gameIconImage = newGameObject.GetComponentInChildren<Button>();

        gameNameText.text = game.Name;
        timeText.text = game.PlayTime;
        gameIconImage.image.sprite = game.Icon;
        gameIconImage.onClick.AddListener(() => { ButtonClickHandler(game.Path); });
    }
}


[Serializable]
public class Game
{
    public string Name { get; set; }
    public string PlayTime { get; set; }
    public Sprite Icon { get; set; }
    public string Path { get; set; }
}


