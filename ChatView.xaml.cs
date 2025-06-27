using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

namespace ChatBotApplication.Views
{
    public partial class ChatView : UserControl
    {
        private string userName = string.Empty;
        private bool isNameCaptured = false;
        private bool hasShownTopicMenu = false;
        private bool inEnhancedChat = false;
        private string pendingTopic = string.Empty;

        private static readonly Dictionary<string, string> sentimentResponses = new()
        {
            { "happy", "It's wonderful to see you're happy! Let's keep that positive energy while we discuss security." },
            { "good", "Great to hear you're doing well! How can I assist you with cybersecurity today?" },
            { "great", "Fantastic! It's great to hear you're doing well. What security topics interest you?" },
            { "well", "I'm glad you're doing well! What security topics can we explore together?" },
            { "fine", "Good to hear you're doing fine. How can I help with your cybersecurity needs today?" },
            { "alright", "Alright! Let's get started with your cybersecurity questions." },
            { "okay", "Okay! Ready to discuss any security topics you have in mind." },
            { "excellent", "Excellent! You're in a great mood for learning about security!" },
            { "awesome", "Awesome! Let's channel this positive energy into security awareness!" },
            { "perfect", "Perfect! Let's make your digital security just as perfect!" },
            { "bad", "I'm sorry to hear that. Maybe I can help improve your day with some security tips." },
            { "sad", "I'm sorry you're feeling down. Security can be overwhelming, but I'm here to help." },
            { "worried", "It's completely understandable to feel that way. Let me share tips to help you stay safe." },
            { "angry", "I understand your frustration. Let's work through this security issue together." },
            { "confused", "Don't worry, cybersecurity can be confusing. I'll explain things clearly." },
            { "scared", "Your concerns are valid. I'll help you take steps to protect yourself." },
            { "excited", "I love your enthusiasm for security! Let's channel that energy into learning." },
            { "curious", "That's great! Curiosity is the first step to being informed and protected." },
            { "interested", "Wonderful that you're interested! Let's dive into security topics." },
            { "eager", "I love your eagerness! Let's satisfy your security curiosity." },
            { "bored", "Maybe it's a good time to sharpen your cybersecurity skills! Want a fun tip or quick quiz?" },
            { "tired", "Sounds like you've had a long day. Cybersecurity doesn’t sleep though! Want something light, like browsing safety tips?" },
            { "anxious", "That’s okay, many people feel overwhelmed online. Maybe I can help ease your concerns. Want a quick security checklist?" },
            { "stressed", "Stress is normal, but don't worry. I can help you feel more in control of your digital life." },
            { "tell me a joke", "Why did the hacker break up with the WiFi? ...Because it just wasn’t secure anymore 😄" },
            { "who are you", "I'm CyberBot, your friendly neighborhood security sidekick 🤖. My mission? Helping you stay safe online!" },
            { "what can you do", "I can help you with passwords, phishing, VPNs, malware, hacked accounts, and more. Just ask about any of those!" },
            { "daily tip", "🗓️ Cyber Tip of the Day:\nAlways think before you click! Hover over suspicious links and check the URL before logging in." },
            { "give me motivation", "🔐 ‘Security is not a product, but a process.’ – Bruce Schneier\nStay sharp, stay safe!" }
        };

        public ChatView()
        {
            InitializeComponent();
            Loaded += ChatView_Loaded;
        }

        private void ChatView_Loaded(object sender, RoutedEventArgs e)
        {
            AddMessageToChat("CyberBot", "👋 Hello! Welcome to the Cybersecurity Assistant.", Brushes.MediumPurple);
            AddMessageToChat("CyberBot", "Before we begin, may I know your full name?", Brushes.MediumPurple);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = UserInputBox.Text.Trim();
            if (string.IsNullOrEmpty(userInput)) return;

            AddMessageToChat("You", userInput, Brushes.DeepSkyBlue);
            string botResponse = GetBotResponse(userInput);
            AddMessageToChat("CyberBot", botResponse, Brushes.MediumPurple);

            UserInputBox.Text = string.Empty;
        }

