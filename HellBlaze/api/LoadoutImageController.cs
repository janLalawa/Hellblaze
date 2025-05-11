using HellBlaze.Models;
using HellBlaze.Services;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using Svg.Skia;

namespace HellBlaze.api;

[ApiController]
[Route("api/[controller]")]
public class LoadoutImageController : ControllerBase
{
    private const int ImageWidth = 1200;
    private const int ImageHeight = 630;
    private const float TitleYPosition = 100;
    private const float LineYPosition = 140;
    private const float DiverStartY = 220;
    private const float StratagemIconSize = 90;
    private const float StratagemIconMargin = 15;
    private readonly IWebHostEnvironment _environment;
    private readonly LoadoutService _loadoutService;

    public LoadoutImageController(LoadoutService loadoutService, IWebHostEnvironment environment)
    {
        _loadoutService = loadoutService;
        _environment = environment;
    }

    [HttpGet("{loadoutCode}")]
    public IActionResult GetLoadoutImage(string loadoutCode)
    {
        var loadout = _loadoutService.GetLoadout(loadoutCode);
        if (loadout == null) return NotFound();

        using var surface = SKSurface.Create(new SKImageInfo(ImageWidth, ImageHeight));
        var canvas = surface.Canvas;
        canvas.Clear(new SKColor(20, 22, 25));

        DrawBackgroundImage(canvas);
        DrawTitle(canvas);
        //DrawSeparatorLine(canvas);
        DrawDiverLoadouts(canvas, loadout);

        using var image = surface.Snapshot();
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);

