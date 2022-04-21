using Microsoft.AspNetCore.Http;

namespace mniaAPI.Helpers
{
    public static class ValidateImage
    {
        public static bool imageFile(IFormFile anexo)
        {

            switch (anexo.ContentType)
            {
                case "image/jpeg":
                    return true;
                case "image/bmp":
                    return true;
                case "image/gif":
                    return true;
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }

    }
}