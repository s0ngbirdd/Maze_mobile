using UnityEngine;
using System.IO;
using System.Xml;
using TMPro;

public enum Languages
{
    EN,
    UA
}

public class LocalizationController : MonoBehaviour
{
    // Serialize
    [SerializeField] private string _xmlPathPattern = "//body";

    [Header("Text objects")]
    [SerializeField] private TextMeshProUGUI _UITextPause;
    [SerializeField] private TextMeshProUGUI _UITextShield;
    [SerializeField] private TextMeshProUGUI _UITextResume;
    [SerializeField] private TextMeshProUGUI _UITextExit;
    [SerializeField] private TextMeshProUGUI _UITextLanguages;

    [Header("EN Xml files")]
    [SerializeField] private TextAsset _xmlRawFilePauseEN;
    [SerializeField] private TextAsset _xmlRawFileShieldEN;
    [SerializeField] private TextAsset _xmlRawFileResumeEN;
    [SerializeField] private TextAsset _xmlRawFileExitEN;
    [SerializeField] private TextAsset _xmlRawFileLanguagesEN;

    [Header("UA Xml files")]
    [SerializeField] private TextAsset _xmlRawFilePauseUA;
    [SerializeField] private TextAsset _xmlRawFileShieldUA;
    [SerializeField] private TextAsset _xmlRawFileResumeUA;
    [SerializeField] private TextAsset _xmlRawFileExitUA;
    [SerializeField] private TextAsset _xmlRawFileLanguagesUA;

    // Private
    private string _pauseData;
    private string _shieldData;
    private string _resumeData;
    private string _exitData;
    private string _languagesData;
    private Languages _currentLanguage;

    private void Start()
    {
        _currentLanguage = SaveLoadSystem.Instance.LoadLanguage();
        SetCurrentLanguage();
    }

    // XmlFileLoader
    private void ParseXmlFile(string xmlData, TextMeshProUGUI textMeshProUGUI)
    {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(new StringReader(xmlData));

        XmlNodeList xmlNodeList = xmlDocument.SelectNodes(_xmlPathPattern);

        foreach (XmlNode node in xmlNodeList)
        {
            XmlNode text = node.FirstChild;
            XmlNode fontSize = text.NextSibling;

            textMeshProUGUI.text = text.InnerXml;
            textMeshProUGUI.fontSize = float.Parse(fontSize.InnerXml);
        }
    }

    private void ParseAllXmlFiles()
    {
        ParseXmlFile(_pauseData, _UITextPause);
        ParseXmlFile(_shieldData, _UITextShield);
        ParseXmlFile(_resumeData, _UITextResume);
        ParseXmlFile(_exitData, _UITextExit);
        ParseXmlFile(_languagesData, _UITextLanguages);
    }

    public void ChangeLanguage()
    {
        if (_currentLanguage == Languages.EN)
        {
            _currentLanguage = Languages.UA;

            _pauseData = _xmlRawFilePauseUA.text;
            _shieldData = _xmlRawFileShieldUA.text;
            _resumeData = _xmlRawFileResumeUA.text;
            _exitData = _xmlRawFileExitUA.text;
            _languagesData = _xmlRawFileLanguagesUA.text;

            ParseAllXmlFiles();
        }
        else if (_currentLanguage == Languages.UA)
        {
            _currentLanguage = Languages.EN;

            _pauseData = _xmlRawFilePauseEN.text;
            _shieldData = _xmlRawFileShieldEN.text;
            _resumeData = _xmlRawFileResumeEN.text;
            _exitData = _xmlRawFileExitEN.text;
            _languagesData = _xmlRawFileLanguagesEN.text;

            ParseAllXmlFiles();
        }

        SaveLoadSystem.Instance.SaveLanguage(_currentLanguage);
    }

    private void SetCurrentLanguage()
    {
        if (_currentLanguage == Languages.EN)
        {
            _pauseData = _xmlRawFilePauseEN.text;
            _shieldData = _xmlRawFileShieldEN.text;
            _resumeData = _xmlRawFileResumeEN.text;
            _exitData = _xmlRawFileExitEN.text;
            _languagesData = _xmlRawFileLanguagesEN.text;

            ParseAllXmlFiles();
        }
        else if (_currentLanguage == Languages.UA)
        {
            _pauseData = _xmlRawFilePauseUA.text;
            _shieldData = _xmlRawFileShieldUA.text;
            _resumeData = _xmlRawFileResumeUA.text;
            _exitData = _xmlRawFileExitUA.text;
            _languagesData = _xmlRawFileLanguagesUA.text;

            ParseAllXmlFiles();
        }
    }
}
