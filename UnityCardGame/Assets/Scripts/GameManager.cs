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
        Invoke("GameWinner", 2.5f);
    }

    /// <summary>
    /// 電腦取得卡片
    /// </summary>
    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
        State = player - pc;
        StartCoroutine(GameWinner());
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
    IEnumerator GameWinner()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
        if (State > 0)
        {
            print("Win");
            JudgeText.text = "Win";
            yield return 0;

        }
        if (State < 0)
        {
            print("Lose");
            JudgeText.text = "Lose";
            yield return 2;
        }
        else
        {
            print("draw");
            JudgeText.text = "Draw";
            yield return 1;
        }


        EndInterFace.SetActive(true);



    }
    public void ReGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("練習場景");
    }
    #endregion
}
