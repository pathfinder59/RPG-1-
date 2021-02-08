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

    // Start is called before the first frame update
    void Awake()
    {
        EventManager.On("UpdataQuestPage", UpdateContent);
        EventManager.On("CloseQuestPage", SetActiveContent);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateContent(GameObject obj = null)
    {
        QuestManager questMgr = PlayerManager.Instance._questManager;
        Quest quest = null;
        if (questMgr.currentQuests[data._type-'0'].ContainsKey(data.client + data.questIdx))
            quest = questMgr.currentQuests[data._type-'0'][data.client + data.questIdx];
        int id = DialogManager.Instance.ActionObject.GetComponent<ObjData>().id;
        string str = quest == null? "수락가능" : (quest.processRate == 2? "진행중": data._type == '1' && data.client == id? "진행중" : "완료");

        _text.text = str + (data.client + data.questIdx).ToString();
    }
    public void SetActiveContent(GameObject obj = null)
    {
        gameObject.SetActive(false);
    }
}
