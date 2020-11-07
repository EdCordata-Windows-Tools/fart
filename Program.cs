using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

namespace fart {

    public struct Options {
        public bool   preview;
        public bool   output_version;
        public bool   output_help;
        public string input;
        public string output;
        public string find;
        public string replace;
    }

    class Program {


        static string GetVersion() {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }


        static void OutputVersion() {
            Console.WriteLine($"FART version {GetVersion()}");
            Exit();
        }


        static void OutputHelp() {
            Console.WriteLine("");
            Console.WriteLine("FART - Find And Replace Text v" + GetVersion());
            Console.WriteLine("       by EdCordata - https://GitHub.com/EdCordata-Windows-Tools/fart");
            Console.WriteLine("");
            Console.WriteLine(" Usage: fart [options]");
            Console.WriteLine("");
            Console.WriteLine(" Options:");
            Console.WriteLine("  -h, -help          Display this window");
            Console.WriteLine("  -v, -version       Output version");
            Console.WriteLine("  -p, -preview       Preview changes without actually changing files");
            Console.WriteLine("  -i, -input         Input file path");
            Console.WriteLine("  -o, -output        Output file path");
            Console.WriteLine("  -f, -find          Text to find");
            Console.WriteLine("  -r, -replace       Text to replace");
            Exit();
        }


        static void Exit() {
            Environment.Exit(-1);
        }


        static void ExitWithError(string message) {
            Console.WriteLine(message);
            Environment.Exit(-1);
        }


        static Options ParseArgs(string[] args) {
            Options options = new Options();
            string next     = null;

            options.preview        = false;
            options.output_help    = false;
            options.output_version = false;

            foreach (string arg in args)  {
                switch (next) {
                    case "input":
                        options.input = arg;
                        next = null;
                        break;

                    case "output":
                        options.output = arg;
                        next = null;
                        break;

                    case "find_txt":
                        options.find = arg;
                        next = null;
                        break;

                    case "replace_txt":
                        options.replace = arg;
                        next = null;
                        break;

                    default:
                        if (arg == "-help"    || arg == "-h") { options.output_help    = true; }
                        if (arg == "-version" || arg == "-v") { options.output_version = true; }
                        if (arg == "-preview" || arg == "-p") { options.preview        = true; }
                        if (arg == "-input"   || arg == "-i") { next = "input"; }
                        if (arg == "-output"  || arg == "-o") { next = "output"; }
                        if (arg == "-find"    || arg == "-f") { next = "find_txt"; }
                        if (arg == "-replace" || arg == "-r") { next = "replace_txt"; }
                        break;

                }
            }

            return options;
        }


        static Options ValidateOptions(Options options) {

            if (!File.Exists(options.input))  {
                ExitWithError($"FART: input file not found: '{options.input}'");
            }

            if (options.output == null) {
                options.output = options.input;
            }

            if (options.find == null) {
                ExitWithError($"FART: missing find value");
            }

            if (options.replace == null) {
                ExitWithError($"FART: missing replace value");
            }

            return options;
        }


        static void Main(string[] args) {

            if (args.Length == 0) {
                OutputHelp();
            } else {
                Options options = ParseArgs(args);

                if (options.output_version) { OutputVersion(); }
                if (options.output_help)    { OutputHelp(); }

                options        = ValidateOptions(options);
                string[] lines = System.IO.File.ReadAllLines(options.input);

                if (options.preview) {
                    ushort line_number = 1;

                    foreach (string line in lines) {
                        if (line.IndexOf(options.find) > -1) {
                            Console.WriteLine("");
                            Console.WriteLine($"Found match in line #{line_number}");
                            Console.WriteLine(line);
                        }
                        line_number += 1;
                    }

                } else {
                    List<string> new_lines = new List<string>();

                    foreach (string line in lines) {
                        new_lines.Add(line.Replace(options.find, options.replace));
                    }

                    File.WriteAllLines(options.output, new_lines);
                }
            }

        }


    }
}
