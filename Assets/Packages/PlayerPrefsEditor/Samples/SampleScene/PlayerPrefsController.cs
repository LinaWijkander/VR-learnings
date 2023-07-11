using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

    #region Add
    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
    #endregion

    #region Get

    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
    
    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    
    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }
    
    #endregion
    
    #region Remove
    public void RemoveKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    /*public void RemoveInt(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    public void RemoveFloat(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }*/

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
    #endregion
}