        private void AddMessageToChat(string senderName, string message, Brush background)
        {
            var border = new Border
            {
                Background = background,
                CornerRadius = new CornerRadius(8),
                Padding = new Thickness(10),
                Margin = new Thickness(5),
                MaxWidth = 600,
                Child = new TextBlock
                {
                    Text = $"{senderName}: {message}",
                    TextWrapping = TextWrapping.Wrap,
                    Foreground = Brushes.White,
                    FontSize = 14
                }
            };

            ChatList.Children.Add(border);
        }

        private string GetBotResponse(string input)
        {
            input = input.ToLower();

            if (!isNameCaptured)
            {
                userName = input;
                isNameCaptured = true;
                return $"Nice to meet you, {userName}! Here's what I can help you with today:\n\n" +
                       "1. Password security and generation\n" +
                       "2. Phishing detection and prevention\n" +
                       "3. Safe browsing practices\n" +
                       "4. General cybersecurity tips\n" +
                       "5. Emergency security actions\n" +
                       "6. Exit\n" +
                       "7. Engage in a deeper cybersecurity chat";
            }

            if (inEnhancedChat)
            {
                if (input.Contains("exit"))
                {
                    inEnhancedChat = false;
                    return "👍 Exiting enhanced chat. Let’s go back to the main menu.";
                }

                if (!string.IsNullOrEmpty(pendingTopic) && (input.Contains("yes") || input.Contains("sure") || input.Contains("please")))
                {
                    string expanded = TriggerTopicExpansion(pendingTopic);
                    pendingTopic = string.Empty;
                    return expanded + "\n\nI hope I have helped you with your request. You can now choose another topic to explore.";
                }

                return HandleEnhancedConversation(input);
            }

            return input switch
            {
                "1" => "🔐 Password Tips:\n- Use at least 12 characters mixing letters, numbers, and symbols\n- Avoid dictionary words and personal info\n- Use a password manager\n- Change passwords regularly\n- Enable 2FA for critical accounts\n\nI hope I have helped you with your password security request. You can now choose another topic to explore.",
                "2" => "🎣 Phishing Awareness:\n- Check sender address before clicking\n- Hover over links before opening them\n- Avoid urgent requests or too-good-to-be-true offers\n- Never provide personal info via email\n- Use anti-phishing filters\n\nI hope I have helped you with your phishing awareness request. You can now choose another topic to explore.",
                "3" => "🌐 Safe Browsing Tips:\n- Use secure HTTPS connections\n- Update browser and extensions regularly\n- Avoid suspicious sites and downloads\n- Disable autofill for sensitive fields\n- Use a VPN on public Wi-Fi\n\nI hope I have helped you with your safe browsing request. You can now choose another topic to explore.",
                "4" => "💡 Cybersecurity Essentials:\n- Unique password for each site\n- 2FA everywhere\n- Regular updates\n- Backups using 3-2-1 rule\n- Beware of social engineering\n\nI hope I have helped you with your cybersecurity essentials request. You can now choose another topic to explore.",
                "5" => "🆘 Emergency Protocol:\n1. Disconnect from the internet\n2. Change passwords from a secure device\n3. Contact your IT or security team\n4. Monitor accounts for suspicious activity\n5. Consider freezing your credit report\n\nI hope I have helped you with your emergency response request. You can now choose another topic to explore.",
                "6" or "exit" => GetFarewellMessage(),
                "7" => StartEnhancedChat(),
                _ => "❓ I didn't understand that. Please choose a number from 1 to 7 or type 'exit'."
            };
        }

        private string GetFarewellMessage()
        {
            string[] farewells = {
                "Remember to always think before you click!",
                "Stay vigilant against online threats!",
                "Your security is worth the extra effort!",
                "Keep your digital life safe and secure!"
            };
            var rnd = new Random();
            return $"👋 Goodbye, {userName}! {farewells[rnd.Next(farewells.Length)]}";
        }

