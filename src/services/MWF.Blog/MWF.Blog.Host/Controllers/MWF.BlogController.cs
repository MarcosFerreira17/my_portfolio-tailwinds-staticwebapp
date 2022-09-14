using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MWF.Blog.Application.RequestFeatures;
using MWF.Blog.Domain.Dtos;
using MWF.Blog.Domain.Interfaces;

namespace MWF.Blog.Host.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MWF.BlogController : ControllerBase
{
    protected readonly IMWF.BlogService _service;

    public MWF.BlogController(IMWF.BlogService service)
    {
        _service = service;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PagedList<MWF.BlogDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetAll([FromQuery] MWF.BlogParameters mwf.blogParameters)
    {
        var entitiesFromDb = await _service.PagedGetAll(mwf.blogParameters);
        Response.Headers.Add("X-Pagination",
            JsonConvert.SerializeObject(entitiesFromDb.MetaData));

        return Ok(entitiesFromDb);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetById(long id)
    {
        var entityFromDb = await _service.GetById(id);
        return Ok(entityFromDb);
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> Create([FromBody] MWF.BlogDto addDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await _service.Create(addDto);
        return Ok();
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] MWF.BlogDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        await _service.Update(id, updateDto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}