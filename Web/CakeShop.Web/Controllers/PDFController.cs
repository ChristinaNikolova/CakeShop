namespace CakeShop.Web.Controllers
{
    using System.Collections.Generic;

    using CakeShop.Common;
    using CakeShop.Services.Messaging;
    using Microsoft.AspNetCore.Mvc;

    public static class PDFController
    {
        public static List<EmailAttachment> PreparePdfFile<T>(T model, string viewName, string fileName, ActionContext context)
        {
            var pdfAsBytes = new Rotativa.AspNetCore
                .ViewAsPdf(viewName, model)
                .BuildFile(context)
                .GetAwaiter()
                .GetResult();

            var pdfAsEmailAttachment = new EmailAttachment()
            {
                Content = pdfAsBytes,
                FileName = fileName,
                MimeType = GlobalConstants.PdfMimeType,
            };

            var emailAttachments = new List<EmailAttachment>() { pdfAsEmailAttachment };

            return emailAttachments;
        }
    }
}
