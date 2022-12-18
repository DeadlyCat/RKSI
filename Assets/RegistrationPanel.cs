using System;
using System.Collections;
using System.Collections.Generic;
using AdvancedInputFieldPlugin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationPanel : Panel
{
   [SerializeField] private AdvancedInputField loginInput;
   [SerializeField] private AdvancedInputField passwordInput;
   [SerializeField] private AdvancedInputField passwordConfimInput;
   [SerializeField] private AdvancedInputField mailInput;
   [SerializeField] private AdvancedInputField fioInput;
   [SerializeField] private AdvancedInputField organizationInput;
   [SerializeField] private AdvancedInputField postInput;
   [SerializeField] private TMP_Dropdown statysDropDown;
   [SerializeField] private Toggle getSertificate;
   [SerializeField] private Button btnSendRequist;

   private AnswerServer answerServer;
   private void Start()
   {
      btnSendRequist.onClick.AddListener(() =>
      {
         if (passwordInput.Text == passwordConfimInput.Text)
         {
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("username", loginInput.Text);
            formData.Add("password", passwordInput.Text);
            formData.Add("mail", mailInput.Text);
            formData.Add("fio", fioInput.Text);
            formData.Add("organization", organizationInput.Text);
            formData.Add("post", postInput.Text);
            formData.Add("status", statysDropDown.value.ToString()); // 0 - organizator \ 1 - speaker \2 -user
            formData.Add("is_need_sert_by_default", getSertificate.isOn.ToString()); 
            ServerContector.SendRequist(TypeRequist.Post,"register",formData,out answerServer);
            answerServer.Accept.AddListener((answer) =>
            {
               Auntifitation aunt = JsonUtility.FromJson<Auntifitation>(answer);
               User.SetJwtToken(aunt.token);
               Debug.Log(User.JwtToken);
            });
         }
         else
         {
            Debug.Log("Пароли не совпадают");
         }
         
      });
   }
}
[Serializable]
public struct Auntifitation
{
   public string id;
   public string token;

   public Auntifitation(string _id, string jwt)
   {
      id = _id;
      token = jwt;
   }
}
