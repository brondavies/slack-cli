# slack-cli
A .Net command line interface for sending slack messages via [Slack Incoming Webhooks](https://api.slack.com/incoming-webhooks)

## Building
Just open slack.sln and build.  The build combines all dependencies into the executable using Fody + Costura.

## Configuration
First [set up your incoming webhook](https://api.slack.com/incoming-webhooks#getting-started) for the app to use.  Usually users will set the webhook url in slack.exe.config at `appSettings/Slack:Webhook` and that is enough to send messages using `slack "Hello World!"`

## Usage
`slack [options] "<message>"`
```
    Options:
    -w, --webhook       Supply the url to post this message to. This can also be set in slack.exe.config at appSettings/Slack:Webhook
    -c, --channel       Supply the channel to post this message to. This can also be set in slack.exe.config at appSettings/Slack:Channel
    -u, --username      Supply the username to post this message as. This can also be set in slack.exe.config at appSettings/Slack:Username
    -e, --iconemoji     Supply the emoji to post with this message. This can also be set in slack.exe.config at appSettings/Slack:IconEmoji
    -i, --iconurl       Supply the icon url to post with this message. Overrides --iconemoji. This can also be set in slack.exe.config at appSettings/Slack:IconUrl
    -a, --attachments   Supply the path to a json formatted file that describes the message attachments. Use --help attachments for more information
    -h, --help          Show this help message or extended help for other command options

    Required:
    <message>   The message to send.  You should enclose this string in quotes
```
## Examples
This command:

`slack -c #random -u my-bot "Hello I'm a bot!"`

is the same as:

`slack --channel #random --username my-bot "Hello I'm a bot!"`

