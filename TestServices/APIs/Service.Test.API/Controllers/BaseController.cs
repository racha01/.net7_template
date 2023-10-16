using AutoMapper;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Net;

namespace Service.Test.API.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult APIResponse(object model = null)
        {
            switch (Request.Method.ToLower())
            {
                case "head":
                case "get":
                case "delete":
                    if (model != null) return Ok(model);
                    else return NotFound();
                case "post":
                    if (model != null) return CreatedAtRoute(null, model);
                    else return StatusCode(201);
                case "put":
                case "patch":
                    if (model != null) return Ok(model);
                    else return NoContent();
                default:
                    return StatusCode((int)HttpStatusCode.MethodNotAllowed);
            }
        }

        protected IActionResult ValidateObjectId(string id, out MongoDB.Bson.ObjectId objectId)
        {
            MongoDB.Bson.ObjectId.TryParse(id, out objectId);
            if (objectId == MongoDB.Bson.ObjectId.Empty)
            {
                ModelState.AddModelError("id", "invalid document id format");
                return CustomBadRequest();
            }
            return null;
        }

        protected ActionResult CustomBadRequest()
        {
            var traceIdKey = Response.Headers["Req-Trace-Id"];
            var errorResponse = new APIStandardErrorResponse<object>()
            {
                trace_id = traceIdKey.Any() ? traceIdKey.First() : HttpContext.TraceIdentifier ?? string.Empty,
                errors = ModelState.Select(s => s).ToDictionary(k => k.Key, v => v.Value.Errors.Select(s => s.ErrorMessage).FirstOrDefault())
            };
            return StatusCode(400, errorResponse);
        }
    }
}
