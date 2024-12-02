using RepositoryModel.ConstEnum;

namespace Nawy.Dtos
{
    public class UnitDtoRead
    {
        public string Id { get; set; }
        public string Location { get; set; }

        public string Name { get; set; }
        public string Available { get; set; }

        public int AvailableShares { get; set; }

        // MONEY
        public decimal DownPayment { get; set; }

        public decimal CurrentUnitPrice { get; set; }

        public decimal StartUnitPrice { get; set; }


        public decimal CurrentUnitROI { get; set; }
        public decimal MonthlyPayment { get; set; }


        public string AvilableDate { get; set; }

        public string ExitDate { get; set; }

        //public string ContractImage { get; set; }

        public string DeveloperName { get; set; }

        public int NumberOfBathrooms { get; set; }

        public int NumberOfBedrooms { get; set; }
        public int Area { get; set; }
        public List<string> viewnames { get; set; } = new List<string>();

        public List<string> viewimages { get; set; } = new List<string>();

    }
}
