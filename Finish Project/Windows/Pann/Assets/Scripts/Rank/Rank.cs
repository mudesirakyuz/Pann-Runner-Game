using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Rank : MonoBehaviour
{
    private Transform finishLine;
    public Transform player, player1, player2, player3, player4, player5;
    public Transform player6, player7, player8, player9, player10;
    public TextMeshProUGUI tmp_player, tmp_player1, tmp_player2, tmp_player3, tmp_player4, tmp_player5;
    public TextMeshProUGUI tmp_player6, tmp_player7, tmp_player8, tmp_player9, tmp_player10;

    private Transform[] players;
    private Dictionary<int, float> distance = new Dictionary<int, float>();

    private int[] rank = new int[11];
    // Start is called before the first frame update
    void Start()
    {
        players = new Transform[] { player, player1, player2, player3, player4, player5,
                                          player6, player7, player8, player9, player10};

        finishLine = GameObject.FindGameObjectWithTag("Finish").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Length; i++)// Sözlüðe, her oyuncunun x deðeri bitiþ çizgisinin x deðerinden çýkarýlarak eklenir 
        {
            Adding(i, players[i]);
        }

        var items = distance.OrderBy(i => i.Value);// Sýralama
        int j = 0;
        foreach (var item in items)// Sýralamayý aktar
        {
            rank[j] = item.Key;
            j++;
        }

        for (int i = 0; i < rank.Length; i++)//Her bir texte kaçýncý olduðunu yaz
        {
            switch (rank[i])
            {
                case 0: tmp_player.text = (i + 1).ToString(); break;
                case 1: tmp_player1.text = (i + 1).ToString(); break;
                case 2: tmp_player2.text = (i + 1).ToString(); break;
                case 3: tmp_player3.text = (i + 1).ToString(); break;
                case 4: tmp_player4.text = (i + 1).ToString(); break;
                case 5: tmp_player5.text = (i + 1).ToString(); break;
                case 6: tmp_player6.text = (i + 1).ToString(); break;
                case 7: tmp_player7.text = (i + 1).ToString(); break;
                case 8: tmp_player8.text = (i + 1).ToString(); break;
                case 9: tmp_player9.text = (i + 1).ToString(); break;
                case 10: tmp_player10.text = (i + 1).ToString(); break;
                default:
                    break;
            }
        }
        distance.Clear();
    }

    private void Adding(int number, Transform player)
    {
        distance.Add(number, System.Math.Abs(finishLine.position.x) - System.Math.Abs(player.position.x));
    }
}
