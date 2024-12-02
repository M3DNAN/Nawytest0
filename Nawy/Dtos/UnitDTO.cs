using RepositoryModel.ConstEnum;

namespace Nawy.Dtos
{
   
        public class UnitDTO
        {

            public string Location { get; set; }

            public string Name { get; set; }
            public Avalibility Available { get; set; }

            public int AvailableShares { get; set; }

            // MONEY
            public decimal DownPayment { get; set; }

            public decimal CurrentUnitPrice { get; set; }

        public decimal StartUnitPrice { get; set; }

        
            public decimal CurrentUnitROI { get; set; }
        public decimal MonthlyPayment { get; set; }


            public DateTime AvilableDate { get; set; }

            public DateTime ExitDate { get; set; }

            //public string ContractImage { get; set; }
            public string DeveloperId { get; set; }

            public string DeveloperName { get; set; }



            public List<UnitImageDTO> UnitImages { get; set; } = new List<UnitImageDTO>();
            public UnitDescriptionDTO UnitDescription { get; set; }


            public List<UnitViewDTO> UnitViews { get; set; } = new List<UnitViewDTO>();
        }
        public class UnitDescriptionDTO
        {
            //public int Id { get; set; }
            public string UnitId { get; set; }
            public int NumberOfBathrooms { get; set; }
            public int NumberOfBedrooms { get; set; }
            public int Area { get; set; }
        }
        public class UnitImageDTO
        {
            public string ImageUrl { get; set; }
        }

        public class UnitViewDTO
        {
            public ViewSpecial ViewName { get; set; }
        }
    }


