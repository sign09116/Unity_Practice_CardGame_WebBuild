using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] cards;
    private int player, pc;

    public GameObject final;
    public Text textFinal;

    public void PlayerGetCard()
    {
        player = GetCard(new Vector3(0, -3, 0));

        Invoke("PcGetCard", 1.5f);
        Invoke("GameWinner", 2.5f);
    }

    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
    }

    private int GetCard(Vector3 pos)
    {
        int r = Random.Range(0, cards.Length);

        Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

        return r + 1;
    }

    private void GameWinner()
    {
        if (player > pc)
        {
            textFinal.text = "恭喜你獲勝啦!";
        }
        else if (player == pc)
        {
            textFinal.text = "居然平手惹!!!";
        }
        else
        {
            textFinal.text = "輸惹......";
        }

        final.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("練習場景");
    }
}