        private string StartEnhancedChat()
        {
            inEnhancedChat = true;
            return $"🤖 Let's dive deeper, {userName}! Tell me what’s on your mind, and I’ll try to help. You can say things like ‘I'm worried about phishing’ or ‘How do I stay safe online?’\n(Type 'exit' anytime to return to the menu.)";
        }

        private string HandleEnhancedConversation(string input)
        {
            foreach (var sentiment in sentimentResponses)
            {
                if (input.Contains(sentiment.Key))
                    return sentiment.Value;
            }

            if (input.Contains("phishing"))
            {
                pendingTopic = "phishing";
                return "⚠️ Phishing attacks can trick even tech-savvy users. Watch for misspelled URLs, strange attachments, or unexpected messages. Always verify before clicking. Would you like tips to identify them better?";
            }
            else if (input.Contains("password"))
            {
                pendingTopic = "password";
                return "🔐 Passwords are your first defense. Use passphrases like 'PurpleGiraffe$Dance2025'. Never reuse passwords across accounts. Want a list of best practices?";
            }
            else if (input.Contains("vpn"))
            {
                pendingTopic = "vpn";
                return "🌐 VPNs protect your internet traffic, especially on public Wi-Fi. Choose one with a no-logs policy and fast servers. Need help picking one?";
            }
            else if (input.Contains("malware"))
            {
                pendingTopic = "malware";
                return "🦠 Malware can enter through email, downloads, or ads. Run antivirus tools weekly and avoid shady links. Want a checklist to stay protected?";
            }
            else if (input.Contains("hacked"))
            {
                pendingTopic = "hacked";
                return "🚨 If you think you've been hacked, change your passwords immediately, enable 2FA, and scan your device. Do you need recovery steps?";
            }
            else if (input.Contains("social media"))
            {
                pendingTopic = "social media";
                return "📱 Social media can leak personal data. Use privacy settings, avoid location tags, and don’t overshare. Want a guide to secure your accounts?";
            }
            else if (input.Contains("wifi") || input.Contains("network"))
            {
                pendingTopic = "wifi";
                return "📡 Home networks must be secured. Change default router passwords, use WPA3 encryption, and set a guest network. Need full setup steps?";
            }
            else if (input.Contains("how are you"))
            {
                return "🤖 I'm functioning securely and ready to assist! What's on your mind today?";
            }

            return "That's interesting. Can you tell me more so I can assist better? (Remember you can type 'exit' to return to the menu.)";
        }

        private string TriggerTopicExpansion(string topic)
        {
            return topic switch
            {
                "phishing" => "🎯 Phishing Tips:\n- Don't trust urgent emails asking for sensitive info\n- Hover over links before clicking\n- Verify the sender address\n- Look for poor grammar or logos\n- Use email filters and antivirus tools",
                "password" => "💡 Password Best Practices:\n- Use a passphrase with 12+ characters\n- Never reuse the same password\n- Use a password manager\n- Enable 2FA\n- Change passwords regularly",
                "vpn" => "🛡️ VPN Safety Tips:\n- Choose trusted providers (no-logs policy)\n- Use when on public WiFi\n- Avoid free VPNs\n- Check for DNS/IP leak protection\n- Enable kill switch option",
                "malware" => "🛠️ Malware Prevention:\n- Run antivirus scans weekly\n- Avoid opening unknown attachments\n- Don't install software from sketchy sites\n- Keep OS and apps updated\n- Back up files regularly",
                "hacked" => "🚨 Hacked Response Guide:\n- Change passwords immediately\n- Enable 2FA everywhere\n- Scan for malware\n- Review recent logins on accounts\n- Notify affected services",
                "social media" => "📲 Social Media Protection:\n- Set profiles to private\n- Avoid oversharing personal info\n- Use strong, unique passwords\n- Disable location tags\n- Regularly review privacy settings",
                "wifi" => "📶 WiFi Security Steps:\n- Rename default network name\n- Set strong WiFi password\n- Enable WPA3 encryption\n- Create guest network\n- Update router firmware",
                _ => "📘 General Tip: Always stay informed and use common sense online."
            };
        }
    }
}
