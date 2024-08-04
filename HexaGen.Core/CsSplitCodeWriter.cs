﻿namespace HexaGen
{
    using HexaGen.Core;
    using System.Text;

    public sealed class CsSplitCodeWriter : ICodeWriter, IDisposable
    {
        private bool _shouldIndent = true;
        private readonly string[] _indentStrings;
        private string _indentString = "";

        private StreamWriter _writer;
        private readonly StringBuilder sb = new();
        private bool capture = true;
        private readonly HeaderInjectorCallback? injectorCallback;
        private readonly int baseIndentLevel;
        private int linesWritten;
        private int splitCount;
        private int indentLevel;
        private string path;
        private string? name;
        private string? extension;

        public string FileName { get; }

        public string[] Namespaces { get; }

        public int IndentLevel => indentLevel;

        public int SplitThreshold { get; set; } = 5000;

        public int SplitCount => splitCount;

        public CsSplitCodeWriter(string fileName, string @namespace, IList<string> usings, HeaderInjectorCallback? injectorCallback, int baseIndentLevel = 2)
        {
            this.baseIndentLevel = baseIndentLevel;
            _indentStrings = new string[10];
            for (int i = 0; i < _indentStrings.Length; i++)
            {
                _indentStrings[i] = new string('\t', i);
            }

            path = Path.GetDirectoryName(fileName) ?? string.Empty;
            name = Path.GetFileNameWithoutExtension(fileName);
            extension = Path.GetExtension(fileName);
            _writer = File.CreateText(Path.Combine(path, $"{name}.{splitCount:D3}{extension}"));

            FileName = fileName;
            this.injectorCallback = injectorCallback;
            Namespaces = usings.ToArray();

            WriteLineInternal("// ------------------------------------------------------------------------------");
            WriteLineInternal("// <auto-generated>");
            WriteLineInternal("//     This code was generated by a tool.");
            WriteLineInternal("//");
            WriteLineInternal("//     Changes to this file may cause incorrect behavior and will be lost if");
            WriteLineInternal("//     the code is regenerated.");
            WriteLineInternal("// </auto-generated>");
            WriteLineInternal("// ------------------------------------------------------------------------------");
            WriteLineInternal();

            foreach (var ns in Namespaces)
            {
                WriteLineInternal($"using {ns};");
            }

        
            if (Namespaces.Length > 0)
            {
                WriteLineInternal();
            }

            injectorCallback?.Invoke(this, sb);

            _writer.WriteLine(sb);

            BeginBlock($"namespace {@namespace}");
        }

        private void WriteLineInternal(string line)
        {
           
            sb.AppendLine(line);
        }

        private void WriteLineInternal()
        {
          
            sb.AppendLine();
        }

        public void Dispose()
        {
            EndBlock();
            _writer.Dispose();
        }

        public void Write(char chr)
        {
            WriteIndented(chr);
            if (chr == '\n')
            {
                linesWritten++;
            }
        }

        public void Write(string @string)
        {
            WriteIndented(@string);
            linesWritten += @string.Count(c => c == '\n');
        }

        public void WriteLine()
        {
            capture &= indentLevel < baseIndentLevel;
            if (capture)
            {
                sb.AppendLine();
            }
            _writer.WriteLine();
            linesWritten++;
            _shouldIndent = true;
        }

        public void WriteLine(string @string)
        {
            capture &= indentLevel < baseIndentLevel;
            WriteIndented(@string);
            if (capture)
            {
                sb.AppendLine();
            }
            _writer.WriteLine();
            linesWritten++;
            _shouldIndent = true;
        }

        public void WriteLines(string? @string, bool newLineAtEnd = false)
        {
            if (@string == null)
                return;

            capture &= indentLevel < baseIndentLevel;

            if (@string.Contains('\n'))
            {
                var lines = @string.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lines.Length; i++)
                {
                    WriteIndented(lines[i]);
                    if (capture)
                    {
                        sb.AppendLine();
                    }
                    linesWritten++;
                }
            }
            _shouldIndent = true;
        }

        public void WriteLines(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                WriteLine(line);
            }
        }

        public void BeginBlock(string content)
        {
            WriteLine(content);
            WriteLine("{");
            Indent(1);
        }

        public void EndBlock()
        {
            Dedent(1);
            WriteLine("}");

            if (linesWritten >= SplitThreshold && indentLevel == baseIndentLevel)
            {
                while (indentLevel > 0)
                {
                    EndBlock();
                }
                _writer.Dispose();
                splitCount++;
                linesWritten = 0;
                indentLevel = 0;
                _shouldIndent = true;
                _writer = File.CreateText(Path.Combine(path, $"{name}.{splitCount:D3}{extension}"));
                _writer.Write(sb);
                _writer.Flush();
                Indent(baseIndentLevel);
            }
        }

        public IDisposable PushBlock(string marker = "{") => new CodeBlock(this, marker);

        public void Indent(int count = 1)
        {
            indentLevel += count;

            if (indentLevel < _indentStrings.Length)
            {
                _indentString = _indentStrings[indentLevel];
            }
            else
            {
                _indentString = new string('\t', indentLevel);
            }
        }

        public void Dedent(int count = 1)
        {
            if (count > indentLevel)
                throw new ArgumentException("count out of range.", nameof(count));

            indentLevel -= count;
            if (indentLevel < _indentStrings.Length)
            {
                _indentString = _indentStrings[indentLevel];
            }
            else
            {
                _indentString = new string('\t', indentLevel);
            }
        }

        private void WriteIndented(char chr)
        {
            capture &= indentLevel < baseIndentLevel;
            if (_shouldIndent)
            {
                if (capture)
                {
                    sb.Append(_indentString);
                }
                _writer.Write(_indentString);
                _shouldIndent = false;
            }

            if (capture)
            {
                sb.Append(chr);
            }
            _writer.Write(chr);
        }

        private void WriteIndented(string @string)
        {
            capture &= indentLevel < baseIndentLevel;
            if (_shouldIndent)
            {
                if (capture)
                {
                    sb.Append(_indentString);
                }
                _writer.Write(_indentString);
                _shouldIndent = false;
            }

            if (capture)
            {
                sb.Append(@string);
            }
            _writer.Write(@string);
        }

        private class CodeBlock : IDisposable
        {
            private readonly ICodeWriter _writer;

            public CodeBlock(ICodeWriter writer, string content)
            {
                _writer = writer;
                _writer.BeginBlock(content);
            }

            public void Dispose()
            {
                _writer.EndBlock();
            }
        }
    }
}