using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tip_Myself
{
    public class NetworkUtil
    {
        public NetworkUtil()
        {
        }

        internal static string baseUrl = "https://tipproj.azurewebsites.net/api/";
        internal static async Task<string> PostGeneralAsyc(string actionName, string mrawData)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);
                  
                    var response = await mclient.PostAsync($"{actionName}/", new StringContent(mrawData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }

        internal static async Task<string> PostGeneralAsycTip(string actionName, string mrawData, string queryString, string acctNumber)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);

                    string mmr = $"{actionName}?{queryString}={acctNumber}";
                    var response = await mclient.PostAsync($"{actionName}?{queryString}={acctNumber}", new StringContent(mrawData, Encoding.UTF8, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }


        internal static async Task<string> GetGeneralAsycTip(string actionName,string queryString, string acctNumber)
        {
            string mresult = "";
            try
            {
                using (var mclient = new HttpClient() { BaseAddress = new Uri(baseUrl) })
                {
                    mclient.Timeout = TimeSpan.FromMinutes(1);

   
                    var response = await mclient.GetAsync($"{actionName}?{queryString}={acctNumber}").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                mresult = "";
            }
            return mresult;
        }


        internal static Task<string> GetStringAsync(string v, string log)
        {
            throw new NotImplementedException();
        }
    }
}
