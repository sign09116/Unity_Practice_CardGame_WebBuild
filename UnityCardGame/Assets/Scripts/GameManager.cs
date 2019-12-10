using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region KID
    [Header("卡片陣列")]
    public GameObject[] cards;
    private int player, pc;     // 玩家、電腦卡片編號

    /// <summary>
    /// 玩家取得卡片
    /// </summary>
    public void PlayerGetCard()
    {
        player = GetCard(new Vector3(0, -3, 0));

        Invoke("PcGetCard", 1.5f);
        Invoke("GameWinner", 2.5f);
    }

    /// <summary>
    /// 電腦取得卡片
    /// </summary>
    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="pos">卡片座標</param>
    /// <returns>取得的卡片編號</returns>
    private int GetCard(Vector3 pos)
    {
        int r = Random.Range(0, cards.Length);

        Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

        return r + 1;
    }
    #endregion

    #region 練習區域
    public GameObject final;
    public Text textFinal;

    public void Replay()
    {
        SceneManager.LoadScene("練習場景");
    }

    /// <summary>
    /// 勝負顯示：使用玩家與電腦取得卡片判斷獲勝、平手或失敗
    /// 玩家卡片編號：player
    /// 電腦卡片編號：pc
    /// 顯示結算畫面
    /// </summary>
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
    #endregion
}