        return File(data.ToArray(), "image/png");
    }

    private static void DrawTitle(SKCanvas canvas)
    {
        var titleFont = new SKFont
        {
            Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold),
            Size = 70
        };

        using var titlePaint = new SKPaint();
        titlePaint.Color = SKColor.FromHsl(48, 100, 50);
        titlePaint.IsAntialias = true;

        using var textBlob = SKTextBlob.Create("HellBlaze", titleFont);

        canvas.DrawText(
            textBlob,
            450f,
            TitleYPosition,
            titlePaint
        );
    }

    private static void DrawSeparatorLine(SKCanvas canvas)
    {
        using var linePaint = new SKPaint();
        linePaint.Color = new SKColor(255, 212, 0);
        linePaint.StrokeWidth = 2;
        linePaint.IsAntialias = true;

        canvas.DrawLine(100, LineYPosition, ImageWidth - 100, LineYPosition, linePaint);
    }

    private void DrawDiverLoadouts(SKCanvas canvas, SharedLoadout loadout)
    {
        var diverCount = Math.Min(loadout.DiverLoadouts.Count, 4);
        const float xAdjust = 50;

        const float leftColumnX = ImageWidth * 0.25f - xAdjust;
        const float rightColumnX = ImageWidth * 0.75f - xAdjust;
        const float topRowY = DiverStartY;
        const float bottomRowY = DiverStartY + 230;

        for (var i = 0; i < diverCount; i++)
        {
            var diver = loadout.DiverLoadouts[i];

            float diverX, diverY;

            if (i % 2 == 0)
                diverX = leftColumnX;
            else
                diverX = rightColumnX;

            if (i < 2)
                diverY = topRowY;
            else
                diverY = bottomRowY;

            DrawDiverName(canvas, diver.Name, diverX, diverY);
            diverY += 50;

            DrawDiverStratagems(canvas, diver, diverX, diverY);
        }
    }


    private void DrawBackgroundImage(SKCanvas canvas)
    {
        try
        {
            var backgroundPath = Path.Combine(_environment.WebRootPath, "images", "ogbackground.jpg");

            if (!System.IO.File.Exists(backgroundPath))
            {
                Console.WriteLine($"Background image not found: {backgroundPath}");
                return;
            }

            using var bitmap = SKBitmap.Decode(backgroundPath);
            if (bitmap == null)
            {
                Console.WriteLine("Failed to decode background image");
                return;
            }

            var scale = Math.Max((float)ImageWidth / bitmap.Width, (float)ImageHeight / bitmap.Height);
            var scaledWidth = bitmap.Width * scale;
            var scaledHeight = bitmap.Height * scale;
            var left = (ImageWidth - scaledWidth) / 2;
            var top = (ImageHeight - scaledHeight) / 2;

            var destRect = new SKRect(left, top, left + scaledWidth, top + scaledHeight);

            canvas.DrawBitmap(bitmap, destRect);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error drawing background image: {ex.Message}");
        }
    }


    private static void DrawDiverName(SKCanvas canvas, string name, float x, float y)
    {
        var diverNameFont = new SKFont
        {
            Typeface = SKTypeface.FromFamilyName(
                "Arial",
                SKFontStyleWeight.Bold,
                SKFontStyleWidth.Normal,
                SKFontStyleSlant.Upright
            ),
            Size = 36
        };

        using var diverNamePaint = new SKPaint();
        diverNamePaint.Color = SKColors.White;
        diverNamePaint.IsAntialias = true;

        canvas.DrawText(
            name,
            x,
            y,
            SKTextAlign.Center,
            diverNameFont,
            diverNamePaint
        );
    }

    private void DrawDiverStratagems(SKCanvas canvas, SharedLoadout.DiverLoadout diver, float diverX, float diverY)
    {
        var xPos1 = diverX - StratagemIconSize * 1.5f - StratagemIconMargin * 1.5f;
        var xPos2 = diverX - StratagemIconSize * 0.5f - StratagemIconMargin * 0.5f;
        var xPos3 = diverX + StratagemIconSize * 0.5f + StratagemIconMargin * 0.5f;
        var xPos4 = diverX + StratagemIconSize * 1.5f + StratagemIconMargin * 1.5f;

        diverY += 20;
        DrawStratagem(canvas, diver.Kit.Stratagem1.Icon, xPos1, diverY);
        DrawStratagem(canvas, diver.Kit.Stratagem2.Icon, xPos2, diverY);
        DrawStratagem(canvas, diver.Kit.Stratagem3.Icon, xPos3, diverY);
        DrawStratagem(canvas, diver.Kit.Stratagem4.Icon, xPos4, diverY);

        var boosterX = diverX + StratagemIconSize * 2 + StratagemIconMargin * 5.5f;
        DrawBooster(canvas, diver.Kit.Booster.Icon, boosterX, diverY);
    }


    private void DrawStratagem(SKCanvas canvas, string iconPath, float x, float y)
    {
        if (string.IsNullOrEmpty(iconPath))
            return;

        try
        {
            var fullPath = Path.Combine(_environment.WebRootPath, "stratagems", iconPath);

            if (!System.IO.File.Exists(fullPath))
            {
                Console.WriteLine($"File not found: {fullPath}");
                return;
            }

            DrawStratagemBackground(canvas, x, y);
            DrawSvgIcon(canvas, fullPath, x, y, StratagemIconSize);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error drawing stratagem {iconPath}: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    private void DrawBooster(SKCanvas canvas, string iconPath, float x, float y)
    {
        if (string.IsNullOrEmpty(iconPath))
            return;

        try
        {
            var fullPath = Path.Combine(_environment.WebRootPath, "boosters", iconPath);

            if (!System.IO.File.Exists(fullPath))
            {
                Console.WriteLine($"File not found: {fullPath}");
                return;
            }

            DrawStratagemBackground(canvas, x, y);
            DrawSvgIcon(canvas, fullPath, x, y, StratagemIconSize);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error drawing booster {iconPath}: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }

    private static void DrawStratagemIcon(SKCanvas canvas, string fullPath, float x, float y)
    {
        DrawSvgIcon(canvas, fullPath, x, y, StratagemIconSize);
    }

    private static void DrawSvgIcon(SKCanvas canvas, string fullPath, float x, float y, float iconSize)
    {
        using var svg = new SKSvg();
        var picture = svg.Load(fullPath);
        if (picture != null)
        {
            var svgWidth = svg.Picture?.CullRect.Width ?? 0;
            var svgHeight = svg.Picture?.CullRect.Height ?? 0;

            if (svgWidth > 0 && svgHeight > 0)
            {
                var scale = iconSize * 0.9f / Math.Max(svgWidth, svgHeight);

                using (new SKAutoCanvasRestore(canvas))
                {
                    var clipRect = new SKRect(
                        x - iconSize / 2,
                        y - iconSize / 2,
                        x + iconSize / 2,
                        y + iconSize / 2
                    );
                    canvas.ClipRect(clipRect);

                    canvas.Translate(
                        x - svgWidth * scale / 2,
                        y - svgHeight * scale / 2
                    );
                    canvas.Scale(scale);

                    if (svg.Picture != null) canvas.DrawPicture(svg.Picture);
                }
            }
            else
            {
                Console.WriteLine($"Invalid SVG dimensions: {svgWidth}x{svgHeight}");
            }
        }
        else
        {
            Console.WriteLine($"Failed to load SVG: {fullPath}");
        }
    }


    private static void DrawBoosterIcon(SKCanvas canvas, string fullPath, float x, float y)
    {
        using var svg = new SKSvg();
        var picture = svg.Load(fullPath);
        if (picture != null)
        {
            var svgWidth = svg.Picture?.CullRect.Width ?? 0;
            var svgHeight = svg.Picture?.CullRect.Height ?? 0;

            if (svgWidth > 0 && svgHeight > 0)
            {
                var scale = StratagemIconSize * 0.7f / Math.Max(svgWidth, svgHeight);

                canvas.Save();
                canvas.Translate(
                    x - svgWidth * scale / 2,
                    y - svgHeight * scale / 2
                );
                canvas.Scale(scale);

                if (svg.Picture != null) canvas.DrawPicture(svg.Picture);

                canvas.Restore();
            }
            else
            {
                Console.WriteLine($"Invalid SVG dimensions: {svgWidth}x{svgHeight}");
            }
        }
        else
        {
            Console.WriteLine($"Failed to load SVG: {fullPath}");
        }
    }

    private static void DrawStratagemBackground(SKCanvas canvas, float x, float y)
    {
        using var bgPaint = new SKPaint();
        bgPaint.Color = new SKColor(40, 42, 45);
        bgPaint.IsAntialias = true;
        canvas.DrawRect(
            x - StratagemIconSize / 2,
            y - StratagemIconSize / 2,
            StratagemIconSize,
            StratagemIconSize,
            bgPaint
        );

        using var borderPaint = new SKPaint();
        borderPaint.Color = new SKColor(255, 212, 0);
        borderPaint.IsAntialias = true;
        borderPaint.Style = SKPaintStyle.Stroke;
        borderPaint.StrokeWidth = 3;

        canvas.DrawRect(
            x - StratagemIconSize / 2,
            y - StratagemIconSize / 2,
            StratagemIconSize,
            StratagemIconSize,
            borderPaint
        );
    }
}