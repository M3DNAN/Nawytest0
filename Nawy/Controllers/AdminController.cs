using DataBaseModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nawy.Dtos;
using RepositoryModel;
using RepositoryModel.ConstEnum;
using RepositoryModel.Models;

namespace Nawy.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly IUnitOFWork unitOFWork;

		public AdminController(IUnitOFWork unitOFWork)
		{
			this.unitOFWork = unitOFWork;
		}

		[HttpPost("Analysis")]
		public IActionResult Analysis(int id)
		{
			var location = unitOFWork.LocationRois.GetById(id);
			if (location != null) {
				{
					double x = (double)location.RoiPercentage;
					double numerator = Math.Log(1.5);
					double denominator = Math.Log(1 + x);
					double result = numerator / denominator;
					return Ok(new { T = result });
				}
			}
			else return BadRequest();
		}

		[HttpGet("ActiveUsers")]
		public IActionResult ActiveUsers()
		{
			// Step 1: Fetch users with their transactions
			var users = unitOFWork.ApplicationUsers
				.FindAll(u => u.CompanyInstallments.Any() || u.SharesTransactionS.Any(),
						 new string[] { "CompanyInstallments", "SharesTransactionS" })
				.Select(user => new
				{
					user.Id,
					user.UserName,
					user.Email,
					user.Trust,
					user.External,
					user.ProfilePicture,
					user.NationalId,
					TotalCompanyTransactions = user.CompanyInstallments.Count,
					TotalSharesTransactions = user.SharesTransactionS.Count
				})
				.ToList();

			// Step 2: Return the filtered result
			return Ok(users);
		}
		[HttpPost("FinishedUnits")]
		public IActionResult FinishedUnits()
		{
	
			var fundedUnits = unitOFWork.Units.GetAllWith(new string[] { "UnitDescription", "Developer", "LocationRoi" })
											 .Where(u => u.Available == Avalibility.Funded)
											 .ToList();


			var unitInfoList = new List<AdminDto>();

			foreach (var unit in fundedUnits)
			{
				var locationRoi = unitOFWork.LocationRois.Findme(lr => lr.Location == unit.Location);

				if (locationRoi != null)
				{
					var monthsPassed = (DateTime.Now.Year - unit.AvilableDate.Year) * 12 + DateTime.Now.Month - unit.AvilableDate.Month;

					decimal monthlyRoi = locationRoi.RoiPercentage / 12;

					decimal updatedPrice = unit.CurrentUnitPrice;
					for (int i = 0; i < monthsPassed; i++)
					{
						updatedPrice += updatedPrice * (monthlyRoi / 100);
					}

					var unitDto = new AdminDto
					{
						Id = unit.Id,
						Name = unit.Name,
						Location = unit.Location,
						StartUnitPrice = unit.StartUnitPrice,
						CurrentUnitPrice = updatedPrice,
						DownPayment = unit.DownPayment,
						CurrentUnitROI = unit.CurrentUnitROI,
						MonthlyPayment = unit.MonthlyPayment,
						AvailableShares = 0,
						AvilableDate = unit.AvilableDate.ToString("dddd, MMMM d 'at' h:mm tt"),
						ExitDate = unit.ExitDate.ToString("dddd, MMMM d 'at' h:mm tt"),
						Available = unit.Available.ToString(),
						DeveloperName = unit.Developer.Name,
						Area = unit.UnitDescription.Area,
						NumberOfBathrooms = unit.UnitDescription.NumberOfBathrooms,
						NumberOfBedrooms = unit.UnitDescription.NumberOfBedrooms,
						RoiPercentage = locationRoi.RoiPercentage
					};
					foreach (var image in unit.UnitImages)
					{
						var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{image.PhotoUrl}";
						unitDto.viewimages.Add(imageUrl);
					}

					foreach (var view in unit.UnitViews)
					{
						unitDto.viewnames.Add(view.Name.ToString());
					}

					unitInfoList.Add(unitDto);

					
					unit.CurrentUnitPrice = updatedPrice;

					unitOFWork.Units.Update(unit);
				}
			}

			// Step 10: Commit changes in the database
			unitOFWork.Compelet();

			// Step 11: Return the updated unit information
			return Ok( unitInfoList);
		}
		//public IActionResult ActiveUnits()
		//{
		//	return Ok();
		//}













		[HttpGet("Units")]
		// [Authorize]
		public IActionResult GetAll()
		{
			List<UnitDtoRead> units = new List<UnitDtoRead>();

			var unitsdata = unitOFWork.Units.GetAllWith(new string[] { "UnitImages" , "UnitDescription",
		  "UnitViews","Developer"
		  });

			foreach (var item in unitsdata)
			{

				string developername = item.Name;
				Console.WriteLine(item.Developer.Id);
				if (developername == null)
				{ Console.WriteLine("i null"); }

				UnitDtoRead unitcurrent = new UnitDtoRead()
				{
					Id = item.Id,
					Name = item.Name,
					Location = item.Location,
					StartUnitPrice = item.StartUnitPrice,
					CurrentUnitPrice = item.CurrentUnitPrice,
					DownPayment = item.DownPayment,
					CurrentUnitROI = item.CurrentUnitROI,
					MonthlyPayment = item.MonthlyPayment,
					AvailableShares = item.AvailableShares,
					AvilableDate = item.AvilableDate.ToString("dddd, MMMM d 'at' h:mm tt"),
					ExitDate = item.ExitDate.ToString("dddd, MMMM d 'at' h:mm tt"),
					Available = item.Available.ToString(),
					DeveloperName = item.Developer.Name,
					Area = item.UnitDescription.Area,
					NumberOfBathrooms = item.UnitDescription.NumberOfBathrooms,
					NumberOfBedrooms = item.UnitDescription.NumberOfBedrooms
				};
				foreach (var image in item.UnitImages)
				{
					var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{image.PhotoUrl}";
					unitcurrent.viewimages.Add(imageUrl);
				}

				foreach (var view in item.UnitViews)
				{
					unitcurrent.viewnames.Add(view.Name.ToString());
				}


				units.Add(unitcurrent);


			}




			return Ok(units);
		}
	}
}
