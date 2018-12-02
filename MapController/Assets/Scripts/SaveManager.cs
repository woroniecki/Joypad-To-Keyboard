using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour {

    const string lastPathSaveKey = "pathSaveKey";

    SimulatorManager sim;

    private void Awake()
    {
        sim = GetComponent<SimulatorManager>();
    }

    public void Save()
    {
        SaveFileDialog window = new SaveFileDialog();

        window.InitialDirectory = GetInitPath();
        window.Filter = "json files (*.json)|*.json";
        window.FilterIndex = 1;
        
        if (window.ShowDialog() == DialogResult.OK && window.FileName != "")
        {
            try
            {
                StreamWriter sw = new StreamWriter(Path.GetFullPath(window.FileName));
                sw.WriteLine(JsonUtility.ToJson(sim.data));
                sw.Close();
                UI.UIController.instance.SetName(
                    System.IO.Path.GetFileNameWithoutExtension(window.FileName)
                    );
                PlayerPrefs.SetString(lastPathSaveKey, window.FileName);
            }
            catch (Exception e)
            {
                Debug.LogError (e);
                ErrorMessage(e.ToString());
            }
        }
    }

    public void Load()
    {
        OpenFileDialog window = new OpenFileDialog();

        window.InitialDirectory = GetInitPath();
        window.Filter = "json files (*.json)|*.json";
        window.FilterIndex = 1;

        string json = "";

        if (window.ShowDialog() == DialogResult.OK && window.FileName != "")
        {
            try
            {
                StreamReader sr = new StreamReader(Path.GetFullPath(window.FileName));
                json = sr.ReadToEnd();
                sr.Close();
                PlayerPrefs.SetString(lastPathSaveKey, window.FileName);
            }
            catch (Exception e)
            {
                json = "";
                Debug.LogError(e);
                ErrorMessage(e.ToString());
            }
        }

        try
        {
            if (!string.IsNullOrEmpty(json))
            {
                SimulatorManager.Data data = JsonUtility.FromJson<SimulatorManager.Data>(json);
                sim.data = data;
                UI.UIController.instance.DisableAllViews();
                UI.UIController.instance.SetName(
                        System.IO.Path.GetFileNameWithoutExtension(window.FileName)
                        );
            }
        }
        catch (Exception e)
        {
            Debug.LogError (e);
            ErrorMessage(e.ToString());
        }
    }

    static string GetInitPath()
    {
        try
        {
            string path = Path.GetDirectoryName(PlayerPrefs.GetString(lastPathSaveKey, ""));
            if (Directory.Exists(path))
            {
                return path;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return UnityEngine.Application.dataPath;
    }

    static void ErrorMessage(string msg, string name = "Error")
    {
        MessageBox.Show(msg,
        name,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error
        );
    }
}
