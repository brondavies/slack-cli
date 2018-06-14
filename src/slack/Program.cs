using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using slack.Extensions;
using Slack.Webhooks;

namespace slack
{
    partial class Program
    {
        static string webhookUrl = Config("Slack:Webhook");
        static string channel = Config("Slack:Channel");
        static string username = Config("Slack:Username");
        static string iconemoji = Config("Slack:IconEmoji");
        static string iconUrl = Config("Slack:IconUrl");
        static string attachments = null;
        static string message = null;

        static void Main(string[] args)
        {
            if (ParseCommandLine(args))
            {
                Execute();
            }
        }

        private static void Execute()
        {
            try
            {
                var client = new SlackClient(webhookUrl);
                var slackMessage = CreateMessage();
                if (client.Post(slackMessage))
                {
                    print("Successfully sent the message.");
                }
                else
                {
                    print($"The message could not be sent. The error returned was {client.LastResult}");
                    Environment.ExitCode = -2;
                }
            }
            catch (Exception e)
            {
                print($"{e.Message} : {e.GetType().Name}");
                print(e.StackTrace);
                Environment.ExitCode = -1;
            }
        }

        private static SlackMessage CreateMessage()
        {
            var slackMessage = new SlackMessage
            {
                Text = message,
            };
            if (!iconemoji.IsNull())
            {
                slackMessage.IconEmoji = GetEmoji(iconemoji, Emoji.Computer);
            }
            if (!username.IsNull())
            {
                slackMessage.Username = username;
            }
            if (!channel.IsNull())
            {
                slackMessage.Channel = channel;
            }
            if (!iconUrl.IsNull())
            {
                slackMessage.IconUrl = new Uri(iconUrl);
            }
            if (!attachments.IsNull())
            {
                slackMessage.Attachments = ReadAttachments(attachments);
            }
            return slackMessage;
        }

        private static List<SlackAttachment> ReadAttachments(string filename)
        {
            var list = JsonConvert.DeserializeObject<List<SlackAttachment>>(filename.ReadAllText());
            //TODO: Validate the list of attachments and their options
            return list;
        }

        private static Emoji GetEmoji(string iconemoji, Emoji defaultEmoji)
        {
            try
            {
                return (Emoji)Enum.Parse(typeof(Emoji), iconemoji);
            }
            catch
            {
                print($"Warning: {iconemoji} was not recognized as an iconemoji option. Use {opt_help_long} iconemoji for more information.");
                return defaultEmoji;
            }
        }
    }
}
