namespace Onion.Core.Readers;

/// <summary>
///         (c) Alexandr Yaz
/// Класс логики взаимодействия с файлами
/// </summary>
class TomlReader
{
    // key = "value"
    // intKey = value
    // #comment
    // key = ''' value1 value2 ''' // Mulit-line string
    // key = true/false
    // array = [ "value1", "value2" ]
    //
    // [table]
    // key1 = value
    // key2 = value
    //

    public List<Models.TomlElement> GetValuesFromFile(string tomlText)
    {
        List<Models.TomlElement> tomlResult = new();

        tomlResult = Head(tomlText);

        tomlResult.AddRange(GetTables(tomlText));

        return tomlResult;
    }

    public List<Models.TomlElement> GetTables(string fileText)
    {
        if (fileText.Length <= 2)
            throw new ArgumentException("Text not found");

        //Dictionary<string, List<Models.TomlElement>> tablesDictionary = new();

        List<Models.TomlElement> tablesPairs = new();

        string[] lines = fileText.Split("\n");

        // Num Table
        // Value Lines numbers
        Dictionary<int, List<int>> foundedTables = new();

        // Search Tables
        bool tableFound = false;
        int currentTableNumber = 1;

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("[[mods]]"))
            {
                lines[i] = " ";
                Console.WriteLine(lines[i]);
            }

            switch (tableFound)
            {
                case true:
                    if (lines[i].StartsWith("[["))
                    {
                        currentTableNumber++;
                        foundedTables.Add(currentTableNumber, new List<int> { i });
                        continue;
                    }
                    
                    if (lines[i].Length <= 2)
                    {
                        tableFound = false;
                        continue;
                    }

                    if (!foundedTables.ContainsKey(currentTableNumber))
                        foundedTables.Add(currentTableNumber, new List<int> { i });
                    else
                        foundedTables[currentTableNumber].Add(i);

                    break;

                case false:
                    if (lines[i].StartsWith("[["))
                    {
                        tableFound = true;
                    }

                    break;
            }
        }

        // GetValues
        for (int i = 0; i < foundedTables.Count; i++)
        {
            tablesPairs.Add(new Models.TomlElement("TableNum", $"{i}"));
            Console.WriteLine(foundedTables.ElementAt(i).Value.Count + " + " + i);
            for (int j = 0; j < foundedTables.ElementAt(i).Value.Count; j++)
            {
                var keyValuePair = Element(lines[foundedTables.ElementAt(i).Value[j]]);

                if (keyValuePair != null)
                {
                    tablesPairs.Add(keyValuePair);
                    //if (!tablesDictionary.ContainsKey("Dependence" + i))
                    //    tablesDictionary.Add("Dependence" + i, new List<Models.TomlElement> { keyValuePair });
                    //else
                    //    tablesDictionary["Dependence" + i].Add(keyValuePair);
                }
            }
        }

        return tablesPairs; //tablesDictionary;
    }

    private List<Models.TomlElement> Head(string fileText)
    {
        // Modified: Function name
        List<Models.TomlElement> keyValuePairs = new();

        string[] lines = fileText.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].Contains("[[mods]]"))
                lines[i] = " ";

            if (lines[i].StartsWith("["))
                return keyValuePairs;

            Models.TomlElement? pair = Element(lines[i]);
            if (pair != null)
                keyValuePairs.Add(pair);
        }

        return keyValuePairs;
    }

    private Models.TomlElement? Element(string line)
    {
        // Modified: Function name.
        // Modified: invert IF
        if (!line.Contains('=')) return null;
        
        var keyValue = line.Split('=', 2);

        string key = keyValue[0].Trim();
        string value = keyValue[1];

        string replace = value.Replace('\"', ' ');

        // Modified: Join declaration/assignment
        Models.TomlElement keyValuePair = new(key, value);

        return keyValuePair;

    }
}

