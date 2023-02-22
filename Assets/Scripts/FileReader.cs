using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Globalization;

public class FileReader : MonoBehaviour
{
    private const int NUMBER_OF_DATES_COLUMN = 2;
    private const int NUMBER_OF_OILTRACKS_COLUMN = 7;
    private const int NUMBER_OF_LINE_TO_START = 1;
    private const char COLUMNS_DELIMETER = ',';

    [SerializeField] private string _filename;
    [SerializeField] private string[] _textLines;

    [field: SerializeField] public List<double> oilTracks{ get; private set;}
    [SerializeField] private List<string> _dates = new List<string>();

    void Awake()
    {
        oilTracks = new List<double>();

        var watch = new Stopwatch();
        watch.Start();

        ExtractLinesFromFile();
        ExtractColumnsFromLines();


        watch.Stop();
        UnityEngine.Debug.Log($"time Spent: {watch.ElapsedMilliseconds}");
    }
    private void ExtractLinesFromFile()
    {
        string path = Application.dataPath + "/TextFile/" + _filename;

        if (File.Exists(path))
            _textLines = File.ReadAllLines(path);
        else
            UnityEngine.Debug.LogError("File not found: " + path);
    }

    private void ExtractColumnsFromLines()
    {
        CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

        for (int l = NUMBER_OF_LINE_TO_START; l < _textLines.Length; l++)
        {
            string[] columns = _textLines[l].Split(COLUMNS_DELIMETER);

            TryParseOilTrack(culture, columns);
        }
    }

    private void TryParseOilTrack(CultureInfo culture, string[] columns)
    {
        if (double.TryParse(columns[NUMBER_OF_OILTRACKS_COLUMN],
            NumberStyles.Float, culture, out double oilTrack))
            oilTracks.Add(oilTrack);
    }
}