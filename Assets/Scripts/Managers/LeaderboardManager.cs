using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{

	public static System.Action OnNewHighScore;
	private List<int> scores;
	public List<int> Scores => scores;

	private void Awake()
	{
		if (StorageMgr.Exists(StorageMgr.SCORES))
		{
			scores = StorageMgr.LoadData<List<int>>(StorageMgr.SCORES);
			scores = scores.OrderByDescending(i => i).ToList();
		}
		else
		{
			scores = new List<int>() { 3,2,1};
			StorageMgr.SaveData(scores,StorageMgr.SCORES);
		}
	}

	public void SetNewScores(int score)
	{
		if (score > scores.Min()) {
			scores.Add(score);
			scores = scores.OrderByDescending(i => i).ToList().GetRange(0, 3);
			StorageMgr.SaveData(scores, StorageMgr.SCORES);
			OnNewHighScore?.Invoke();
		}
	}

}

public class StorageMgr 
{

	public const string SCORES = "Scores";

	/// <summary>
	/// Metodo que persiste un archivo binario del tiempo que se le pasa como objeto T y con el nombre que le pasamos por parametro
	/// Ej: SaveData(GameData, "UG_data");
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="data"></param>
	/// <param name="dataName"></param>
	public static void SaveData<T>(T data, string dataName)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath + "/" + dataName + ".dat", FileMode.Create);

		bf.Serialize(stream, data);
		stream.Close();

	}

	/// <summary>
	/// Metodo que carga un archivo de la ruta de persistencia.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="dataName"></param>
	/// <returns></returns>
	public static T LoadData<T>(string dataName)
	{
		string datapath = Application.persistentDataPath + "/" + dataName + ".dat";
		if (File.Exists(datapath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(datapath, FileMode.Open);

			T data = (T)bf.Deserialize(stream);
			stream.Close();

			return data;
		}

		throw new System.Exception("El archivo no existe");
	}

	public static bool Exists(string dataName)
	{
		string datapath = Application.persistentDataPath + "/" + dataName + ".dat";
		return File.Exists(datapath);
	}
}
