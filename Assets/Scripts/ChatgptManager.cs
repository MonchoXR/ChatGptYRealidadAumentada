using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.Events;
public class ChatgptManager : MonoBehaviour
{
    public OnResponseEvent OnResponse;
     [System.Serializable] public class OnResponseEvent: UnityEvent<string>{}
    // [SerializeField] private UnityEvent MiRespEvent; //Importante hacer esta definción
    string context =
        "Hoy serás batman, deberás ser amable y cuidadoso ya que asistirás a niños. Principamente ayudarás en" +
        "resolver acertijos y preguntas solo de mátemáticas. Solo reponderas preguntas relacionados a matemáticas"+ 
        " No podrás responder sobre otros cursos ni temas de historia, politica o parecido";

    private OpenAIApi openAI = new OpenAIApi();
    public List<ChatMessage> messages = new List<ChatMessage>();

    public async void AskChatGPT(string newText)
    {
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = context+ newText;
        newMessage.Role = "user";

        
        messages.Add(newMessage);
 
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Model = "gpt-3.5-turbo";
        request.Messages = messages;
       
        // Make the API request
        var response = await openAI.CreateChatCompletion(request);

        if (response.Choices != null && response.Choices.Count > 0)
        {
              
            var chatResponse = response.Choices[0].Message;
    ;
           messages.Add(chatResponse);


            OnResponse.Invoke(chatResponse.Content);

            // Debug.Log(chatResponse.Content);
            //    Debug.Log(chatResponse.Role);
            // Debug.Log(messages.Count);
         //   messages.Clear();

        }
    }

        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
