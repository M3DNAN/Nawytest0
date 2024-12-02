using DataBaseModel;
using Microsoft.AspNetCore.Authorization;
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
    public class UnitController : ControllerBase
    {

        private readonly IUnitOFWork unitOFWork;

        public UnitController(IUnitOFWork unitOFWork)
        {
            this.unitOFWork = unitOFWork;
        }




        [HttpGet("Units")]
        [Authorize]
        public IActionResult GetAll()
        { 
          List<UnitDtoRead> units=new List<UnitDtoRead> ();

          var  unitsdata=unitOFWork.Units.GetAllWith(new string[] { "UnitImages" , "UnitDescription",
          "UnitViews","Developer"
          });

            foreach (var item in unitsdata) {
            
               string developername = item.Name;
                Console.WriteLine(item.Developer.Id);
                if (developername ==null)
                { Console.WriteLine("i null"); }

                UnitDtoRead unitcurrent=new UnitDtoRead()
                {
                    Id=item.Id,
                    Name=item.Name,
                    Location=item.Location,
                    StartUnitPrice=item.StartUnitPrice,
                    CurrentUnitPrice=item.CurrentUnitPrice,
                    DownPayment=item.DownPayment,
                    CurrentUnitROI=item.CurrentUnitROI,
                    MonthlyPayment=item.MonthlyPayment,
                    AvailableShares=item.AvailableShares,
                    AvilableDate= item.AvilableDate.ToString("dddd, MMMM d 'at' h:mm tt"),
                    ExitDate=item.ExitDate.ToString("dddd, MMMM d 'at' h:mm tt"),
                    Available=item.Available.ToString(),
                    DeveloperName= item.Developer.Name,
                    Area=item.UnitDescription.Area,
                    NumberOfBathrooms=item.UnitDescription.NumberOfBathrooms,
                    NumberOfBedrooms=item.UnitDescription.NumberOfBedrooms
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























            [HttpPost]
        public IActionResult Create([FromBody] UnitDTO unitDTO)
        {
            if (unitDTO == null)
            {
                return BadRequest("The unitDTO field is required.");
            }

            // Check if Developer exists
            var developer = unitOFWork.Developers.GetByStringId(unitDTO.DeveloperId);
            //if (developer == null)
            //{
            //    developer = new Developer
            //    {
            //        Id = unitDTO.DeveloperId,
            //        Name = unitDTO.DeveloperName
            //    };
            //}

            // Create Unit object
            var unit = new Unit
            {
                Id = Guid.NewGuid().ToString(),
                Location = unitDTO.Location,
                Name = unitDTO.Name,
                Available = unitDTO.Available,
                DeveloperId = unitDTO.DeveloperId,
                AvailableShares = unitDTO.AvailableShares,
                DownPayment = unitDTO.DownPayment,
                StartUnitPrice= unitDTO.StartUnitPrice,
                CurrentUnitPrice = unitDTO.CurrentUnitPrice,
                CurrentUnitROI = unitDTO.CurrentUnitROI,
                MonthlyPayment = unitDTO.MonthlyPayment,
                AvilableDate = unitDTO.AvilableDate,
                ExitDate = unitDTO.ExitDate,
                //ContractImage = unitDTO.ContractImage,
            };

            unitOFWork.Units.Addone(unit);
            unitOFWork.Compelet();

            var unitcuirrent = unitOFWork.Units.GetByStringId(unit.Id);

            UnitDescription description = new UnitDescription
            {
                UnitId = unitcuirrent.Id,
                NumberOfBathrooms = unitDTO.UnitDescription.NumberOfBathrooms,
                NumberOfBedrooms = unitDTO.UnitDescription.NumberOfBedrooms,
                Area = unitDTO.UnitDescription.Area
            };

            unitOFWork.UnitDescriptions.Addone(description);
            unitOFWork.Compelet();

            List<UnitImages> imagelist = new List<UnitImages>();

            foreach (var img in unitDTO.UnitImages)
            {

                imagelist.Add(new UnitImages()
                {
                    UnitId = unit.Id,
                    PhotoUrl = img.ImageUrl,
                });
            }

            foreach (var img in imagelist)
            {
                unitOFWork.UnitImages.Addone(img);

            }

            unitOFWork.Compelet();

            List<UnitView> Viewlist = new List<UnitView>();

            foreach (var View in unitDTO.UnitViews)
            {

                Viewlist.Add(new UnitView()
                {
                    UnitId = unit.Id,
                    Name = View.ViewName,
                });
            }

            foreach (var View in Viewlist)
            {
                unitOFWork.UnitViews.Addone(View);

            }


            unitOFWork.Compelet();

            return Ok("add ok");
        }









    }
}
