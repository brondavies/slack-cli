using Newtonsoft.Json;
using Slack.Webhooks;
using System;
using System.Collections.Generic;

namespace slack
{
    partial class Program
    {
        const string opt_webhook_long = "--webhook";
        const string opt_channel_long = "--channel";
        const string opt_username_long = "--username";
        const string opt_iconemoji_long = "--iconemoji";
        const string opt_iconurl_long = "--iconurl";
        const string opt_attachments_long = "--attachments";
        const string opt_help_long = "--help";
        const string opt_webhook_short = "-w";
        const string opt_channel_short = "-c";
        const string opt_username_short = "-u";
        const string opt_iconemoji_short = "-e";
        const string opt_iconurl_short = "-i";
        const string opt_attachments_short = "-a";
        const string opt_help_short = "-h";

        private static void Help()
        {
            print($@"
Usage: slack [options] ""<message>""
    Options:
    {opt_webhook_short}, {opt_webhook_long}       Supply the url to post this message to. This can also be set in slack.exe.config at appSettings/Slack:Webhook
    {opt_channel_short}, {opt_channel_long}       Supply the channel to post this message to. This can also be set in slack.exe.config at appSettings/Slack:Channel
    {opt_username_short}, {opt_username_long}      Supply the username to post this message as. This can also be set in slack.exe.config at appSettings/Slack:Username
    {opt_iconemoji_short}, {opt_iconemoji_long}     Supply the emoji to post with this message. This can also be set in slack.exe.config at appSettings/Slack:IconEmoji
    {opt_iconurl_short}, {opt_iconurl_long}       Supply the icon url to post with this message. Overrides {opt_iconemoji_long}. This can also be set in slack.exe.config at appSettings/Slack:IconUrl
    {opt_attachments_short}, {opt_attachments_long}   Supply the path to a json formatted file that describes the message attachments. Use --help attachments for more information
    {opt_help_short}, {opt_help_long}          Show this help message or extended help for other command options

    Required:
    <message>   The message to send.  You should enclose this string in quotes
    
    Examples:
    slack {opt_channel_short} #random {opt_username_short} my-bot ""Hello I'm a bot!""
    slack {opt_channel_long} #random {opt_username_long} my-bot ""Hello I'm a bot!""
".Trim());
        }

        private static void Help(string subject)
        {
            switch (subject.ToLower())
            {
                case "attachments":
                    AttachmentsHelp();
                    break;
                default:
                    Abort($"Can't help with subject \"{subject}\"");
                    break;
            }
        }

        private static void AttachmentsHelp()
        {
            print($@"The commandline option {opt_attachments_long} <filename> is used to supply optional attachments and their properties.  Here is an example of an attachments description JSON file:");
            print("");
            print(exampleAttachments);
        }

        private static string exampleAttachments
        {
            get
            {
                const string example = "http://example.com/";
                var list = new List<SlackAttachment>();
                List<SlackAction> actions = new List<SlackAction>();
                actions.Add(new SlackAction
                {
                    Name = "Action 1",
                    Options = new List<Slack.Webhooks.Action.Option>(),
                    SelectedOptions = new List<Slack.Webhooks.Action.Option>(),
                    Style = SlackActionStyle.Default,
                    Text = "Action Text",
                    Type = SlackActionType.Button,
                    Url = $"{example}action1",
                    Value = "Action 1 Value"
                });
                List<SlackField> fields = new List<SlackField>();
                fields.Add(new SlackField {
                    Title = "Field Title",
                    Value = "Field Value"
                });
                list.Add(new SlackAttachment {
                    Actions = actions,
                    AuthorIcon = $"{example}author.png",
                    AuthorLink = $"{example}author/profile",
                    AuthorName = "Author Name",
                    CallbackId = "attachment-id",
                    Color = "#7788FF",
                    Fallback = "Plain text summary",
                    Fields = fields,
                    Footer = "Footer text",
                    FooterIcon = $"{example}footericon.png",
                    ImageUrl = $"{example}attachment1.png",
                    MarkdownIn = new List<string>(),
                    Pretext = "Pre text",
                    Text = "Attachment text",
                    ThumbUrl = $"{example}attachment1-thumb.png",
                    Title = "Attachment Title",
                    TitleLink = $"{example}attachment.pdf"
                });
                return JsonConvert.SerializeObject(list, Formatting.Indented);
            }
        }
    }
}
