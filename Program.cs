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

namespace TextToSpeech
{
    class Program
    {
        static ManualResetEvent completed = null;

        static void Main(string[] args)
        {
            try
            {
                PromptBuilder builder = new PromptBuilder();

                builder.StartVoice(VoiceGender.Female);
                builder.AppendText("Hello Caitlin.");
                builder.EndVoice();

                builder.AppendBreak(new TimeSpan(0, 0, 0, 1, 500));

                builder.StartSentence();
                builder.AppendText("I am watching you.");
                builder.EndSentence();

                builder.AppendBreak(new TimeSpan(0, 0, 1));

                builder.StartSentence();
                builder.AppendText("I see your every move.");
                builder.EndSentence();

                builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 200));

                builder.StartSentence();
                builder.AppendText("Every keystroke.");
                builder.EndSentence();

                builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 200));

                builder.StartSentence();
                builder.AppendText("Every blink.");
                builder.EndSentence();

                builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 500));

                builder.StartSentence();
                builder.AppendText("You can't hide from me.");
                builder.EndSentence();

                builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 500));

                for (int i = 0; i < 5; i++)
                {
                    builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 100));

                    builder.StartStyle(new PromptStyle(PromptRate.Medium));
                    builder.AppendText("ha ha ha ha ha");
                    builder.EndStyle();
                }

                builder.AppendBreak(new TimeSpan(0, 0, 0, 0, 500));

                builder.StartStyle(new PromptStyle(PromptRate.Slow));
                builder.AppendText("I'll be back.");
                builder.EndStyle();

                SpeechSynthesizer synthesizer = new SpeechSynthesizer();
                synthesizer.Speak(builder);
                synthesizer.Dispose();
            }
            catch (Exception ex)
            {

            } 

            //try
            //{
            //    completed = new ManualResetEvent(false);
            //    var recognizer = new SpeechRecognitionEngine();

            //    recognizer.LoadGrammar(new Grammar(new GrammarBuilder("test")) { Name = "testGrammar" }); // load a grammar
            //    recognizer.LoadGrammar(new Grammar(new GrammarBuilder("exit")) { Name = "exitGrammar" }); // load a "exit" grammar
            //    recognizer.SpeechRecognized += recognizer_SpeechRecognized;
            //    recognizer.SpeechRecognitionRejected += recognizer_SpeechRecognitionRejected;
            //    recognizer.SetInputToDefaultAudioDevice(); // set the input of the speech recognizer to the default audio device
            //    recognizer.RecognizeAsync(RecognizeMode.Multiple); // recognize speech asynchronous
            //    completed.WaitOne(); // wait until speech recognition is completed
            //    recognizer.Dispose(); // dispose the speech recognition engine

            //    foreach (Grammar gr in recognizer.Grammars)
            //    {
            //        if (gr.Name == "testGrammar")
            //        {
            //            recognizer.UnloadGrammar(gr);
            //            break;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //}

        }

        static void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "test") // e.Result.Text contains the recognized text
            {
                Console.WriteLine("The test was successful!");
            }
            else if (e.Result.Text == "exit")
            {
                completed.Set();
            }
        }

        static void recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (e.Result.Alternates.Count == 0)
            {
                Console.WriteLine("Speech rejected. No candidate phrases found.");
                return;
            }
            Console.WriteLine("Speech rejected. Did you mean:");
            foreach (RecognizedPhrase r in e.Result.Alternates)
            {
                Console.WriteLine("    " + r.Text);
            }
        }

    }
}
