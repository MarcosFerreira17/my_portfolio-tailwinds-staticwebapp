using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MWF.Projects.Application.RequestFeatures;
using MWF.Projects.Domain.Dtos;
using MWF.Projects.Domain.Interfaces;

namespace MWF.Projects.Host.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MWF.ProjectsController : ControllerBase
{
    protected readonly IMWF.ProjectsService _service;

    public MWF.ProjectsController(IMWF.ProjectsService service)
    {
        _service = service;
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PagedList<MWF.ProjectsDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetAll([FromQuery] MWF.ProjectsParameters mwf.projectsParameters)
    {
        var entitiesFromDb = await _service.PagedGetAll(mwf.projectsParameters);
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
    public async Task<IActionResult> Create([FromBody] MWF.ProjectsDto addDto)
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
    public async Task<IActionResult> UpdateAsync(long id, [FromBody] MWF.ProjectsDto updateDto)
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