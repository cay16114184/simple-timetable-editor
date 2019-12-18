﻿using PdfSharp.Drawing;
using System;
using Unicorn.Impl.PdfSharp.Extensions;
using Unicorn.Interfaces;

namespace Unicorn.Impl.PdfSharp
{
    /// <summary>
    /// Data describing a font.
    /// </summary>
    public class FontDescriptor : IFontDescriptor
    {
        /// <summary>
        /// Construct a <see cref="FontDescriptor" /> for a font with a given family name, style and point size.
        /// </summary>
        /// <param name="name">The font family name</param>
        /// <param name="style">The font style</param>
        /// <param name="size">The font size in em-units</param>
        public FontDescriptor(string name, UniFontStyles style, double size)
        {
            Font = new XFont(name, size, style.ToXFontStyle());
            if (Font != null)
            {
                Ascent = (Font.Metrics.Ascent / (Font.Metrics.Ascent + (double)Font.Metrics.Descent)) * Font.Height;
                Descent = (Font.Metrics.Descent / (Font.Metrics.Ascent + (double)Font.Metrics.Descent)) * Font.Height;
            }
        }

        /// <summary>
        /// Construct a <see cref="FontDescriptor" /> for a regular roman-face font with a given family name and point size.
        /// </summary>
        /// <param name="name">The font family name</param>
        /// <param name="size">The font size in em-units</param>
        public FontDescriptor(string name, double size) : this(name, UniFontStyles.Regular, size)
        {

        }

        /// <summary>
        /// The underlying font.
        /// </summary>
        public XFont Font { get; }

        /// <summary>
        /// The ascent value of this font - the distance from the baseline to the top of character cells.
        /// </summary>
        public double Ascent { get; }

        /// <summary>
        /// The descent value of this font - the distance from the baseline to the bottom of character cells.
        /// </summary>
        public double Descent { get; }

        /// <summary>
        /// Returns the width of a single space character in this font, with the given context.
        /// </summary>
        /// <param name="graphicsContext"></param>
        /// <returns></returns>
        public double GetNormalSpaceWidth(IGraphicsContext graphicsContext)
        {
            if (graphicsContext is null)
            {
                throw new ArgumentNullException(nameof(graphicsContext));
            }
            return graphicsContext.MeasureString(Resources.SpaceCharacter, this).Width;
        }
    }
}
