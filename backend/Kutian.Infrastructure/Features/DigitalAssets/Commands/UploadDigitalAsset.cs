using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kutian.Domain.Entities;
using Kutian.Domain.ViewModels;
using Kutian.Utilities.Abstractions;
using Kutian.Utilities.Core.Models;
using Kutian.Utilities.Core.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Kutian.Infrastructure.Features.DigitalAssets.Commands
{
    public class UploadDigitalAsset
    {
        public class Command : IRequest<DigitalAssetResponse> { }

        public class Handler : IRequestHandler<Command, DigitalAssetResponse>
        {
            private readonly IDatabaseContext _databaseContext;
            private readonly IHttpContextAccessor __httpContextAccessor;

            public Handler(IDatabaseContext databaseContext, IHttpContextAccessor _httpContextAccessor)
            {
                __httpContextAccessor = _httpContextAccessor;
                _databaseContext = databaseContext;
            }

            public async Task<DigitalAssetResponse> Handle(Command command, CancellationToken cancellationToken)
            {
                var httpContext = __httpContextAccessor.HttpContext;
                var defaultFormOptions = new FormOptions();
                var digitalAssets = new List<DigitalAsset>();
                var contentType = httpContext.Request.ContentType;

                if (!MultipartRequestHelper.IsMultipartContentType(contentType))
                    throw new Exception($"Expected a multipart request, but got { contentType }");

                var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(contentType);
                var boundary = MultipartRequestHelper.GetBoundary(
                    mediaTypeHeaderValue, defaultFormOptions.MultipartBoundaryLengthLimit
                );
                var reader = new MultipartReader(boundary, httpContext.Request.Body);
                var section = await reader.ReadNextSectionAsync(cancellationToken);

                while (section != null)
                {
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                        {
                            using var targetStream = new MemoryStream();
                            await section.Body.CopyToAsync(targetStream, cancellationToken);

                            var name = $"{ contentDisposition.FileName }".Trim(new char[] { '"' }).Replace("&", "and");
                            var bytes = StreamHelper.ReadToEnd(targetStream);
                            var sectionContentType = section.ContentType;
                            var digitalAsset = new DigitalAsset(name, bytes, sectionContentType);

                            _databaseContext.Store(digitalAsset);
                            digitalAssets.Add(digitalAsset);
                        }
                    }

                    section = await reader.ReadNextSectionAsync(cancellationToken);
                }

                await _databaseContext.SaveChangesAsync(cancellationToken);

                return new DigitalAssetResponse()
                {
                    DigitalAssetsIds = digitalAssets.Select(x => x.DigitalAssetId).ToList()
                };
            }
        }
    }
}