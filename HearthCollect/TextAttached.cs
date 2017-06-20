using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace HearthCollect
{
    static class TextAttached
    {
        // Using a DependencyProperty as the backing store for RichText.  This enables animation, styling, binding, etc...
        // RichText parses the text into Inlines for bold etc.
        public static string GetRichText(DependencyObject obj)
        {
            return (string)obj.GetValue(RichTextProperty);
        }

        public static void SetRichText(DependencyObject obj, string value)
        {
            obj.SetValue(RichTextProperty, value);
        }

        public static readonly DependencyProperty RichTextProperty =
            DependencyProperty.RegisterAttached("RichText", typeof(string), typeof(TextAttached), new FrameworkPropertyMetadata(string.Empty, richTextChanged));

        static void richTextChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            TextBlock textBlock = o as TextBlock;
            if (o == null)
            {
                throw new InvalidOperationException("Can only apply TextAttached.RichText to a TextBlock");
            }

            text = (string)e.NewValue ?? string.Empty;
            textBlock.Text = string.Empty;
            pos = 0;
            Span span = new Span();
            textBlock.Inlines.Add(span);
            Parse(span);
        }

        static string text;
        static int pos;

        static void Parse(Span span)
        {
            string tagString = string.Empty;
            int open;
            int close;
            // Iterate siblings
            while (pos < text.Length)
            {
                open = text.IndexOf('<', pos);
                // Can either be an open tag, close tag or not found
                if (open >= pos)
                {
                    close = text.IndexOf('>', open + 1);
                    if (close > open)
                        tagString = text.Substring(open, close - open + 1);
                    // If no matching close bracket found, write up to open bracket and finish
                }
                else
                {
                    // No more tags, write out remaining text and finish
                    tagString = string.Empty;
                    open = text.Length;
                }

                if (open > pos)
                {
                    span.Inlines.Add(text.Substring(pos, open - pos));
                }

                if (tagString == string.Empty)
                {
                    pos = text.Length;
                    // Finished
                }
                else
                if (tagString[1] != '/')
                {
                    // Recurse with new tag
                    pos = open + tagString.Length;
                    Span spanNew = new Span();
                    switch (tagString[1])
                    {
                        case 'b':
                            spanNew.FontWeight = FontWeights.Bold;
                            break;
                        case 'i':
                            spanNew.FontStyle = FontStyles.Italic;
                            break;
                    }
                    span.Inlines.Add(spanNew);
                    Parse(spanNew);
                    // pos will be positioned after a close tag or at EOF, go on to next sibling
                }
                else
                if (tagString[1] == '/')
                {
                    pos = open + tagString.Length;
                    // Return to previous level
                    return;
                }
                else
                    throw new InvalidOperationException("Shouldn't get here.");
            }
        }
    }
}
