using OSPABA;
namespace simulation
{
	public class Mc : IdList
	{
		//meta! userInfo="Generated code: do not modify", tag="begin"
		public const int CustomerArrival = 1001;
		public const int CustomerDeparture = 1002;
		public const int Init = 1003;
		public const int CustomerService = 1004;
		public const int CustomerServiceReception = 1005;
		public const int CustomerServiceInspection = 1006;
		public const int CustomerServiceSendToInspection = 1012;
		public const int GetNumberOfFreeMechanicsReception = 1010;
		public const int GetNumberOfFreeMechanicsWorkshop = 1011;
		public const int NewCustomer = 1016;
		//meta! tag="end"

		// 1..1000 range reserved for user
		public const int CheckInDone = 1;
		public const int InspectionDone = 2;
		public const int FreedResourceWorkshop = 3;
		public const int FreedResourceSTK = 4;
		public const int PaymentDone = 5;
		public const int LunchDone = 6;
		public const int NextClerkToLunch = 7;
	}
}