/*
⡏⠉⠉⠉⠉⠉⠉⠋⠉⠉⠉⠉⠉⠉⠋⠉⠉⠉⠉⠉⠉⠉⠉⠉⠉⠙⠉⠉⠉⠹
⡇⢸⣿⡟⠛⢿⣷⠀⢸⣿⡟⠛⢿⣷⡄⢸⣿⡇⠀⢸⣿⡇⢸⣿⡇⠀⢸⣿⡇⠀
⡇⢸⣿⣧⣤⣾⠿⠀⢸⣿⣇⣀⣸⡿⠃⢸⣿⡇⠀⢸⣿⡇⢸⣿⣇⣀⣸⣿⡇⠀
⡇⢸⣿⡏⠉⢹⣿⡆⢸⣿⡟⠛⢻⣷⡄⢸⣿⡇⠀⢸⣿⡇⢸⣿⡏⠉⢹⣿⡇⠀
⡇⢸⣿⣧⣤⣼⡿⠃⢸⣿⡇⠀⢸⣿⡇⠸⣿⣧⣤⣼⡿⠁⢸⣿⡇⠀⢸⣿⡇⠀
⣇⣀⣀⣀⣀⣀⣀⣄⣀⣀⣀⣀⣀⣀⣀⣠⣀⡈⠉⣁⣀⣄⣀⣀⣀⣠⣀⣀⣀⣰
⣇⣿⠘⣿⣿⣿⡿⡿⣟⣟⢟⢟⢝⠵⡝⣿⡿⢂⣼⣿⣷⣌⠩⡫⡻⣝⠹⢿⣿⣷
⡆⣿⣆⠱⣝⡵⣝⢅⠙⣿⢕⢕⢕⢕⢝⣥⢒⠅⣿⣿⣿⡿⣳⣌⠪⡪⣡⢑⢝⣇
⡆⣿⣿⣦⠹⣳⣳⣕⢅⠈⢗⢕⢕⢕⢕⢕⢈⢆⠟⠋⠉⠁⠉⠉⠁⠈⠼⢐⢕⢽
⡗⢰⣶⣶⣦⣝⢝⢕⢕⠅⡆⢕⢕⢕⢕⢕⣴⠏⣠⡶⠛⡉⡉⡛⢶⣦⡀⠐⣕⢕
⡝⡄⢻⢟⣿⣿⣷⣕⣕⣅⣿⣔⣕⣵⣵⣿⣿⢠⣿⢠⣮⡈⣌⠨⠅⠹⣷⡀⢱⢕
⡝⡵⠟⠈⢀⣀⣀⡀⠉⢿⣿⣿⣿⣿⣿⣿⣿⣼⣿⢈⡋⠴⢿⡟⣡⡇⣿⡇⡀⢕
⡝⠁⣠⣾⠟⡉⡉⡉⠻⣦⣻⣿⣿⣿⣿⣿⣿⣿⣿⣧⠸⣿⣦⣥⣿⡇⡿⣰⢗⢄
⠁⢰⣿⡏⣴⣌⠈⣌⠡⠈⢻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣬⣉⣉⣁⣄⢖⢕⢕⢕
⡀⢻⣿⡇⢙⠁⠴⢿⡟⣡⡆⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣵⣵⣿
⡻⣄⣻⣿⣌⠘⢿⣷⣥⣿⠇⣿⣿⣿⣿⣿⣿⠛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
⣷⢄⠻⣿⣟⠿⠦⠍⠉⣡⣾⣿⣿⣿⣿⣿⣿⢸⣿⣦⠙⣿⣿⣿⣿⣿⣿⣿⣿⠟
⡕⡑⣑⣈⣻⢗⢟⢞⢝⣻⣿⣿⣿⣿⣿⣿⣿⠸⣿⠿⠃⣿⣿⣿⣿⣿⣿⡿⠁⣠
⡝⡵⡈⢟⢕⢕⢕⢕⣵⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣶⣿⣿⣿⣿⣿⠿⠋⣀⣈⠙
⡝⡵⡕⡀⠑⠳⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⢉⡠⡲⡫⡪⡪⡣
*/