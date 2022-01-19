using System;
using Android.Content;
using Newtonsoft.Json;
using Tip_Myself.Models;

namespace Tip_Myself.Utils
{
    public class MemoryManager
    {
        private static MemoryManager instance = null;
        private static string DEFAULT_VALUE = "";
        private static readonly object padlock = new object();
        private static ISharedPreferences preferences;
        private static ISharedPreferencesEditor editor;
        MemoryManager()
        {
        }

        public static MemoryManager Instance(Context context)
        {
            lock (padlock)
            {
                if (preferences == null)
                {
                    instance = new MemoryManager();
                    preferences = context.GetSharedPreferences(context.PackageName, FileCreationMode.Private);
                    editor = preferences.Edit();

                }
                return instance;
            }
        }

        public void savePreference(string key, string value)
        {
            editor.PutString(key, value);
            editor.Commit();
        }
        public void savePreference(string key, bool value)
        {
            editor.PutBoolean(key, value);
            editor.Commit();
        }

        public void removePreference(string key)
        {
            editor.Remove(key)
;
            editor.Commit();
        }
        

        public void setUserAccount(string key, LoginResponseModel user)
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }

      

        public LoginResponseModel getUser(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<LoginResponseModel>(user);
            }
            else return null;

        }

        //.......Start...................

        public void setToggleStatus(string key, ToggleTipResponseModel user)
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }



        public ToggleTipResponseModel getToggleStatus(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<ToggleTipResponseModel>(user);
            }
            else return null;

        }


        public void setTipStatus(string key, TipActivationResponseModel user)
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }



        public TipActivationResponseModel getTip(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<TipActivationResponseModel>(user);
            }
            else return null;

        }

        public void setSendMoney(string key, SendMoneyResponseModel user)
        {
            editor.PutString(key, JsonConvert.SerializeObject(user));
            editor.Commit();
        }



        public SendMoneyResponseModel getSendMoney(string key)
        {
            string user = preferences.GetString(key, DEFAULT_VALUE);
            if (user != null)
            {
                return JsonConvert.DeserializeObject<SendMoneyResponseModel>(user);
            }
            else return null;

        }

        //..........End.......................

        public bool clearPreference()
        {
            return editor.Clear().Commit();
        }


        public string retreivePreference(string key)
        {
            return preferences.GetString(key, DEFAULT_VALUE);
        }

    }

}
