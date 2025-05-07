using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Burguer404.Frontend.Pages.Pedidos
{
    public class QrCodeModel : PageModel
    {
        public string QRCodeText { get; set; }
        public void OnGet(string qrcode)
        {
            QRCodeText = qrcode;
        }
    }
}
