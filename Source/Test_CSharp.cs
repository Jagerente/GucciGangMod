//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using SimpleJSON;
using UnityEngine;

internal class Test_CSharp : MonoBehaviour
{
    private string m_InGameLog = string.Empty;
    private Vector2 m_Position = Vector2.zero;

    private void OnGUI()
    {
        m_Position = GUILayout.BeginScrollView(m_Position, new GUILayoutOption[0]);
        GUILayout.Label(m_InGameLog, new GUILayoutOption[0]);
        GUILayout.EndScrollView();
    }

    private void P(string aText)
    {
        m_InGameLog = m_InGameLog + aText + "\n";
    }

    private void Start()
    {
        Test();
        Debug.Log("Test results:\n" + m_InGameLog);
    }

    private void Test()
    {
        var node = JSONNode.Parse("{\"name\":\"test\", \"array\":[1,{\"data\":\"value\"}]}");
        node["array"][1]["Foo"] = "Bar";
        P("'nice formatted' string representation of the JSON tree:");
        P(node.ToString(string.Empty));
        P(string.Empty);
        P("'normal' string representation of the JSON tree:");
        P(node.ToString());
        P(string.Empty);
        P("content of member 'name':");
        P(node["name"]);
        P(string.Empty);
        P("content of member 'array':");
        P(node["array"].ToString(string.Empty));
        P(string.Empty);
        P("first element of member 'array': " + node["array"][0]);
        P(string.Empty);
        node["array"][0].AsInt = 10;
        P("value of the first element set to: " + node["array"][0]);
        P("The value of the first element as integer: " + node["array"][0].AsInt);
        P(string.Empty);
        P("N[\"array\"][1][\"data\"] == " + node["array"][1]["data"]);
        P(string.Empty);
        var aText = node.SaveToBase64();
        var str2 = node.SaveToCompressedBase64();
        node = null;
        P("Serialized to Base64 string:");
        P(aText);
        P("Serialized to Base64 string (compressed):");
        P(str2);
        P(string.Empty);
        node = JSONNode.LoadFromBase64(aText);
        P("Deserialized from Base64 string:");
        P(node.ToString());
        P(string.Empty);
        var class2 = new JSONClass();
        class2["version"].AsInt = 5;
        class2["author"]["name"] = "Bunny83";
        class2["author"]["phone"] = "0123456789";
        class2["data"][-1] = "First item\twith tab";
        class2["data"][-1] = "Second item";
        class2["data"][-1]["value"] = "class item";
        class2["data"].Add("Forth item");
        class2["data"][1] = class2["data"][1] + " 'addition to the second item'";
        class2.Add("version", "1.0");
        P("Second example:");
        P(class2.ToString());
        P(string.Empty);
        P("I[\"data\"][0]            : " + class2["data"][0]);
        P("I[\"data\"][0].ToString() : " + class2["data"][0].ToString());
        P("I[\"data\"][0].Value      : " + class2["data"][0].Value);
        P(class2.ToString());
    }
}

