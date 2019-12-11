using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    #region KID
    [Header("卡片陣列")]
    public GameObject[] cards;
    [Header("發牌按鈕")]
    public Button btnGetCard;
    [Header("結算畫面")]
    public GameObject EndInterFace;
    [Header("勝負判定文字")]
    public Text JudgeText;
    public int State;

    private int player, pc;     // 玩家、電腦卡片編號


    private void Start()
    {

        aud = GetComponent<AudioSource>();
        EndInterFace.SetActive(false);

    }

    /// <summary>
    /// 玩家取得卡片
    /// </summary>
    public void PlayerGetCard()
    {
        btnGetCard.interactable = false;
        player = GetCard(new Vector3(0, -3, 0));

        Invoke("PcGetCard", 1.5f);

    }

    /// <summary>
    /// 電腦取得卡片
    /// </summary>
    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
        Invoke("GameWinner", 1.5f);
        Invoke("GameOver", 1.5f);


    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="pos">卡片座標</param>
    /// <returns>取得的卡片編號</returns>
    private int GetCard(Vector3 pos)
    {
        aud.PlayOneShot(soundGetCard);

        int r = Random.Range(0, cards.Length);

        Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

        return r + 1;
    }
    #endregion

    #region 練習區域
    [Header("音效區域")]
    public AudioClip soundGetCard;  // 發牌
    public AudioClip soundWin;      // 獲勝
    public AudioClip soundLose;     // 失敗
    public AudioClip soundTie;      // 平手

    private AudioSource aud;        // 音效來源：喇叭


    /// <summary>
    /// 勝負顯示：使用玩家與電腦取得卡片判斷獲勝、平手或失敗
    /// 玩家卡片編號：player
    /// 電腦卡片編號：pc
    /// 顯示結算畫面

    /// </summary>
    /// <summary>
    /// 判定遊戲勝負 當State為負值 則玩家失敗 反之玩家勝利 但A為例外持有A者為最大
    /// </summary>
    void GameWinner()
    {
        State = player - pc;
        // Time.timeScale = 0;
        if (State < 0)
        {
            if (player == 1)
            {
                JudgeText.text = "勝った！";
                aud.PlayOneShot(soundWin);
                return;
            }
            JudgeText.text = "負けちゃった～～";
            aud.PlayOneShot(soundLose);
            return;

        }
        else if (State > 0)
        {
            if (pc == 1)
            {
                JudgeText.text = "残念～～";
                aud.PlayOneShot(soundLose);
                return;
            }
            JudgeText.text = "勝った！";
            aud.PlayOneShot(soundWin);
            return;
        }
        else
        {
            JudgeText.text = "引き分けですね！";
            aud.PlayOneShot(soundTie);
        }

    }
    public void ReGame()
    {
        //Time.timeScale = 1;
        btnGetCard.interactable = true;
        SceneManager.LoadScene("練習場景");
    }
    void GameOver()
    {
        EndInterFace.SetActive(true);
    }
    #endregion
}
