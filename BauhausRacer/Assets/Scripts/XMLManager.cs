using System.Collections;
using UnityEngine;

using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

namespace BauhausRacer {

public class XMLManager : MonoBehaviour {

	public static XMLManager instance;

	private string dataPath; 

	void Awake(){
		instance = this;
		dataPath = Application.streamingAssetsPath+"\\highscore_data.xml";
		if(File.Exists(dataPath)){
				XMLManager.instance.Load();
			}
	}

	public HighscoreDatabase highscoreDatabase;

	public void Save(){
		XmlSerializer serializer = new XmlSerializer(typeof (HighscoreDatabase));
		FileStream fs = new FileStream(dataPath, FileMode.Create);
		serializer.Serialize(fs, highscoreDatabase);
		fs.Close();
	}

	public void Load(){
		XmlSerializer serializer = new XmlSerializer(typeof (HighscoreDatabase));
		FileStream fs = new FileStream(dataPath, FileMode.Open);
		highscoreDatabase = serializer.Deserialize(fs) as HighscoreDatabase;
		fs.Close();
	}
}

	[System.Serializable]
	public class HighScoreEntry {
		public string name;
		public float time;

        public HighScoreEntry(string name, float time)
        {
            this.name = name;
            this.time = time;
        }

		public HighScoreEntry(){

		}
    }

	[System.Serializable]
	public class HighscoreDatabase {
		[XmlArray("Highscore")]
		public List <HighScoreEntry> list  = new List <HighScoreEntry>();
		public int maxHighscoreEntries = 10;

		public void AddEntry(string name, float time){
			list.Add(new HighScoreEntry(name, time));
			list.Sort(delegate(HighScoreEntry h1, HighScoreEntry h2){
				return h1.time.CompareTo(h2.time);
			});
			if(list.Count > maxHighscoreEntries){
				list.RemoveAt(maxHighscoreEntries);
			}
			XMLManager.instance.Save();
		}

		public void RemoveEntry(int index){
			list.RemoveAt(index);
			XMLManager.instance.Save();
		}

		public void ClearHighscore(){
			list.Clear();
			XMLManager.instance.Save();
		}

		public HighScoreEntry GetLastEntry(){
			return list[maxHighscoreEntries-1];
		}

		
    }

}