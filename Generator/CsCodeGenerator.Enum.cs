namespace Generator
{
    using CppAst;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static partial class CsCodeGenerator
    {
        public static void GenerateEnums(CppCompilation compilation, string outputPath)
        {
            using var writer = new CodeWriter(Path.Combine(outputPath, "Enumerations.cs"), "System");
            var createdEnums = new Dictionary<string, string>();

            foreach (CppEnum cppEnum in compilation.Enums)
            {
                string csName = GetCsCleanName(cppEnum.Name);
                string enumNamePrefix = GetEnumNamePrefix(cppEnum.Name);
                if (csName.EndsWith("_"))
                {
                    csName = csName.Remove(csName.Length - 1);
                }

                // Remove extension suffix from enum item values
                string extensionPrefix = "";

                createdEnums.Add(csName, cppEnum.Name);

                bool noneAdded = false;
                WriteCsSummary(cppEnum.Comment, writer);
                using (writer.PushBlock($"public enum {csName}"))
                {
                    foreach (var enumItem in cppEnum.Items)
                    {
                        var enumItemName = GetEnumItemName(cppEnum, enumItem.Name, enumNamePrefix);

                        if (!string.IsNullOrEmpty(extensionPrefix) && enumItemName.EndsWith(extensionPrefix))
                        {
                            enumItemName = enumItemName.Remove(enumItemName.Length - extensionPrefix.Length);
                        }

                        if (enumItemName == "None" && noneAdded)
                        {
                            continue;
                        }

                        var commentWritten = WriteCsSummary(enumItem.Comment, writer);
                        if (enumItem.ValueExpression is CppRawExpression rawExpression)
                        {
                            string enumValueName = GetEnumItemName(cppEnum, rawExpression.Text, enumNamePrefix);

                            if (!string.IsNullOrEmpty(extensionPrefix) && enumValueName.EndsWith(extensionPrefix))
                            {
                                enumValueName = enumValueName.Remove(enumValueName.Length - extensionPrefix.Length);

                                if (enumItemName == enumValueName)
                                    continue;
                            }

                            if (rawExpression.Kind == CppExpressionKind.Unexposed)
                            {
                                writer.WriteLine($"{enumItemName} = unchecked((int){enumValueName.Replace("_", "")}),");
                            }
                            else
                            {
                                writer.WriteLine($"{enumItemName} = {enumValueName},");
                            }
                        }
                        else
                        {
                            writer.WriteLine($"{enumItemName} = unchecked({enumItem.Value}),");
                        }

                        if (commentWritten)
                            writer.WriteLine();
                    }
                }

                writer.WriteLine();
            }
        }

        private static string GetEnumItemName(CppEnum @enum, string cppEnumItemName, string enumNamePrefix)
        {
            string enumItemName = GetPrettyEnumName(cppEnumItemName, enumNamePrefix);

            return enumItemName;
        }

        private static string NormalizeEnumValue(string value)
        {
            if (value == "(~0U)")
            {
                return "~0u";
            }

            if (value == "(~0ULL)")
            {
                return "~0ul";
            }

            if (value == "(~0U-1)")
            {
                return "~0u - 1";
            }

            if (value == "(~0U-2)")
            {
                return "~0u - 2";
            }

            if (value == "(~0U-3)")
            {
                return "~0u - 3";
            }

            return value.Replace("ULL", "UL");
        }

        public static string GetEnumNamePrefix(string typeName)
        {
            if (CsCodeGeneratorSettings.Default.KnownEnumPrefixes.TryGetValue(typeName, out string? knownValue))
            {
                return knownValue;
            }
            if (typeName.Contains('_'))
            {
                return typeName;
            }
            List<string> parts = new(4);
            int chunkStart = 0;
            for (int i = 0; i < typeName.Length; i++)
            {
                if (char.IsUpper(typeName[i]))
                {
                    if (chunkStart != i)
                    {
                        parts.Add(typeName.Substring(chunkStart, i - chunkStart));
                    }

                    chunkStart = i;
                    if (i == typeName.Length - 1)
                    {
                        parts.Add(typeName.Substring(i, 1));
                    }
                }
                else if (i == typeName.Length - 1)
                {
                    parts.Add(typeName.Substring(chunkStart, typeName.Length - chunkStart));
                }
            }

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i] == "Flag" ||
                    parts[i] == "Flags" ||
                    (parts[i] == "K" && (i + 2) < parts.Count && parts[i + 1] == "H" && parts[i + 2] == "R") ||
                    (parts[i] == "A" && (i + 2) < parts.Count && parts[i + 1] == "M" && parts[i + 2] == "D") ||
                    (parts[i] == "E" && (i + 2) < parts.Count && parts[i + 1] == "X" && parts[i + 2] == "T") ||
                    (parts[i] == "Type" && (i + 2) < parts.Count && parts[i + 1] == "N" && parts[i + 2] == "V") ||
                    (parts[i] == "Type" && (i + 3) < parts.Count && parts[i + 1] == "N" && parts[i + 2] == "V" && parts[i + 3] == "X") ||
                    (parts[i] == "Scope" && (i + 2) < parts.Count && parts[i + 1] == "N" && parts[i + 2] == "V") ||
                    (parts[i] == "Mode" && (i + 2) < parts.Count && parts[i + 1] == "N" && parts[i + 2] == "V") ||
                    (parts[i] == "Mode" && (i + 5) < parts.Count && parts[i + 1] == "I" && parts[i + 2] == "N" && parts[i + 3] == "T" && parts[i + 4] == "E" && parts[i + 5] == "L") ||
                    (parts[i] == "Type" && (i + 5) < parts.Count && parts[i + 1] == "I" && parts[i + 2] == "N" && parts[i + 3] == "T" && parts[i + 4] == "E" && parts[i + 5] == "L")
                    )
                {
                    parts = new List<string>(parts.Take(i));
                    break;
                }
            }

            return string.Join("_", parts.Select(s => s.ToUpper()));
        }

        private static string GetPrettyEnumName(string value, string enumPrefix)
        {
            if (CsCodeGeneratorSettings.Default.KnownEnumValueNames.TryGetValue(value, out string? knownName))
            {
                return knownName;
            }

            if (value.StartsWith("0x"))
                return value;

            string[] parts = value.Split('_', StringSplitOptions.RemoveEmptyEntries).SelectMany(x => x.SplitByCase()).ToArray();
            string[] prefixParts = enumPrefix.Split('_', StringSplitOptions.RemoveEmptyEntries);

            bool capture = false;
            var sb = new StringBuilder();
            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];
                if (CsCodeGeneratorSettings.Default.IgnoredParts.Contains(part, StringComparer.InvariantCultureIgnoreCase) || (prefixParts.Contains(part, StringComparer.InvariantCultureIgnoreCase) && !capture))
                {
                    continue;
                }

                part = part.ToLower();

                sb.Append(char.ToUpper(part[0]));
                sb.Append(part[1..]);
                capture = true;
            }

            if (sb.Length == 0)
                sb.Append(value);

            string prettyName = sb.ToString();
            return (char.IsNumber(prettyName[0])) ? prefixParts[^1].ToCamelCase() + prettyName : prettyName;
        }

        public static unsafe string ToCamelCase(this string str)
        {
            string output = new('\0', str.Length);
            fixed (char* p = output)
            {
                p[0] = char.ToUpper(str[0]);
                for (int i = 1; i < str.Length; i++)
                {
                    p[i] = char.ToLower(str[i]);
                }
            }
            return output;
        }

        public static string[] SplitByCase(this string s)
        {
            var ʀ = new List<string>();
            var ᴛ = new StringBuilder();
            var previous = SplitByCaseModes.None;
            foreach (var ɪ in s)
            {
                SplitByCaseModes mode_ɪ;
                if (string.IsNullOrWhiteSpace(ɪ.ToString()))
                {
                    mode_ɪ = SplitByCaseModes.WhiteSpace;
                }
                else if ("0123456789".Contains(ɪ))
                {
                    mode_ɪ = SplitByCaseModes.Digit;
                }
                else if (ɪ == ɪ.ToString().ToUpper()[0])
                {
                    mode_ɪ = SplitByCaseModes.UpperCase;
                }
                else
                {
                    mode_ɪ = SplitByCaseModes.LowerCase;
                }
                if ((previous == SplitByCaseModes.None) || (previous == mode_ɪ))
                {
                    ᴛ.Append(ɪ);
                }
                else if ((previous == SplitByCaseModes.UpperCase) && (mode_ɪ == SplitByCaseModes.LowerCase))
                {
                    if (ᴛ.Length > 1)
                    {
                        ʀ.Add(ᴛ.ToString().Substring(0, ᴛ.Length - 1));
                        ᴛ.Remove(0, ᴛ.Length - 1);
                    }
                    ᴛ.Append(ɪ);
                }
                else
                {
                    ʀ.Add(ᴛ.ToString());
                    ᴛ.Clear();
                    ᴛ.Append(ɪ);
                }
                previous = mode_ɪ;
            }
            if (ᴛ.Length != 0) ʀ.Add(ᴛ.ToString());
            return ʀ.ToArray();
        }

        private enum SplitByCaseModes
        { None, WhiteSpace, Digit, UpperCase, LowerCase }
    }
}