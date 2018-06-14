using Newtonsoft.Json;
using slack.Extensions;
using Slack.Webhooks;
using System;
using System.Collections.Generic;

namespace slack
{
    partial class Program
    {
        private static bool ParseCommandLine(string[] args)
        {
            string arg = null;
            for (int index = 0; index < args.Length; index++)
            {
                try
                {
                    arg = args[index];
                    switch (arg.ToLower())
                    {
                        case opt_webhook_long:
                        case opt_webhook_short:
                            index++;
                            webhookUrl = args[index];
                            break;

                        case opt_channel_long:
                        case opt_channel_short:
                            index++;
                            channel = args[index];
                            break;

                        case opt_username_long:
                        case opt_username_short:
                            index++;
                            username = args[index];
                            break;

                        case opt_iconemoji_long:
                        case opt_iconemoji_short:
                            index++;
                            iconemoji = args[index];
                            break;

                        case opt_iconurl_long:
                        case opt_iconurl_short:
                            index++;
                            iconUrl = args[index];
                            break;

                        case opt_attachments_long:
                        case opt_attachments_short:
                            index++;
                            attachments = args[index];
                            break;

                        case opt_help_long:
                        case opt_help_short:
                            index++;
                            if (index < args.Length)
                            {
                                Help(args[index]);
                            }
                            else
                            {
                                Help();
                            }
                            return false;

                        default:
                            message = $"{message} {arg}".TrimStart();
                            break;
                    }
                }
                catch
                {
                    return Abort($"Invalid option for {arg}");
                }
            }

            if (!webhookUrl.IsUrl())
            {
                return Abort($"Webhook is not set or invalid. Use {opt_webhook_long} <url>");
            }

            if (message.IsNull())
            {
                return Abort($"Message is not set.");
            }

            if (!iconUrl.IsNull() && !iconUrl.IsUrl())
            {
                return Abort($"IconUrl is not a valid URL. Use {opt_iconurl_long} <url>");
            }

            if (!attachments.IsNull())
            {
                if (!attachments.FileExists())
                {
                    return Abort($"Could not find the attachments file. Use {opt_attachments_long} <filename>");
                }
                else if (!IsAttachmentsFile(attachments))
                {
                    return Abort($"Invalid attachments file format. See {opt_help_long} attachments");
                }
            }
            return true;
        }

        private static bool IsAttachmentsFile(string filename)
        {
            try
            {
                JsonConvert.DeserializeObject<List<SlackAttachment>>(filename.ReadAllText());
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static bool Abort(string abortMessage)
        {
            if (!abortMessage.IsNull())
            {
                print($"{abortMessage}\n");
            }
            Help();
            Environment.ExitCode = 1;
            return false;
        }

        private static void print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
