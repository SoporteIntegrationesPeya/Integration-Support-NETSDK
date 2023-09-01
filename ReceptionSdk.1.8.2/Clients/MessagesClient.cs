using System;
using ReceptionSdk.Exceptions;
using ReceptionSdk.Helpers;
using ReceptionSdk.Models;
using ReceptionSdk.Http;
using System.Threading.Tasks;
using PubnubApi;

namespace ReceptionSdk.Clients
{
    public class MessagesClient
    {
        private int? partnerId = 0;

        private ApiConnection Connection { get; set; }

        public Pubnub PubNubCredentials(int? partnerId = 0)
        {
            String authKey = "";
            if (partnerId != 0)
            {
                authKey = partnerId.ToString();
                this.partnerId = partnerId;
            }
            else {
                authKey = Connection.Credentials.ClientId.ToString();
            }

            PNConfiguration pnConfiguration = new PNConfiguration
            {
                SubscribeKey = Connection.MessagesSusbcriberKey(),
                AuthKey = Base64Encode(authKey)

            };
            return new Pubnub(pnConfiguration);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public MessagesClient(ApiConnection connection)
        {
            Ensure.ArgumentNotNull(connection, "Connection");

            Connection = connection;
        }

        /// <summary>
        /// Delegate to be executed when a new message received
        /// </summary>
        /// <param name="message">the received message to be processed</param>
        /// <returns><c>true</c> if the message processed correctly</returns>
        public delegate bool OnReceivedMessage(Message message);

        /// <summary>
        /// Delegate to be executed when a error occurred receving a new message
        /// </summary>
        /// <param name="ex">the exception thrown</param>
        public delegate void OnError(ApiException ex);

        /// <summary>
        /// Listen for new messages. 
        /// </summary>
        /// <param name="onSuccess">delegate to be called when a new message is received</param>
        /// <param name="onError">delegate to be called when a error has occurred</param>
        public void GetAll( OnReceivedMessage onSuccess, OnError onError, int? partnerId = 0)
        {
            Pubnub pubnub = PubNubCredentials(partnerId);

            LoadMessages(onSuccess, onError, pubnub);
        }

        private void LoadMessages(OnReceivedMessage onSuccess, OnError onError, Pubnub pubnub)
        {
            try
            {
                SubscribeCallbackExt generalSubscribeCallack = new SubscribeCallbackExt(
                    delegate (Pubnub pubnubObj, PNMessageResult<object> message)
                    {
                        onSuccess(Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(message.Message.ToString()));
                    },
                     delegate (Pubnub pubnubObj, PNPresenceEventResult presence) { },
                     delegate (Pubnub pubnubObj, PNStatus status) { });
                pubnub.AddListener(generalSubscribeCallack);

                if (partnerId != 0)
                {
                    pubnub.Subscribe<string>()
                       .Channels(new string[] { "restaurant:" + partnerId + ":messages" }).Execute();
                }
                else {
                    pubnub.Subscribe<string>()
                       .Channels(new string[] { "receptionSystem:" + Connection.Credentials.ClientId + ":messages" }).Execute();
                } 
            }
            catch (Exception ex)
            {
                throw new ApiException(ex, ErrorCode.INTERNAL_SERVER_ERROR);
            }

            return;
        }
    }
}
