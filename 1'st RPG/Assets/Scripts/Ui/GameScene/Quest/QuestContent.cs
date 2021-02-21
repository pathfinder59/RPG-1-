using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using common;
public class QuestContent : MonoBehaviour , IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public static QuestContent clickedContent;
    public QuestData data;
    
    [SerializeField]
    Image contentImage;
    [SerializeField]
    Text _text;

    void Awake()
    {
        EventManager.On("UpdataQuestPage", UpdateContent);
        EventManager.On("CloseQuestPage", SetActiveContent);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public static void ResetPointer()
    {
        clickedContent = null;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

        clickedContent = this;
        EventManager.Emit("QuestContentClick");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        contentImage.color = contentImage.color + new Color(0.1f, 0.1f, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        contentImage.color = contentImage.color - new Color(0.1f, 0.1f, 0.1f);
    }

    public void UpdateContent(GameObject obj = null)
    {
        Quest quest = null;
        if (QuestManager.Instance.currentQuests[data._type-'0'].ContainsKey(data.questIdx))
            quest = QuestManager.Instance.currentQuests[data._type-'0'][data.questIdx];
        int id = GameSceneManager.Instance.ActionObject.GetComponent<ObjData>().id;
        string str = quest == null? "[수락가능]" : (quest.processRate == 2? "[진행중]": !(data._type == '1' && data.target == id)? "[완료]"  : data.client == data.target? "[완료]": "[진행중]");

        _text.text = str + data.title;
    }
    public void SetActiveContent(GameObject obj = null)
    {
        gameObject.SetActive(false);
    }
}
