using Api.ModelDTO;
using DinkToPdf;
using DinkToPdf.Contracts;
using RazorLight;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Api.Services
{
    public class PDFService : IPDFService
    {
        private readonly IRazorLightEngine _razorEngine;
        private readonly IConverter _pdfConverter;
        public PDFService(IRazorLightEngine razorEngine, IConverter pdfConverter)
        {
            _razorEngine = razorEngine;
            _pdfConverter = pdfConverter;
        }
        public async Task<byte[]> Create(IEnumerable<CampaignDTO> verification)
        {
            var model = verification;
            var templatePath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, $"Api/Infrastructure/Template.cshtml");
            string template = await _razorEngine.CompileRenderAsync(templatePath, model);


            var globalSetting = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "Raport"
                //Out = @"D:\raport.PDF"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = template,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSetting,
                Objects = { objectSettings }
            };


            byte[] file = _pdfConverter.Convert(pdf);
            return file;
        }

    }
}
