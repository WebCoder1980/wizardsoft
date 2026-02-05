using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wizardsoft_testtask.Constants;
using wizardsoft_testtask.Dtos;
using wizardsoft_testtask.Service;

namespace wizardsoft_testtask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TreeController : ControllerBase
    {
        private readonly ITreeService _service;

        public TreeController(ITreeService service)
        {
            _service = service;
        }

        [HttpGet("{id:long}")]
        [Authorize]
        public async Task<ActionResult<TreeNodeResponse>> GetById(long id, CancellationToken cancellationToken)
        {
            TreeNodeResponse? node = await _service.GetAsync(id, cancellationToken);
            if (node == null)
            {
                return NotFound();
            }

            return Ok(node);
        }

        [HttpGet("roots")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TreeNodeRootResponse>>> GetRoots(CancellationToken cancellationToken)
        {
            IReadOnlyCollection<TreeNodeRootResponse> roots = await _service.GetRootsWithChildrenIdAsync(cancellationToken);
            return Ok(roots);
        }

        [HttpGet("export")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TreeNodeResponse>>> Export(CancellationToken cancellationToken)
        {
            IReadOnlyCollection<TreeNodeResponse> tree = await _service.ExportAsync(cancellationToken);
            return Ok(tree);
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.ADMIN)]
        public async Task<ActionResult<TreeNodeResponse>> Create(TreeNodeCreateRequest request, CancellationToken cancellationToken)
        {
            TreeNodeResponse created = await _service.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:long}")]
        [Authorize(Roles = AppRoles.ADMIN)]
        public async Task<ActionResult<TreeNodeResponse>> Update(long id, TreeNodeUpdateRequest request, CancellationToken cancellationToken)
        {
            TreeNodeResponse? updated = await _service.UpdateAsync(id, request, cancellationToken);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id:long}")]
        [Authorize(Roles = AppRoles.ADMIN)]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            bool deleted = await _service.DeleteAsync(id, cancellationToken);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
