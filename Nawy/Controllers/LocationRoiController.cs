using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryModel;
using RepositoryModel.Models;

namespace Nawy.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LocationRoiController : ControllerBase
	{
		private readonly IUnitOFWork unitOFWork;

		public LocationRoiController(IUnitOFWork unitOFWork)
		{
			this.unitOFWork = unitOFWork;
		}
		[HttpPost]
		public IActionResult CreateLocationRoi([FromBody] LocationRoi locationRoi)
		{
			if (locationRoi == null)
			{
				return BadRequest("Invalid data.");
			}

			unitOFWork.LocationRois.Addone(locationRoi);
			unitOFWork.Compelet();
			return Ok(locationRoi);
		}

		[HttpGet("GetById")]
		public IActionResult GetLocationRoiById(int id)
		{
			var locationRoi = unitOFWork.LocationRois.GetById(id);
			if (locationRoi == null)
			{
				return NotFound();
			}

			return Ok(locationRoi);
		}

		[HttpGet("GetAll")]
		public IActionResult GetAllLocationRois()
		{
			var locationRois = unitOFWork.LocationRois.GetAll();
			return Ok(locationRois);
		}

		[HttpPut]
		public IActionResult UpdateLocationRoi( [FromBody] LocationRoi locationRoi)
		{
		
			unitOFWork.LocationRois.Update(locationRoi);
			unitOFWork.Compelet();
			return Ok();
		}

		[HttpDelete]
		public IActionResult DeleteLocationRoi(int id)
		{
			var locationRoi = unitOFWork.LocationRois.GetById(id);
			if (locationRoi == null)
			{
				return NotFound();
			}

			unitOFWork.LocationRois.Delete(locationRoi);
			unitOFWork.Compelet();
			return Ok();
		}
	}
}
