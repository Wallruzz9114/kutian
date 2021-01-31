using System;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace Kutian.Utilities.Core.Models
{
    public static class MultipartRequestHelper
    {
        public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);

            if (string.IsNullOrWhiteSpace(boundary.ToString()))
                throw new InvalidDataException("Missing content-type boundary");

            if (boundary.Length > lengthLimit)
                throw new InvalidDataException($"Multipart boundary length limit { lengthLimit } exceeded.");

            return boundary.ToString();
        }

        public static bool IsMultipartContentType(string contentType) =>
            !string.IsNullOrEmpty(contentType) && contentType.Contains("multipart/", StringComparison.OrdinalIgnoreCase);

        public static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            return contentDisposition != null
                && contentDisposition.DispositionType.Equals("form-data")
                && string.IsNullOrEmpty(contentDisposition.FileName.ToString())
                && string.IsNullOrEmpty(contentDisposition.FileNameStar.ToString());
        }

        public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            return contentDisposition != null
                && contentDisposition.DispositionType.Equals("form-data")
                && (!string.IsNullOrEmpty(contentDisposition.FileName.ToString())
                    || !string.IsNullOrEmpty(contentDisposition.FileNameStar.ToString()));
        }
    }
}