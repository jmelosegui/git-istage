using System;

namespace GitIStage
{
    internal abstract class ViewLineRenderer
    {
        private readonly char[] _blankRow = Whitespace.GetSpaces(Console.WindowWidth);

        public abstract void Render(View view, int lineIndex);

        protected void RenderLine(View view, int lineIndex, string line, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            var textStart = Math.Min(view.LeftChar, line.Length);
            var textLength = Math.Max(Math.Min(view.Width, line.Length - view.LeftChar), 0);
            var text = textLength < 0 ? string.Empty : line.ToVisual(view.VisibleWhitespace).Substring(textStart, textLength);

            var visualLine = lineIndex - view.TopLine + view.Top;

            Vt100.SetCursorPosition(view.Left, visualLine);
            Vt100.SetForegroundColor(foregroundColor);
            Vt100.SetBackgroundColor(backgroundColor);
            Console.Write(text);

            var remaining = view.Width - textLength;
            if (remaining > 0)
                Console.Write(_blankRow, 0, remaining);
        }
    }
}