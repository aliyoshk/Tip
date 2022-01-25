using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Tip_Myself.Models;
using Tip_Myself.Utils;

namespace Tip_Myself
{
    [Activity(Label = "SendMoney")]
    public class SendMoney : Activity
    {
        Button btn;
        RadioGroup radioGroup, pri;
        TextView acct_no, acct_bal;
        EditText amountToSend, acctNoInput;
        RadioButton priAccount;
        EditText pass; //Dialog Activity EditText
        TextView trsDesc;
        ImageView back;
        TextView acctDetails;

        //private LoginResponseModel mIntentData;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_send_money);

            // Create your application here
            btn = FindViewById<Button>(Resource.Id.btn_makeTransfer);
            ViewGroup viewGroup = FindViewById<ViewGroup>(Android.Resource.Id.Content);
            radioGroup = FindViewById<RadioGroup>(Resource.Id.radioGroup);
            pri = FindViewById<RadioGroup>(Resource.Id.pri);

            acct_no = FindViewById<TextView>(Resource.Id.acct_no);
            acct_bal = FindViewById<TextView>(Resource.Id.textView8);
            amountToSend = FindViewById<EditText>(Resource.Id.amountToSend);
            acctNoInput = FindViewById<EditText>(Resource.Id.acctNoInput);
            priAccount = FindViewById<RadioButton>(Resource.Id.priAccount);
            acctDetails = FindViewById<TextView>(Resource.Id.acctDetails);


            back = FindViewById<ImageView>(Resource.Id.imageView7);

            back.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Dashboard));
                StartActivity(intent);
            };

            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                acct_bal.Text = user.acctBalance.ToString();
                acct_no.Text = user.acctNumber + " |  TIER 3";
            }


            btn.Click += delegate
                {
                    LayoutInflater layoutInflater = LayoutInflater.From(this);
                    View view = layoutInflater.Inflate(Resource.Layout.transfer_dialog, null);
                    AlertDialog.Builder builder =  new AlertDialog.Builder(this);
                
                    builder.SetView(view);
                    EditText edt_pass = view.FindViewById<EditText>(Resource.Id.editText2);
                    trsDesc = FindViewById<TextView>(Resource.Id.trDia);
                    //trsDesc.Text = "You’re about to send " + amountToSend.Text.ToString() + " from account 1234561012 to  MICHAEL WILLS of KUDA BANK";

                    string ai = acctNoInput.Text.ToString();

                    var amt = amountToSend.Text.ToString();
                    if (!string.IsNullOrEmpty(amt))
                    {
                        decimal a = Convert.ToDecimal(amountToSend.Text.ToString());
   
                        if (!Empty())
                        {
                            builder.SetCancelable(false)
                            .SetPositiveButton("Send", delegate
                            {
                         
                                string pin = edt_pass.Text.ToString();
                                if(pin.Trim().Length < 4)
                                {
                                    Toast.MakeText(this, "Password should be 4 digit", ToastLength.Short).Show();
                                }
                                else
                                {
                                    validateActivation(ai, a, pin);
                                }

                                //Toast.MakeText(this, "Clicked", ToastLength.Short).Show();
                                
                            })
                            .SetNegativeButton("Cancel", delegate
                            {
                                builder.Dispose();
                            });

                            builder.Show();
                        }
                    }

                  

                };
        }


        public bool Empty()
        {
            int isSelected = radioGroup.CheckedRadioButtonId;
            int acct = pri.CheckedRadioButtonId;

            if (isSelected == -1)
            {
                Toast.MakeText(this, "Choose Bank", ToastLength.Short).Show();
                return true;
            }

            if (acct == -1)
            {
                Toast.MakeText(this, "Choose your Primary Account", ToastLength.Short).Show();
                return true;
            }

            if (amountToSend.Text.ToString() == String.Empty)
            {
                amountToSend.Error = "Enter Amount to Transfer";
                amountToSend.RequestFocus();
                return true;
            }

            if (acctNoInput.Text.ToString() == String.Empty)
            {
                acctNoInput.Error = "Enter Account Number";
                acctNoInput.RequestFocus();
                return true;
            }

            if(acctNoInput.Text.ToString().Trim().Length < 10 || acctNoInput.Text.ToString().Trim().Length > 10)
            {
                acctNoInput.Error = "Account Number Should be 10 digit";
                acctNoInput.RequestFocus();
                return true;
            }
            return false;
        }

        private async void validateActivation(string toAccount, decimal amount, string transactionPin)
        {
            var user = MemoryManager.Instance(this).getUser("LoginResponseModel");
            if (user != null)
            {
                SendMoneyModel sendMoney = new SendMoneyModel() { amount = amount, toAccount = toAccount, transactionPin = transactionPin};
                string send = JsonConvert.SerializeObject(sendMoney);
                var mResult = await NetworkUtil.PostGeneralAsycTip("Transactions/SendMoney", send, "FromAccount", user.acctNumber);

                if (!string.IsNullOrEmpty(mResult))
                {
                    var mm = JsonConvert.DeserializeObject<SendMoneyResponseModel>(mResult);
                    if (mm != null)
                    {
                        MemoryManager.Instance(this).setSendMoney("SendMoneyResponseModel", mm);   //added to save transaction status
                        Toast.MakeText(this, "Transferred Successfully", ToastLength.Long).Show();
                                 Finish();
                        //string send1 = JsonConvert.SerializeObject(mm);

                    }
                    else
                    {
                        Toast.MakeText(this, "Insufficient Fund", ToastLength.Short).Show();
                   }

                }
            }
        }
    }
}
