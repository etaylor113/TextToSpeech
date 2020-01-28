using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace TextToSpeech
{
    // page ref: https://www.codeproject.com/Articles/483347/Speech-recognition-speech-to-text-text-to-speech-a
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    // Clear window
                    Console.Clear();

                    //Console.WriteLine($"Current Version: {}");

                    // Get text to say
                    Console.Write("\n Enter text to say: ");
                    string inputText = Console.ReadLine();

                    // Close app if they type exit
                    if (inputText.ToLower() == "exit")
                        Exit();
                    else if (inputText.ToLower() == "open notepad")
                    {
                        OpenNotePad();
                        continue;
                    }

                    Speak(inputText);
                }
            }
            catch (Exception ex)
            {

            } 
        }

        private static void Exit()
        {
            PromptBuilder builder = new PromptBuilder();

            builder.StartStyle(new PromptStyle(PromptRate.NotSet));
            builder.AppendText("bye bye");
            builder.EndStyle();

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(builder);
            synthesizer.Dispose();

            Environment.Exit(0);
        }

        private static void Speak(string textToSay)
        {
            Task.Run(() => SpeakAsync(textToSay));
        }

        private static async Task SpeakAsync(string textToSay)
        {
            // Say the text they typed
            PromptBuilder builder = new PromptBuilder();

            builder.StartSentence();
            builder.AppendText(textToSay);
            builder.EndSentence();

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Speak(builder);
            synthesizer.Dispose();
        }

        private static void OpenNotePad()
        {
            Speak("opening notepad");
            Process.Start("notepad.exe");
        }
    }
}
