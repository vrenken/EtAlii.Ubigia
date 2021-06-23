// ReSharper disable All

////////////////////////////////////////////////////////////////////////////////////////////////
//
// Copyright © Yaroslavov Alexander 2010
//
// Contacts:
// Phone: +7(906)827-27-51, +7(964)595-55-94
// Email: x-ronos@yandex.ru
//
/////////////////////////////////////////////////////////////////////////////////////////////////

using System;

namespace Moppet.Lapa.Parsers
{

    /// <summary>
    /// Парсер для полноценного разбора пути к файлу или папке в локальной сети или на компьютере.
    ///
    /// На конструирование парсера уходит время, поэтому не делайте так в цикле: LpFilePath.Abs().Do(...).
    /// </summary>
    public static class LpFilePath
    {
        /// <summary>
        /// Разделитель пути.
        /// </summary>
        public static readonly Func<char, bool> PathSep = (c) => c == '\\' || c == '/';

        /// <summary>
        /// Допустимый символ для имени файла, исключая точку и пробел.
        /// </summary>
        public static readonly Func<char, bool> NameChar = (c) => char.IsLetterOrDigit(c) || (" .\\*:?/%|\"<>".IndexOf(c) == -1);

        /// <summary>
        /// Корень пути.
        /// Локальный корень (LocalRoot): [Prefix]&lt;Drive&gt;[Sep].
        /// Сетевой корень (NetRoot): [Prefix]&lt;Host&gt;[Sep].
        /// Пустой корень (EmptyRoot) - это &lt;Sep&gt;
        ///
        /// Prefix - это либо "\\?\" либо "\\";
        /// Host - это имя сервера;
        /// Sep - символ-разделитель имён папок в пути;
        ///
        /// http://en.wikipedia.org/wiki/Filename
        /// </summary>
        /// <returns>parser.</returns>
        public static LpsParser Root()
        {
            var uncwPrefix = Lp.Term(@"\\?\").Id("Prefix");
            var netUnc     = (Lp.Term(@"\\?\UNC") | Lp.Term(@"\\")).TakeFirst().Id("Prefix");

            var drive = (Lp.One(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z')) + Lp.Char(':')).Id("Drive");
            var root =
            (
                (uncwPrefix.Maybe() + drive + PathSep.Maybe().Id("Sep")).Id("LocalRoot")
                |
                (netUnc + Lp.Letters().Id("Host") + PathSep.Maybe().Id("Sep")).Id("NetRoot")
                |
                (Lp.One(PathSep).Id("Sep")).Wrap("EmptyRoot")
            ).
            TakeFirst().Id("Root");
            return root;
        }


        /// <summary>
        /// Имя файла или папки. Только имя, без пути.
        /// Если возвращаемый узел имеет потомков, то однозначно потомков всего два - это имя без расширения и, собственно, расширение.
        /// Помеченые узлы:
        /// "Name" , "Ext" - имя с расширением (включает два дочерних узла "Name" и "Ext");
        /// "Name" - имя без расширения;
        ///
        /// http://en.wikipedia.org/wiki/Filename,
        /// http://msdn.microsoft.com/ru-ru/library/aa365247(VS.85).aspx (Naming Files, Paths, and Namespaces)
        /// </summary>
        public static LpsParser FileOrDirName()
        {
            // Правила:
            // 1) Имя может начинаться с точки.
            // 2) По краям запрещены пробелы. Точка в конце также не допускается.
            // 3) Если имя начинается с точки, то оно должно быть больше одного символа.
            // 4) Расширение без имени файла существовать не может. Но имя файла может быть точкой, если далее есть расширение.
            // 5) Если после точки есть пробел, то точка не относится к расширению.

            const int Start = 0;
            const int NeedNextChar = 1;
            const int NeedExt = 2;
            const int MaybeNextChar = 3;

            // Несмотря на то что конечные автоматы не приветствуются, здесь ему самое место, ибо он существенно сократил код и его понимание.
            var name_ = new LpsParser((t) =>
            {
                var state = Start;
                var iPoint = -1;
                var i = -1;
                var len = t.Length;

                while(++i < len)
                {
                    var c = t[i];
                    switch (state)
                    {
                        case Start:
                            if (c == ' ') return new LpNode(t);
                            if (c == '.') { state = NeedNextChar; break; }
                            if (NameChar(c)) { state = MaybeNextChar; break; }
                            return new LpNode(t);

                        case NeedNextChar:
                            if (c == '.') { iPoint = i; state = NeedExt; break; }
                            if (c == ' ') { iPoint = -1; break; }
                            if (NameChar(c)) { state = MaybeNextChar; break; }
                            return new LpNode(t);

                        case NeedExt:
                            if (PathSep(c) || c == '"') { return new LpNode(t); }
                            if (c == ' ') { state = NeedNextChar; iPoint = -1; break; }
                            if (c == '.') { iPoint = i; break; }
                            if (NameChar(c)) { state = MaybeNextChar; break; }
                            return new LpNode(t);

                        case MaybeNextChar:
                            if (NameChar(c)) break;
                            if (c == ' ') { state = NeedNextChar; iPoint = -1; break; }
                            if (c == '.') { state = NeedExt; iPoint = i; break; }
                            if (PathSep(c) || c == '"') { i--; len = 0; break; }
                            return new LpNode(t);
                    }
                }
                if (state == Start || state == NeedNextChar || state == NeedExt)
                    return new LpNode(t);

                if (iPoint == -1)
                    return new LpNode(t, i, "Name");

                var name_ch = new LpNode(t, iPoint, "Name");
                var ext_ch = new LpNode(t.Skip(iPoint), i - iPoint, "Ext");
                return new LpNode(t, i, "Name", name_ch, ext_ch);
            });
            return name_;
        }

        /// <summary>
        /// Парсер относительного пути.
        /// Узлы помечаются следующими идентификаторами: Relative, Sep, Name.
        /// </summary>
        public static LpsParser Relative()
        {
            var endOfName = Lp.Lookahead(c => PathSep(c) || c == '.');
            var endOfPath = Lp.Lookahead(c => c != '.' && !PathSep(c) && !NameChar(c));

            var cur       = (Lp.Char('.' ) + endOfName).Id(".");
            var parent    = (Lp.Term("..") + endOfName).Id("..");
            var fileOrDir = FileOrDirName();

            var path = Lp.MaybeTails
            (
                body      : Lp.List(  listItem: (fileOrDir | parent | cur).TakeFirst(),   delimiter: Lp.One(PathSep).Id("Sep")  ).Id("Path"),
                maybeLeft : Lp.Empty,
                maybeRight: Lp.One(PathSep).Id("Sep")
            );

            path = (path + endOfPath).Id("Relative");
            return path;
        }


        /// <summary>
        /// Парсер абсолютного пути.
        /// Помечается идентификатором Abs.
        /// </summary>
        public static LpsParser Abs()
        {
            return (Root() + Relative().Maybe()).Id("Abs");
        }


        /// <summary>
        /// Разбирает путь к файлу по кусочкам.
        /// Поддерживает UNC, абсолютный и относительный путь к локальному файлу.
        /// На конструирование парсера уходит время, поэтому не делайте так в цикле: AbsOrRelative().Do(...).
        /// </summary>
        /// <returns>parser.</returns>
        public static LpsParser AbsOrRelative()
        {
            return (Abs() | Relative()).TakeFirst();
        }
    }
}
